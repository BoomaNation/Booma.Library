using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public static class TransientStatTypeExtensions
	{
		public static int ToKey(this TransientStatType stat)
		{
			return (int)stat;
		}
	}
}
