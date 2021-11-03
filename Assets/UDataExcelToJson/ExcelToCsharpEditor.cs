using System.Collections;
using System.Collections.Generic;
using System.Text;
using TableML.Compiler;
using uData.DataTable;
using UnityEditor;
using UnityEngine;

public class UDataExcelToJsonEditor
{
    [MenuItem("Tools/UDataExcelToJson")]
    public static void DoCompileSettings()
    {
        var sourcePath = "Assets/UDataExcelToJson/Excel/";
        var compilePath = "Assets/UDataExcelToJson/Out_DataPath/";
        string SettingCodePath = "Assets/UDataExcelToJson/Out_CS/";
        
        var bc = new BatchCompiler();
        string CSNameSpace = "AAA";
        
        var template = DefaultTemplate.GenCodeTemplate;
        var results = bc.CompileTableMLAll(sourcePath, compilePath, SettingCodePath, template, CSNameSpace, ".json", null, true);
        
        var sb = new StringBuilder();
        foreach (var r in results)
        {
            sb.AppendLine(string.Format("Excel {0} -> {1}", r.ExcelFile, r.TabFileRelativePath));
        }
        
        // make unity compile
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
