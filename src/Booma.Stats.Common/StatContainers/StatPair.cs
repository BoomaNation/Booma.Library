using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public struct StatPair<TStateType>
		where TStateType : struct, IConvertible
	{
		TStateType StatType { get; }

		int Value { get; }

		public StatPair(TStateType statType, int value)
		{
#if DEBUG || DEBUGBUILD
			if (!Enum.IsDefined(typeof(TStateType), statType))
				throw new ArgumentException($"Invalid {nameof(TStateType)} value given: {Enum.ToObject(typeof(TStateType), statType)}", nameof(statType));
#endif
			StatType = statType;
			Value = value;
		}
	}
}
