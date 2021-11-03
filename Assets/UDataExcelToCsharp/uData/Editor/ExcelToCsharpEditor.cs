using System.IO;
using System.Text;
using UnityEditor;

namespace uData
{
    public class UDataExcelToJsonEditor
    {
        [MenuItem("Tools/UDataExcelToCsharp")]
        public static void DoCompileSettings()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var sourcePath = "Assets/UDataExcelToCsharp/Excel/";
            var compilePath = "Assets/UDataExcelToCsharp/Out_DataPath/";
            string SettingCodePath = "Assets/UDataExcelToCsharp/Out_CS/";

            if (!Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(sourcePath);
            }

            if (!Directory.Exists(compilePath))
            {
                Directory.CreateDirectory(compilePath);
            }

            if (!Directory.Exists(SettingCodePath))
            {
                Directory.CreateDirectory(SettingCodePath);
            }

            var bc = new uData.BatchCompiler();
            string CSNameSpace = "s7u.dtb.exceldata";

            var template = DefaultTemplate.GenCodeTemplate;
            var results = bc.CompileTableMLAll(sourcePath, compilePath, SettingCodePath, template, CSNameSpace, ".txt", null, true);

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
}
