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
 public class ConditionData
    {
        
          /// <summary>
          /// 条件id
          /// </summary>
        public Int32 ConditionID { get; private set;}
        
          /// <summary>
          /// 条件名称
          /// </summary>
        public String NconditionName { get; private set;}
        
          /// <summary>
          /// 条件标题
          /// </summary>
        public String NconditionTitle { get; private set;}
        
          /// <summary>
          /// 条件符号
          /// </summary>
        public String Img { get; private set;}
        
        
        public ConditionData(
        Int32 _ConditionID
        ,String _NconditionName
        ,String _NconditionTitle
        ,String _Img
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
                
                
                 Int32  _ConditionID = row.Get_Int32(row.Values[0], "");              
                
                 String  _NconditionName = row.Get_String(row.Values[1], "");              
                
                 String  _NconditionTitle = row.Get_String(row.Values[2], "");              
                
                 String  _Img = row.Get_String(row.Values[3], "");              
                

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