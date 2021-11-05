using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;

namespace uData
{
    public sealed partial class DataTableService : IDataTableService
    {
        private Dictionary<string, DataTableBase> m_DataTables;
        
        public DataTableService()
        {
            m_DataTables = new Dictionary<string, DataTableBase>();
        }

        public DataTable<T> LoadDataTable<T>(string dataTSV) where T : IGameData
        {
            string Key = typeof(T).FullName;
            DataTableBase dataTable = null;
            if (!m_DataTables.TryGetValue(Key, out dataTable))
            {
                if (!typeof(IGameData).IsAssignableFrom(typeof(T)))
                {
                    Debug.LogError(string.Format("Data row type '{0}' is invalid.", typeof(T).FullName));
                }
                
                Type dataTableType = typeof(DataTableService.DataTable<T>);
                DataTable<T> newDataTable = Activator.CreateInstance(dataTableType, Key) as DataTable<T>;
                TableFile file = TableFile.LoadFromString(dataTSV);
                newDataTable.ReloadAll(file);
                m_DataTables.Add(Key, newDataTable);
                dataTable = newDataTable;
            }

            return dataTable as DataTable<T>;
        }

        public DataTable<T> GetDataTable<T>() where T : IGameData
        {
            if (!typeof(IGameData).IsAssignableFrom(typeof(T)))
            {
                Debug.LogError(string.Format("Data row type '{0}' is invalid.", typeof(T).FullName));
            }

            DataTableBase table = null;
            if (m_DataTables.TryGetValue(typeof(T).FullName, out table))
            {
                return table as DataTable<T>;
            }

            Debug.LogError("You need load DataTable first. Type :" + typeof(T).Name);
            return null;
        }
        
        public T GetData<T>(int Id) where T : IGameData
        {
            if (!typeof(IGameData).IsAssignableFrom(typeof(T)))
            {
                Debug.LogError(string.Format("Data row type '{0}' is invalid.", typeof(T).FullName));
            }
            
            DataTable<T> table = GetDataTable<T>();
            T data = table[Id];
            return data;
        }
        
        public T[] GetAllDatas<T>() where T : IGameData
        {
            DataTable<T> table = GetDataTable<T>();
            T[] datas = table.GetAllDatas() as T[];
            return datas;
        }

        public T[] GetAllDatas<T>(Predicate<T> _condition) where T : IGameData
        {
            if (_condition == null)
            {
                throw new Exception("you Condition is invaild");
            }
            
            T[] datas = GetAllDatas<T>();
            List<T> rets = new List<T>();
            foreach (T ret in datas)
            {
                if (_condition(ret))
                {
                    rets.Add(ret);
                }
            }
            
            return rets.ToArray();
        }
        
        public int GetDataCount<T>() where T : IGameData
        {
            DataTable<T> table = GetDataTable<T>();
            return table.Count;
        }

        public T GetMinIdData<T>() where T : IGameData
        {
            DataTable<T> table = GetDataTable<T>();
            return (T)table.MinIdData;
        }
        
        public T GetMaxIdData<T>() where T : IGameData
        {
            DataTable<T> table = GetDataTable<T>();
            return (T)table.MaxIdData;
        }

        public bool HasDataTable<T>() where T : IGameData
        {
            if (!typeof(IGameData).IsAssignableFrom(typeof(T)))
            {
                Debug.LogError(string.Format("Data row type '{0}' is invalid.", typeof(T).FullName));
            }
            
            return m_DataTables.ContainsKey(typeof(T).FullName);
        }


    }

}
