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
 public class LevelCondition : IGameData
    {
        
          /// <summary>
          /// 条件id
          /// </summary>
        public int ConditionID { get; private set;}
        
          /// <summary>
          /// 关卡达成条件
          /// </summary>
        public int[] Condition { get; private set;}
        
          /// <summary>
          /// 达成条件数量
          /// </summary>
        public int[] ConditionNumber { get; private set;}
        
        
        public LevelCondition(
        int _ConditionID
        ,int[] _Condition
        ,int[] _ConditionNumber
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
                
                
                 int  _ConditionID = row.Get_int(row.Values[0], "");              
                
                 int[]  _Condition = row.Get_int_array(row.Values[1], "");              
                
                 int[]  _ConditionNumber = row.Get_int_array(row.Values[2], "");              
                

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