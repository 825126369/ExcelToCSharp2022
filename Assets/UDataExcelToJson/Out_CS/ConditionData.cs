using TableML;
using System.Collections.Generic;
using System.Collections;
using uData.DataTable;

namespace AAA
{

 

[TableFilePaths(new string[]{
         "ConditionData.json"
        },
        typeof(ConditionDataParser)
        )]
 public class ConditionData : IGameData
    {
        
          /// <summary>
          /// 条件id
          /// </summary>
        public int ConditionID { get; private set;}
        
          /// <summary>
          /// 条件名称
          /// </summary>
        public string NconditionName { get; private set;}
        
          /// <summary>
          /// 条件标题
          /// </summary>
        public string NconditionTitle { get; private set;}
        
          /// <summary>
          /// 条件符号
          /// </summary>
        public string Img { get; private set;}
        
        
        public ConditionData(
        int _ConditionID
        ,string _NconditionName
        ,string _NconditionTitle
        ,string _Img
        )               
        {
                
            this.ConditionID=_ConditionID;          
            this.NconditionName=_NconditionName;          
            this.NconditionTitle=_NconditionTitle;          
            this.Img=_Img;         
        }

        
    }



    
    public  class ConditionDataParser : TableRowFieldParser,IDataParser
    {      

        private ConditionData m_ConditionData;
        public IGameData GetData()
        {
           return m_ConditionData;
        }
        public void Reload(TableFileRow row)
        { 
            try
            {
                
                
                 int  _ConditionID = row.Get_int(row.Values[0], "");              
                
                 string  _NconditionName = row.Get_string(row.Values[1], "");              
                
                 string  _NconditionTitle = row.Get_string(row.Values[2], "");              
                
                 string  _Img = row.Get_string(row.Values[3], "");              
                

                m_ConditionData = new ConditionData(
                
                _ConditionID 
                ,_NconditionName 
                ,_NconditionTitle 
                ,_Img               
                );

            }
             catch (System.Exception ex)
            {
                 string str = string.Format("Excel Load Failure. DataRow : ConditionData.ID :{0}. ErrorMessage : {1}",Utility.ParsePrimaryKey(row),ex.ToString()); 
                 throw new System.Exception(str);
            }

        }
    }

   
  
}