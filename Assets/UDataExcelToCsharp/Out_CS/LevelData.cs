using uData;
using System.Collections.Generic;
using System.Collections;

namespace s7u.dtb.exceldata
{

 

[TableFilePaths(new string[]{
         "LevelData.txt"
        },
        typeof(LevelDataParser)
        )]
 public class LevelData : IGameData
    {
        
          /// <summary>
          /// 序号Id
          /// </summary>
        public int Id { get; private set;}
        
          /// <summary>
          /// 关卡id
          /// </summary>
        public int LevelID { get; private set;}
        
          /// <summary>
          /// 关卡所属城市
          /// </summary>
        public int CityID { get; private set;}
        
          /// <summary>
          /// 关卡初始状态
          /// </summary>
        public int LevelState { get; private set;}
        
          /// <summary>
          /// 剧情表id
          /// </summary>
        public int PlotContentID { get; private set;}
        
          /// <summary>
          /// 解锁的下一关卡
          /// </summary>
        public int[] UnlockingLevelID { get; private set;}
        
          /// <summary>
          /// 歌曲名称
          /// </summary>
        public int MusicList { get; private set;}
        
          /// <summary>
          /// 歌谱名称
          /// </summary>
        public int MusicSorce { get; private set;}
        
          /// <summary>
          /// 关卡背景图
          /// </summary>
        public string LevelBGimg { get; private set;}
        
        
        public LevelData(
        int _Id
        ,int _LevelID
        ,int _CityID
        ,int _LevelState
        ,int _PlotContentID
        ,int[] _UnlockingLevelID
        ,int _MusicList
        ,int _MusicSorce
        ,string _LevelBGimg
        )               
        {
                
            this.Id=_Id;          
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
                
                
                 int  _Id = row.Get_int(row.Values[0], "");              
                
                 int  _LevelID = row.Get_int(row.Values[1], "");              
                
                 int  _CityID = row.Get_int(row.Values[2], "");              
                
                 int  _LevelState = row.Get_int(row.Values[3], "");              
                
                 int  _PlotContentID = row.Get_int(row.Values[4], "");              
                
                 int[]  _UnlockingLevelID = row.Get_int_array(row.Values[5], "");              
                
                 int  _MusicList = row.Get_int(row.Values[6], "");              
                
                 int  _MusicSorce = row.Get_int(row.Values[7], "");              
                
                 string  _LevelBGimg = row.Get_string(row.Values[8], "");              
                

                m_LevelData = new LevelData(
                
                _Id 
                ,_LevelID 
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