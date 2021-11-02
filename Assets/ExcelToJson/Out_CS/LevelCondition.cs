using System.Collections;
using System.Collections.Generic;
namespace s7u.dtb.exceldata
{
	public class LevelConditionSheet1 : DbBase
	{
		/// <summary>
		/// 条件id
		/// </summary>
		public readonly int ConditionID;
		/// <summary>
		/// 关卡达成条件
		/// </summary>
		public readonly List<int> Condition=new List<int>();
		/// <summary>
		/// 达成条件数量
		/// </summary>
		public readonly List<int> ConditionNumber=new List<int>();
	}

}