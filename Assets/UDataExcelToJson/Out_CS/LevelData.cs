using TableML;
using System.Collections.Generic;
using System.Collections;
using uData.DataTable;

namespace AAA
{

 

[TableFilePaths(new string[]{
         "LevelData.json"
        },
        typeof(LevelDataParser)
        )]
 public class LevelData
    {
        
          /// <summary>
          /// 关卡id
          /// </summary>
        public Int32 LevelID { get; private set;}
        
          /// <summary>
          /// 关卡所属城市
          /// </summary>
        public Int32 CityID { get; private set;}
        
          /// <summary>
          /// 关卡初始状态
          /// </summary>
        public Int32 LevelState { get; private set;}
        
          /// <summary>
          /// 剧情表id
          /// </summary>
        public Int32 PlotContentID { get; private set;}
        
          /// <summary>
          /// 解锁的下一关卡
          /// </summary>
        public Int32 UnlockingLevelID { get; private set;}
        
          /// <summary>
          /// 歌曲名称
          /// </summary>
        public Int32 MusicList { get; private set;}
        
          /// <summary>
          /// 歌谱名称
          /// </summary>
        public Int32 MusicSorce { get; private set;}
        
          /// <summary>
          /// 关卡背景图
          /// </summary>
        public string LevelBGimg { get; private set;}
        
        
        public LevelData(
        Int32 _LevelID
        ,Int32 _CityID
        ,Int32 _LevelState
        ,Int32 _PlotContentID
        ,Int32 _UnlockingLevelID
        ,Int32 _MusicList
        ,Int32 _MusicSorce
        ,string _LevelBGimg
        )               
        {
                
            this.LevelID=_LevelID;          
            this.CityID=_CityID;          
            this.LevelState=_LevelState;          
            this.PlotContentID=_PlotContentID;          
            this.UnlockingLevelID=_UnlockingLevelID;          
            this.MusicList=_MusicList;          
            this.MusicSorce=_MusicSorce;          
            this.LevelBGimg=_LevelBGimg;         
        }

        
    }



    
    public  class LevelDataParser : TableRowFieldParser,IDataParser
    {      

        private LevelData m_LevelData;
        public IGameData GetData()
        {
           return m_LevelData;
        }
        public void Reload(TableFileRow row)
        { 
            try
            {
                
                
                 Int32  _LevelID = row.Get_Int32(row.Values[0], "");              
                
                 Int32  _CityID = row.Get_Int32(row.Values[1], "");              
                
                 Int32  _LevelState = row.Get_Int32(row.Values[2], "");              
                
                 Int32  _PlotContentID = row.Get_Int32(row.Values[3], "");              
                
                 Int32  _UnlockingLevelID = row.Get_Int32(row.Values[4], "");              
                
                 Int32  _MusicList = row.Get_Int32(row.Values[5], "");              
                
                 Int32  _MusicSorce = row.Get_Int32(row.Values[6], "");              
                
                 string  _LevelBGimg = row.Get_string(row.Values[7], "");              
                

                m_LevelData = new LevelData(
                
                _LevelID 
                ,_CityID 
                ,_LevelState 
                ,_PlotContentID 
                ,_UnlockingLevelID 
                ,_MusicList 
                ,_MusicSorce 
                ,_LevelBGimg               
                );

            }
             catch (System.Exception ex)
            {
                 string str = string.Format("Excel Load Failure. DataRow : LevelData.ID :{0}. ErrorMessage : {1}",Utility.ParsePrimaryKey(row),ex.ToString()); 
                 throw new System.Exception(str);
            }

        }
    }

   
  
}