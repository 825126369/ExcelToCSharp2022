using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class LevelData : DbBase
	{
		/// <summary>
		/// 关卡id
		/// </summary>
		public readonly int LevelID;
		/// <summary>
		/// 关卡所属城市
		/// </summary>
		public readonly int CityID;
		/// <summary>
		/// 关卡初始状态
		/// </summary>
		public readonly int LevelState;
		/// <summary>
		/// 剧情表id
		/// </summary>
		public readonly int PlotContentID;
		/// <summary>
		/// 解锁的下一关卡
		/// </summary>
		public readonly int UnlockingLevelID;
		/// <summary>
		/// 歌曲名称
		/// </summary>
		public readonly int MusicList;
		/// <summary>
		/// 歌谱名称
		/// </summary>
		public readonly int MusicSorce;
	}

}