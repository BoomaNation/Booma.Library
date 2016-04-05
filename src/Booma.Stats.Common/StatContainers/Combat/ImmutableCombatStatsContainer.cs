using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this class isn't generic, and trust me I have the wizard skills to create such an amalgation,
	//is because I expect the ICombatStatsContainer to eventually have functionality different from resistence
	//Either explictly or maybe through extensions (though this could be supported without the marker interface)
	/// <summary>
	/// Immutable, readonly implementation of the <see cref="ICombatStatsContainer"/> interface/contract.
	/// </summary>
	public class ImmutableCombatStatsContainer : ICombatStatsContainer
	{
		//Not really how I wanted to compute the value/store the value... But it's the most efficient and thread safe way.
		private static readonly int maxMapSize = IStatsContainerExtensions.GetMapSize<CombatStatType>();

		/// <summary>
		/// Readonly index accessor that takes in <see cref="CombatStatType"/>
		/// and returns the contained value. Does not throw if the container doesn't contain
		/// <paramref name="statIndex"/>; returns null instead.
		/// </summary>
		/// <param name="statIndex">The state type to query the container for.</param>
		/// <returns>The corresponding value for the <paramref name="statIndex"/> or null if the container doesn't contain it.</returns>
		public Nullable<int> this[CombatStatType statIndex]
		{
			get
			{
				//An ugly nested ternary but basically if it's within bounds we'll check to see if there is a value
				//If there is then we provide it otherwise we provide null
				//This is as expected, it's not in the collection then the caller recieves null.
				return isWithinBounds(statIndex) ? 
					(statsMap[statIndex.ToKey()].HasValue ? (int?)statsMap[statIndex.ToKey()].Value : null) 
					: null;
			}
		}

		//for better caching we don't use a dictionary; use a flat array.
		//Hopefully this doesn't cause GC pressure casting enum to int... I should read the language specs
		private readonly int?[] statsMap;

		/// <summary>
		/// Creates an empty-immutable container for stats.
		/// </summary>
		public ImmutableCombatStatsContainer()
		{
			//We need a new array of atleast the size of the largest value in CombatStatType
			statsMap = new int?[maxMapSize];
		}

		/// <summary>
		/// Indicates if the container has a value for the given <see cref="CombatStatType"/>
		/// </summary>
		/// <param name="statType">The <typeparamref name="TStatType"/> to check the contained status of.</param>
		/// <returns>True if the stat is in the container.</returns>
		public bool Contains(CombatStatType statType)
		{
			//Gets the key value (int) and checks the length and if there is a value in the map
			return isWithinBounds(statType) && statsMap[statType.ToKey()].HasValue;
		}

		private bool isWithinBounds(CombatStatType statType)
		{
			return statType.ToKey() > -1 && statType.ToKey() < statsMap.Length;
		}
	}
}
