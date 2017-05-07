using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public static class CombatStatTypeExtensions
	{
		public static int ToKey(this CombatStatType stat)
		{
			return (int)stat;
		}
	}
}
