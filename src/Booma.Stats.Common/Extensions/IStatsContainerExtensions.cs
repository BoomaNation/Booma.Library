using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public static class IStatsContainerExtensions
	{
		public static int GetMaxMapKeyValue<TStatType>(this IStatsContainer<TStatType> statsContainer)
			where TStatType : struct, IConvertible
		{
			return GetMaxMapKeyValue<TStatType>();
		}

		internal static int GetMaxMapKeyValue<TStatType>()
			where TStatType : struct, IConvertible
		{
			//Gets the values of the enum in the container, casts them to int and then finds the max value
			//of the collection to get max value size
			return ((IEnumerable<TStatType>)Enum.GetValues(typeof(TStatType))).Cast<int>().Max();
		}
	}
}
