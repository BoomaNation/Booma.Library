using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public abstract class ImmutableStatsContainer<TStatType> : IStatsContainer<TStatType>
		where TStatType : struct, IConvertible
	{
		//Not really how I wanted to compute the value/store the value... But it's the most efficient and thread safe way.
		protected static readonly int maxMapSize = IStatsContainerExtensions.GetMapSize<TStatType>();

		//for better caching we don't use a dictionary; use a flat array.
		//Hopefully this doesn't cause GC pressure casting enum to int... I should read the language specs
		private readonly int?[] _statsMap;
		
		//We can use an IEnumerable because O(1) for arrays (since array implements IList magically)
		protected IEnumerable<int?> statsMap
		{
			get { return _statsMap; }
		} 

		/// <summary>
		/// Creates an empty-immutable container for stats.
		/// </summary>
		public ImmutableStatsContainer()
		{
			//We need a new array of atleast the size of the largest value in CombatStatType
			_statsMap = new int?[maxMapSize];
		}

		//the reason we don't implement this generically is because this would cause boxing due to the inability to simply cast from enum to int
		public abstract int? this[TStatType statIndex]
		{
			get;
		}

		//the reason we don't implement this generically is because this would cause boxing due to the inability to simply cast from enum to int
		public abstract bool Contains(TStatType statType);
	}
}
