using System;
using System.Collections.Generic;
using s7u.dtb.exceldata;
using uData;
using UnityEngine;

public class UDataTest : MonoBehaviour
{
    public TextAsset mTextAsset;
    private Dictionary<string, DataTableBase> m_DataTables = new Dictionary<string, DataTableBase>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDataTable(typeof(LevelData));

        DataTableService.DataTable<LevelData> mLeveDataTable = m_DataTables["s7u.dtb.exceldata.LevelData"] as DataTableService.DataTable<LevelData>;
        foreach (var v in mLeveDataTable.GetAllDatas())
        {
            LevelData data = v as LevelData;
            Debug.Log(data.UnlockingLevelID[0] + ", " + data.UnlockingLevelID[1]);

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
        uData.TableFile file = uData.TableFile.LoadFromString(mTextAsset.text);
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
