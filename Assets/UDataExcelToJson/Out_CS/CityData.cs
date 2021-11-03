using TableML;
using System.Collections.Generic;
using System.Collections;
using uData.DataTable;

namespace AAA
{

 

[TableFilePaths(new string[]{
         "CityData.json"
        },
        typeof(CityDataParser)
        )]
 public class CityData : IGameData
    {
        
          /// <summary>
          /// 城市id
          /// </summary>
        public int CityID { get; private set;}
        
          /// <summary>
          /// 城市名称
          /// </summary>
        public string CityName { get; private set;}
        
          /// <summary>
          /// 城市背景图
          /// </summary>
        public string CityBGIMG { get; private set;}
        
          /// <summary>
          /// 城市封面图
          /// </summary>
        public string CityIMG { get; private set;}
        
          /// <summary>
          /// 城市唱片色
          /// </summary>
        public int CityColor { get; private set;}
        
        
        public CityData(
        int _CityID
        ,string _CityName
        ,string _CityBGIMG
        ,string _CityIMG
        ,int _CityColor
        )               
        {
                
            this.CityID=_CityID;          
            this.CityName=_CityName;          
            this.CityBGIMG=_CityBGIMG;          
            this.CityIMG=_CityIMG;          
            this.CityColor=_CityColor;         
        }

        
    }



    
    public  class CityDataParser : TableRowFieldParser,IDataParser
    {      

        private CityData m_CityData;
        public IGameData GetData()
        {
           return m_CityData;
        }
        public void Reload(TableFileRow row)
        { 
            try
            {
                
                
                 int  _CityID = row.Get_int(row.Values[0], "");              
                
                 string  _CityName = row.Get_string(row.Values[1], "");              
                
                 string  _CityBGIMG = row.Get_string(row.Values[2], "");              
                
                 string  _CityIMG = row.Get_string(row.Values[3], "");              
                
                 int  _CityColor = row.Get_int(row.Values[4], "");              
                

                m_CityData = new CityData(
                
                _CityID 
                ,_CityName 
                ,_CityBGIMG 
                ,_CityIMG 
                ,_CityColor               
                );

            }
             catch (System.Exception ex)
            {
                 string str = string.Format("Excel Load Failure. DataRow : CityData.ID :{0}. ErrorMessage : {1}",Utility.ParsePrimaryKey(row),ex.ToString()); 
                 throw new System.Exception(str);
            }

        }
    }

   
  
}