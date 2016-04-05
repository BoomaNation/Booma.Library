using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Base-class type for Immutable stats container types.
	/// </summary>
	/// <typeparam name="TStatType"></typeparam>
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
		/// Creates a partially initialized immutable container for stats.
		/// </summary>
		/// <param name="values">Values key-store for <see cref="TStatType"/>.</param>
		public ImmutableStatsContainer(IDictionary<TStatType, int> values)
		{
			//If it's empty then we don't need to do anything fancy
			if (values.Count <= 0)
			{
				//empty container
				_statsMap = new int?[0];
				return;
			}
			else
				_statsMap = new int?[maxMapSize];


			//Set each keypair to be in the flat cache-quick array of nullable ints
			foreach (var kvp in values.AsEnumerable())
			{
				//map to the enum int codes
				_statsMap[ConvertStatToKey(kvp.Key)] = kvp.Value;
			}
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
		/// <summary>
		/// Readonly index accessor that takes in <typeparamref name="TStateType"/>s
		/// and returns the contained value. Does not throw if the container doesn't contain
		/// <paramref name="statIndex"/>; returns null instead.
		/// </summary>
		/// <param name="statIndex">The state type to query the container for.</param>
		/// <returns>The corresponding value for the <paramref name="statIndex"/> or null if the container doesn't contain it.</returns>
		public int? this[TStatType statIndex]
		{
			get
			{
				//An ugly nested ternary but basically if it's within bounds we'll check to see if there is a value
				//If there is then we provide it otherwise we provide null
				//This is as expected, it's not in the collection then the caller recieves null.
				return isWithinBounds(statIndex) ?
					(_statsMap[ConvertStatToKey(statIndex)].HasValue ? (int?)_statsMap[ConvertStatToKey(statIndex)].Value : null)
					: null;
			}
		}

		//the reason we don't implement this generically is because this would cause boxing due to the inability to simply cast from enum to int
		/// <summary>
		/// Indicates if the container has a value for the given <paramref name="statType"/>
		/// </summary>
		/// <param name="statType">The <typeparamref name="TStatType"/> to check the contained status of.</param>
		/// <returns>True if the stat is in the container.</returns>
		public bool Contains(TStatType statType)
		{
			//Gets the key value (int) and checks the length and if there is a value in the map
			return isWithinBounds(statType) && _statsMap[ConvertStatToKey(statType)].HasValue;
		}

		/// <summary>
		/// Method should convert the <paramref name="statType"/> to a key-value; implementation is
		/// deferred to implementing concrete (non-generic) types.
		/// </summary>
		/// <param name="statType">The stat type to convert to int.</param>
		/// <returns>A key value for the given <paramref name="statType"/>.</returns>
		protected abstract int ConvertStatToKey(TStatType statType);

		private bool isWithinBounds(TStatType statType)
		{
			return ConvertStatToKey(statType) > -1 && ConvertStatToKey(statType) < statsMap.Count();
		}
	}
}
