using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public static class ResistanceStateTypeExtensions
	{
		public static int ToKey(this ResistanceStatType stat)
		{
			return (int)stat;
		}
	}
}
