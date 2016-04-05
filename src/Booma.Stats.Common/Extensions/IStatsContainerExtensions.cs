using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	internal static class IStatsContainerExtensions
	{
		internal static int GetMapSize<TStatType>(this IStatsContainer<TStatType> statsContainer)
			where TStatType : struct, IConvertible
		{
			return GetMapSize<TStatType>();
		}

		internal static int GetMapSize<TStatType>()
			where TStatType : struct, IConvertible
		{
			//Gets the values of the enum in the container, casts them to int and then finds the max value
			//of the collection to get max value size
			return ((IEnumerable<TStatType>)Enum.GetValues(typeof(TStatType))).Cast<int>().Max();
		}
	}
}
