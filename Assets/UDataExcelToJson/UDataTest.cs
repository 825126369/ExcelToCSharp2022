using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using uData.DataTable;
using UnityEngine;

public class UDataTest : MonoBehaviour
{
    public TextAsset mTextAsset;
    private Dictionary<string, DataTableBase> m_DataTables = new Dictionary<string, DataTableBase>();
    // Start is called before the first frame update
    void Start()
    {
       CreateDataTable(typeof(AAA.LevelData));
        
       DataTableService.DataTable<AAA.LevelData> mLeveDataTable = m_DataTables["AAA.LevelData"] as DataTableService.DataTable<AAA.LevelData>;
       foreach (var v in mLeveDataTable.GetAllDatas())
       {
           AAA.LevelData data = v as AAA.LevelData;
               Debug.Log(data.UnlockingLevelID[0] +", " +data.UnlockingLevelID[1]);
           
       }
    }
    
     DataTableBase CreateDataTable(Type dataRowType)
    {
        if (!typeof(IGameData).IsAssignableFrom(dataRowType))
        {
            Debug.LogError(string.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
        }
        
        Type dataTableType = typeof(DataTableService.DataTable<>).MakeGenericType(dataRowType);
        DataTableBase dataTable = (DataTableBase) Activator.CreateInstance(dataTableType, dataRowType.FullName);
        TableML.TableFile file = TableML.TableFile.LoadFromString(mTextAsset.text);
        dataTable.ReloadAll(file);
#if UNITY_EDITOR && HOTEXCEL
            dataTable.HoTUpdateEventArg += (sender, arg) =>
            {
               TableFilePathsAttribute attr = arg.Type.GetTableFilesAttributes();
                InternalLoadDataTable(string.Empty,arg.Type, attr.TableFilePaths);
            };
#endif
        m_DataTables.Add(dataTable.Name, dataTable);
        return dataTable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
