using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class Sheet1:ExcelDbBase
	{
		/// <summary>
		/// 城市id
		/// </summary>
		public readonly int32 CityID;
		/// <summary>
		/// 城市名称
		/// </summary>
		public readonly string CityName;
		/// <summary>
		/// 城市背景图
		/// </summary>
		public readonly string CityBGIMG;
		/// <summary>
		/// 城市封面图
		/// </summary>
		public readonly string CityIMG;
		/// <summary>
		/// 城市唱片色
		/// </summary>
		public readonly int32 CityColor;
	}

}