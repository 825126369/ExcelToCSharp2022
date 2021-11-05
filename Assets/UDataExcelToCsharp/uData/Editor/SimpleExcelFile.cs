using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

namespace uData
{
    /// <summary>
    /// 读取带有头部、声明和注释的文件表格
    /// </summary>
    public interface ITableSourceFile
    {
        Dictionary<string, int> ColName2Index { get; set; }
        Dictionary<int, string> Index2ColName { get; set; }
        Dictionary<string, string> ColName2Statement { get; set; }//  string,or something
        Dictionary<string, string> ColName2Comment { get; set; }// string comment
        int GetRowsCount();
        int GetColumnCount();
        string GetString(string columnName, int row);
    }
    
    /// <summary>
    /// TSV格式的支持
    /// </summary>
    public class SimpleTSVFile : ITableSourceFile
    {
        public Dictionary<string, int> ColName2Index { get; set; }
        public Dictionary<int, string> Index2ColName { get; set; }
        public Dictionary<string, string> ColName2Statement { get; set; }
        public Dictionary<string, string> ColName2Comment { get; set; }
        
        private TableFile _tableFile;
        private int _columnCount;
        public SimpleTSVFile(string filePath)
        {
            ColName2Index = new Dictionary<string, int>();
            Index2ColName = new Dictionary<int, string>();
            ColName2Statement = new Dictionary<string, string>();
            ColName2Comment = new Dictionary<string, string>();
            try
            {
                ParseTsv(filePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error TSV File: " + filePath + " / " + e.Message);
            }
        }
    
        private void ParseTsv(string filePath)
        {
            _tableFile = TableFile.LoadFromFile(filePath, Encoding.GetEncoding("GBK"));
            _columnCount = _tableFile.GetColumnCount();
    
    
            // 通过TableFile注册头信息
            var commentRow = _tableFile.GetRow(1);
            foreach (var kv in _tableFile.Headers)
            {
                var header = kv.Value;
                ColName2Index[header.HeaderName] = header.ColumnIndex;
                Index2ColName[header.ColumnIndex] = header.HeaderName;
                ColName2Statement[header.HeaderName] = header.HeaderMeta;
                ColName2Comment[header.HeaderName] = commentRow[header.ColumnIndex];
            }
        }
        public int GetRowsCount()
        {
            return _tableFile.GetRowCount() - 1; // 减去注释行
        }
    
        public int GetColumnCount()
        {
            return _columnCount;
        }
    
        public string GetString(string columnName, int dataRow)
        {
            return _tableFile.GetRow(dataRow + 1 + 1)[columnName]; // 1行开始，并且多了说明行，+2
        }
    }
    
    /// <summary>
    /// 简单的NPOI Excel封装, 支持xls, xlsx 和 tsv
    /// 带有头部、声明、注释
    /// </summary>
    public class SimpleExcelFile : ITableSourceFile
    {
        //private Workbook Workbook_;
        //private Worksheet Worksheet_;
        public Dictionary<string, int> ColName2Index { get; set; }
        public Dictionary<int, string> Index2ColName { get; set; }
        public Dictionary<string, string> ColName2Statement { get; set; } //  string,or something
        public Dictionary<string, string> ColName2Comment { get; set; } // string comment
        
        /// <summary>
        /// Header, Statement, Comment, at lease 3 rows
        /// 预留行数
        /// </summary>
        private const int PreserverRowCount = 3;
        
        //private DataTable DataTable_;
        private string Path;
        private DataSet Workbook;
        private DataTable Worksheet;
        //private TableFile _tableFile;
        //public bool IsLoadSuccess = true;
        private int _columnCount;

        public SimpleExcelFile(string excelPath)
        {
            Path = excelPath;
            ColName2Index = new Dictionary<string, int>();
            Index2ColName = new Dictionary<int, string>();
            ColName2Statement = new Dictionary<string, string>();
            ColName2Comment = new Dictionary<string, string>();
            
            ParseExcel(excelPath);
        }
        
        /// <summary>
        /// Parse Excel file to data grid
        /// </summary>
        /// <param name="filePath"></param>
        private void ParseExcel(string filePath)
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
                    Workbook = result;
                }
            }
            
            //if (IsLoadSuccess)
            {
                if (Workbook == null)
                {
                    Debug.LogError("Null Workbook");
                    return;
                }

                //var dt = new DataTable();

                Worksheet = Workbook.Tables[0];
                if (Worksheet == null)
                {
                    Debug.LogError("Null Worksheet");
                    return;
                }
                
                var sheetRowCount = GetWorksheetCount();
                if (sheetRowCount < PreserverRowCount)
                {
                    Debug.LogError(string.Format("At lease {0} rows of this excel", sheetRowCount));
                    return;
                }
                
                // 列头名
                DataRow headerRow = Worksheet.Rows[1];
                // 列总数保存
                int columnCount = _columnCount = Worksheet.Columns.Count;
                
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var cell = headerRow[columnIndex];
                    var headerName = cell != null ? cell.ToString().Trim() : ""; // trim!
                    ColName2Index[headerName] = columnIndex;
                    Index2ColName[columnIndex] = headerName;
                }
                // 表头声明
                var statementRow = Worksheet.Rows[2];
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var colName = Index2ColName[columnIndex];
                    var statementCell = statementRow[columnIndex];
                    var statementString = statementCell != null ? statementCell.ToString() : "";
                    ColName2Statement[colName] = statementString;
                }
                // 表头注释
                var commentRow = Worksheet.Rows[0];
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var colName = Index2ColName[columnIndex];
                    var commentCell = commentRow[columnIndex];
                    var commentString =  commentCell != null ? commentCell.ToString() : "";
                    ColName2Comment[colName] = commentString;
                }
            }
        }
        /// <summary>
        /// 是否存在列名
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool HasColumn(string columnName)
        {
            return ColName2Index.ContainsKey(columnName);
        }

        public float GetFloat(string columnName, int row)
        {
            return float.Parse(GetString(columnName, row));
        }

        public int GetInt(string columnName, int row)
        {
            return int.Parse(GetString(columnName, row));
        }

        /// <summary>
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataRow">无计算表头的数据行数</param>
        /// <returns></returns>
        public string GetString(string columnName, int dataRow)
        {
            dataRow += PreserverRowCount;
                
            var theRow = Worksheet.Rows[dataRow];
            var colIndex = ColName2Index[columnName];
            var cell = theRow[colIndex];
            // if (cell != null)
            // {
            //     if (cell.CellType == CellType.Formula)
            //         return cell.StringCellValue;
            //     if (cell.CellType == CellType.String)
            //         return cell.StringCellValue;
            //     if (cell.CellType == CellType.Boolean)
            //         return cell.BooleanCellValue ? "1" : "0";
            //     if (cell.CellType == CellType.Numeric)
            //         return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
            // }
            
            return cell.ToString();
        }

        /// <summary>
        /// 不带3个预留头的数据总行数
        /// </summary>
        /// <returns></returns>
        public int GetRowsCount()
        {
            return GetWorksheetCount() - PreserverRowCount;
        }
        
        /// <summary>
        /// 工作表的总行数
        /// </summary>
        /// <returns></returns>
        private int GetWorksheetCount()
        {
            if (Worksheet != null)
            {
                return Worksheet.Rows.Count;
            }

            return -1;
        }

        // private ICellStyle GreyCellStyleCache;
        //
        // public void SetRowGrey(int row)
        // {
        //     var theRow = Worksheet.Rows[row];
        //     foreach (var cell in theRow.ItemArray)
        //     {
        //         if (GreyCellStyleCache == null)
        //         {
        //             var newStyle = Workbook.CreateCellStyle();
        //             newStyle.CloneStyleFrom(cell.CellStyle);
        //             //newStyle.FillBackgroundColor = colorIndex;
        //             newStyle.FillPattern = FillPattern.Diamonds;
        //             GreyCellStyleCache = newStyle;
        //         }
        //
        //         cell.CellStyle = GreyCellStyleCache;
        //     }
        // }

        /// <summary>
        /// 获取列总数
        /// </summary>
        /// <returns></returns>
        public int GetColumnCount()
        {
            return _columnCount;
        }
    }
}