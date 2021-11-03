using TableML;
using System.Collections.Generic;
using System.Collections;
using uData.DataTable;

namespace AAA
{

 

[TableFilePaths(new string[]{
         "LevelCondition.json"
        },
        typeof(LevelConditionParser)
        )]
 public class LevelCondition
    {
        
          /// <summary>
          /// 条件id
          /// </summary>
        public int32 ConditionID { get; private set;}
        
          /// <summary>
          /// 关卡达成条件
          /// </summary>
        public Int32[] Condition { get; private set;}
        
          /// <summary>
          /// 达成条件数量
          /// </summary>
        public Int32[] ConditionNumber { get; private set;}
        
        
        public LevelCondition(
        int32 _ConditionID
        ,Int32[] _Condition
        ,Int32[] _ConditionNumber
        )               
        {
                
            this.ConditionID=_ConditionID;          
            this.Condition=_Condition;          
            this.ConditionNumber=_ConditionNumber;         
        }

        
    }



    
    public  class LevelConditionParser : TableRowFieldParser,IDataParser
    {      

        private LevelCondition m_LevelCondition;
        public IGameData GetData()
        {
           return m_LevelCondition;
        }
        public void Reload(TableFileRow row)
        { 
            try
            {
                
                
                 int32  _ConditionID = row.Get_int32(row.Values[0], "");              
                
                 Int32[]  _Condition = row.Get_Int32_array(row.Values[1], "");              
                
                 Int32[]  _ConditionNumber = row.Get_Int32_array(row.Values[2], "");              
                

                m_LevelCondition = new LevelCondition(
                
                _ConditionID 
                ,_Condition 
                ,_ConditionNumber               
                );

            }
             catch (System.Exception ex)
            {
                 string str = string.Format("Excel Load Failure. DataRow : LevelCondition.ID :{0}. ErrorMessage : {1}",Utility.ParsePrimaryKey(row),ex.ToString()); 
                 throw new System.Exception(str);
            }

        }
    }

   
  
}