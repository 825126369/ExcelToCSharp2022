using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;

public class ExcelToCSharp
{
    static string excelDir = "Assets/Excel";
    static string JsonDir = "Assets/Out_Json";
    static string CSDir = "Assets/Out_CS";
    static List<string> mListColumnTypeInfo = null;
    static List<int> mListValidColumn = null;
    
    [MenuItem("Tools/ExcelToCSharp")] 
    static void Build()
    {
        if (!Directory.Exists(JsonDir))
        {
            Directory.CreateDirectory(JsonDir);
        }
        
        if (!Directory.Exists(CSDir))
        {
            Directory.CreateDirectory(CSDir);
        }
        
       //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        
        string[] allFiles = Directory.GetFiles("Assets/", "*.xlsx", SearchOption.AllDirectories);
        foreach (string filePath in allFiles)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            
                        }
                    } while (reader.NextResult());
                    
                    System.Data.DataSet result = reader.AsDataSet();
                    ParseDataSet(result, filePath);
                }
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    static void ParseDataSet(DataSet result, string filePath)
    {
        ParseColumnType(result);
        //ParseData(result, filePath);
        CreateCSFile(result, filePath);
    }
    
    static void ParseColumnType(DataSet result)
    {
        mListColumnTypeInfo = new List<string>();
        mListValidColumn = new List<int>();
        for (int i = 0; i < result.Tables.Count; i++)
        {
            DataTable mTable = result.Tables[i];
            if (mTable.TableName == "Export Summary")
            {
                continue;
            }
            
            for (int j = 0; j < mTable.Columns.Count; j++)
            {
                string fieldName = string.Empty;
                string desName = string.Empty;
                string typeName = string.Empty;
                string subtypeName = string.Empty;
                
                for (int k = 0; k < mTable.Rows.Count; k++)
                {
                    string value = mTable.Rows[k][j].ToString();
                    Console.WriteLine("(" + j + ", " + k + ") : " + value);
                    
                    if (k == 1) //字段名
                    {
                        fieldName = value;
                        Debug.Log("字段名： " + fieldName + " | " + value);
                    }
                    else if (k == 2) //类型
                    {
                        typeName = value;
                        Debug.Log("类型名： " + typeName + " | " + value);
                    }
                    else if (k == 0) //描述
                    {
                        desName = value;
                        Debug.Log("描述： " + desName + " | " + value);
                    }
                    else
                    {
                        continue;
                    }
                }
                
                mListColumnTypeInfo.Add(fieldName);
                mListColumnTypeInfo.Add(desName);
                mListColumnTypeInfo.Add(typeName);
                mListColumnTypeInfo.Add(subtypeName);
            }
        }
    }
    
    static void CheckValidColumn()
    {
         
    }
    
    static void CreateCSFile(DataSet mCollection, string filePath)
    {
        string csFileName = Path.GetFileNameWithoutExtension(filePath).Trim();
        
        string mStr = "";
        mStr += "using System.Collections;\n";
        mStr += "using System.Collections.Generic;\n";
        mStr += "namespace s7u.dtb.exceldata\n{\n";
        foreach (DataTable mDataTable in mCollection.Tables)
        {
            if (mDataTable.Rows.Count >= 3)
            {
                string className = mCollection.Tables.Count > 0 ? mDataTable.TableName : csFileName;
                mStr += "\tpublic class " + className + ":ExcelDbBase\n\t{\n";
                //字段
                for (int j = 0; j < mDataTable.Columns.Count; j++)
                {
                    string fieldDes = mDataTable.Rows[0][j].ToString().Trim();
                    string fieldName = mDataTable.Rows[1][j].ToString().Trim();
                    string fieldTypeName = mDataTable.Rows[2][j].ToString().Trim();
                    
                    if (!string.IsNullOrWhiteSpace(fieldDes) && !string.IsNullOrWhiteSpace(fieldName) &&
                        !string.IsNullOrWhiteSpace(fieldTypeName))
                    {
                        mStr += "\t\t/// <summary>\n\t\t/// " + fieldDes + "\n\t\t/// </summary>\n";

                        if (fieldTypeName.Contains("["))
                        {
                            /* string length = name.Substring(name.IndexOf('[') + 1, name.IndexOf(']')-name.IndexOf('[')-1);
                             name = name.Substring(0, name.IndexOf('['));
                             mStr += "\t\tpublic readonly " + name + "[] " + mDataTable.Rows[3][j].ToString()+"=new "+name+"[" + length+"];\n";*/
                            string fieldType = fieldTypeName.Substring(0, fieldTypeName.IndexOf('['));
                            mStr += "\t\tpublic readonly " + "List<" + fieldType + "> " + fieldName + "=new " +
                                    "List<" + fieldType + ">();\n";
                        }
                        else
                        {
                            mStr += "\t\tpublic readonly " + fieldTypeName + " " + fieldName + ";\n";
                        }
                    }
                }
                
                mStr += "\t}\n\n";
            }
        }
        
        mStr += "}";
        
        string csFilePath = Path.Combine(CSDir, csFileName);
        File.WriteAllText(csFilePath + ".cs", mStr);
        Debug.Log("Finsh Save CS File: " +csFileName);
    }

    // static void ParseData(DataSet result)
    // {
    //     string outStr = "local " + ThemeName + " = {\n";
    //
    //     for (int i = 0; i < result.Tables.Count; i++)
    //     {
    //         DataTable mTable = result.Tables[i];
    //         if (mTable.TableName == "Export Summary")
    //         {
    //             continue;
    //         }
    //
    //         Console.WriteLine("TableName: " + mTable.TableName);
    //
    //         outStr += "\t" + mTable.TableName + " = {\n";
    //
    //         for (int j = 4; j < mTable.Rows.Count; j++)
    //         {
    //             outStr += "\t\t{";
    //             for (int k = 0; k < mTable.Columns.Count; k++)
    //             {
    //                 string value = mTable.Rows[j][k].ToString();
    //
    //                 string fieldName = mListColumnTypeInfo[k * 4 + 0];
    //                 string desName = mListColumnTypeInfo[k * 4 + 1];
    //                 string typeName = mListColumnTypeInfo[k * 4 + 2];
    //                 string subtypeName = mListColumnTypeInfo[k * 4 + 3];
    //
    //                 if (fieldName == string.Empty || typeName == string.Empty)
    //                 {
    //                     continue;
    //                 }
    //
    //                 if (typeName == "FLOAT")
    //                 {
    //                     float fValue = float.Parse(value);
    //                     outStr += fieldName + " = " + fValue;
    //                 }
    //                 else if (typeName == "INT")
    //                 {
    //                     int nValue = int.Parse(value);
    //                     outStr += fieldName + " = " + nValue;
    //                 }
    //                 else if (typeName == "STRING")
    //                 {
    //                     string strValue = value;
    //                     outStr += fieldName + " = \"" + strValue + "\"";
    //                 }
    //                 else if (typeName == "TABLE")
    //                 {
    //                     string strValue = value;
    //                     if (strValue.StartsWith("{", StringComparison.Ordinal))
    //                     {
    //                         strValue = strValue.Substring(1, strValue.Length - 1);
    //                     }
    //
    //                     if (strValue.EndsWith("}", StringComparison.Ordinal))
    //                     {
    //                         strValue = strValue.Substring(0, strValue.Length - 1);
    //                     }
    //
    //                     string[] words = strValue.Split(',');
    //
    //                     outStr += fieldName + " = {";
    //                     for (int n = 0; n < words.Length; n++)
    //                     {
    //                         string tempvalue = words[n];
    //                         if (subtypeName == "FLOAT")
    //                         {
    //                             float fValue = float.Parse(tempvalue);
    //                             if (n == 0)
    //                             {
    //                                 outStr += fValue;
    //                             }
    //                             else
    //                             {
    //                                 outStr += ", " + fValue;
    //                             }
    //                         }
    //                         else if (subtypeName == "INT")
    //                         {
    //                             int nValue = int.Parse(tempvalue);
    //                             if (n == 0)
    //                             {
    //                                 outStr += nValue;
    //                             }
    //                             else
    //                             {
    //                                 outStr += ", " + nValue;
    //                             }
    //                         }
    //                         else if (subtypeName == "STRING")
    //                         {
    //                             if (n == 0)
    //                             {
    //                                 outStr += tempvalue;
    //                             }
    //                             else
    //                             {
    //                                 outStr += ", " + tempvalue;
    //                             }
    //                         }
    //                         else
    //                         {
    //                             Debug.Assert(false);
    //                         }
    //                     }
    //
    //                     outStr += "}";
    //                 }
    //                 else
    //                 {
    //                     Debug.Assert(false);
    //                 }
    //
    //                 if (k < mTable.Columns.Count - 1)
    //                 {
    //                     outStr += ",\t";
    //                 }
    //             }
    //
    //             outStr += "},\n";
    //         }
    //
    //         outStr += "\t},\n\n";
    //     }
    //
    //     outStr += "}\n\n";
    //
    //     outStr += "return " + ThemeName + "\n";
    //
    //     Console.WriteLine("outStr" + outStr);
    //
    //     String outFileName = outPath + ThemeName + ".lua";
    //     File.WriteAllText(outFileName, outStr);
    // }
    
    public string ConvertType(string oriType)
    {
        if (oriType.ToLower() == "Int32".ToLower())
        {
            return "int";
        }
        else if (oriType.ToLower() == "string".ToLower())
        {
            return "string";
        }
        else if (oriType.ToLower() == "Int32".ToLower())
        {
            
        }
        
        return string.Empty;
    }

}
