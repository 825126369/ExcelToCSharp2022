using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class ConditionData:ExcelDbBase
	{
		/// <summary>
		/// 条件id
		/// </summary>
		public readonly Int32 ConditionID;
		/// <summary>
		/// 条件名称
		/// </summary>
		public readonly String NconditionName;
		/// <summary>
		/// 条件标题
		/// </summary>
		public readonly String NconditionTitle;
		/// <summary>
		/// 条件符号
		/// </summary>
		public readonly String Img;
	}

}