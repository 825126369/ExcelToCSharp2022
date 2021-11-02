using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class Sheet1:ExcelDbBase
	{
		/// <summary>
		/// 条件id
		/// </summary>
		public readonly int32 ConditionID;
		/// <summary>
		/// 关卡达成条件
		/// </summary>
		public readonly List<Int32> Condition=new List<Int32>();
		/// <summary>
		/// 达成条件数量
		/// </summary>
		public readonly List<Int32> ConditionNumber=new List<Int32>();
	}

}