using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class ConditionData : DbBase
	{
		/// <summary>
		/// 条件id
		/// </summary>
		public readonly int ConditionID;
		/// <summary>
		/// 条件名称
		/// </summary>
		public readonly string NconditionName;
		/// <summary>
		/// 条件标题
		/// </summary>
		public readonly string NconditionTitle;
		/// <summary>
		/// 条件符号
		/// </summary>
		public readonly string Img;
	}

}