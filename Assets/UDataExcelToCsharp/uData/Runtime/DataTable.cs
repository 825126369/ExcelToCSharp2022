
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using uData;
using UnityEngine;

namespace uData
{
    public sealed partial class DataTableService
    {
        public sealed class DataTable<T> : DataTableBase where T :  IGameData
        {
#if UNITY_EDITOR && HOTEXCEL
            private int m_ReloadCount;      
            private EventHandler<EditorHotUpdateEventArg> m_HoTUpdateEventArg;
#endif
            
            private readonly Dictionary<int, T> m_dict;           
            private T m_MinIdData;
            private T m_MaxIdData;
            private IDataParser m_Parser;
            
            public Type DataType
            {
                get
                {
                    return typeof(T);
                }
            }

            public int Count
            {
                get
                {
                    return m_dict.Count;
                }
            }
            
            public T this[int id]
            {
                get
                {
                    return GetData(id);
                }
            }
#if UNITY_EDITOR && HOTEXCEL
            public int ReloadCount
            {
                get
                {
                    return m_ReloadCount;
                }
                set
                {
                    m_ReloadCount = value;
                }
            }
            public override event EventHandler<EditorHotUpdateEventArg> HoTUpdateEventArg
            {
                add
                {
                    m_HoTUpdateEventArg += value;
                }
                remove
                {
                    m_HoTUpdateEventArg -= value;
                }               
            }
#endif
            public T MinIdData
            {
                get
                {
                    return m_MinIdData;
                }
            }

            public T MaxIdData
            {
                get
                {
                    return m_MaxIdData;
                }
            }

            
            public DataTable(string _name)
                : base(_name)
            {
                m_dict = new Dictionary<int, T>();  
                TableFilePathsAttribute attr= typeof(T).GetTableFilesAttributes();
                m_Parser = (IDataParser)Activator.CreateInstance(attr.ParserType);         
            }
            
            public T[] GetAllDatas()
            {
                return m_dict.Values.ToArray();
            }

            public T GetData(int id)
            {
                T dr;
                if (m_dict.TryGetValue(id, out dr))
                {
                    return dr;
                }

                Debug.LogError(string.Format("You use id read table is error. id:{0} Type : {1}", id, typeof(T)));
                return dr;
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }
            
            public bool HasData(int id)
            {
                return m_dict.ContainsKey(id);
            }
                
            public void ReloadAll(TableFile _file )
            {
#if UNITY_EDITOR && HOTEXCEL

                TableFilePathsAttribute attr= typeof(T).GetTableFilesAttributes();
                if (ReloadCount < attr.TableFilePaths.Length)
                {
                 
                    if (Utility.IsFileSystemMode)
                    {
                        var tabFilePath = attr.TableFilePaths[ReloadCount];
                        Utility.Watch.WatchDataTable(tabFilePath, (path) =>
                        {
                            if (path.Replace("\\", "/").EndsWith(path))
                            {
                                //SendEvent
                                UnityEditorUtils.CallMainThread(() =>
                                {
                                    if (m_HoTUpdateEventArg != null)
                                    {
                                        m_HoTUpdateEventArg(this, new EditorHotUpdateEventArg(Name, DataType));
                                    }
                                });
                             
                            }
                        });
                    }
                    ReloadAll(true, _file);
                }
                else
                {
                     ReloadAll(false, _file);
                }
#else
                ReloadAll(true, _file);
#endif
            }

      

            private void ReloadAll(bool throwWhenDuplicatePrimaryKey,TableFile _file)
            {
                    TableFile tableFile = _file;       
                    using (tableFile)
                    {
                        foreach (var row in tableFile)
                        {
                            var pk = Utility.ParsePrimaryKey(row);
                            
                            T data;
                            if (!m_dict.TryGetValue(pk, out data))
                            {                            
                                m_Parser.Reload(row);
                                data = (T)m_Parser.GetData();
                                m_dict[data.Id] = data;
                               
                                if (m_MinIdData == null || m_MinIdData.Id > data.Id)
                                {
                                    m_MinIdData = data;
                                }

                                if (m_MaxIdData == null || m_MaxIdData.Id < data.Id)
                                {
                                    m_MaxIdData = data;
                                }

                            }
                            else
                            {
                                if (throwWhenDuplicatePrimaryKey)
                                {
                                    throw new System.Exception(string.Format("DuplicateKey, Class: {0}, Key: {1}", DataType.Name, pk));
                                }
                                else
                                {                                  
                                    m_Parser.Reload(row);
                                    data = (T)m_Parser.GetData();
                                    m_dict[data.Id] =data;
                                }
                            }

                        }

                    }
#if UNITY_EDITOR && HOTEXCEL
                ReloadCount++;
#endif
            }

           
        }

    }
}

