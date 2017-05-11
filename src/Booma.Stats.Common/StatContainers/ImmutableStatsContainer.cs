using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Base-class type for Immutable stats container types.
	/// </summary>
	/// <typeparam name="TStatType">The stat type the container is tracking</typeparam>
	public class ImmutableStatsContainer<TStatType> : IStatsContainer<TStatType>
		where TStatType : struct, IConvertible
	{
		//This allows this class not to depend on external stat provider implementations
		//We can make assumptions and optimize if needed (though that's not occuring here yet)
		/// <summary>
		/// Default provider we use if the internal container isn't provided with only int values.
		/// </summary>
		protected class ImmutableStatsContainerDefaultStatProvider : IStatProvider<TStatType>
		{
			/// <summary>
			/// Wrapped <see cref="KeyValuePair{TKey,TValue}"/> that contains the values provided through the
			/// <see cref="IStatProvider{TStatType}"/> interface.
			/// </summary>
			private KeyValuePair<TStatType, int> internalManagedKeyValuePair { get; }

			/// <inheritdoc />
			public TStatType StatType => internalManagedKeyValuePair.Key;

			/// <inheritdoc />
			public int Value => internalManagedKeyValuePair.Value;

			/// <summary>
			/// Creates a new wrapper around the <see cref="KeyValuePair{TKey,TValue}"/> to fit the
			/// <see cref="IStatProvider{TStatType}"/> interface.
			/// </summary>
			/// <param name="kvp">The <see cref="KeyValuePair{TKey,TValue}"/> to wrap around.</param>
			public ImmutableStatsContainerDefaultStatProvider(KeyValuePair<TStatType, int> kvp)
			{
				internalManagedKeyValuePair = kvp;
			}
		}

		//Not really how I wanted to compute the value/store the value... But it's the most efficient and thread safe way.
		//This value represents the maximum key value that the enum contains. This can help create a properly sized array for the container
		protected static readonly int maxMapKeyValue = IStatsContainerExtensions.GetMaxMapKeyValue<TStatType>();

		//for better caching we don't use a dictionary; use a flat array.
		//Hopefully this doesn't cause GC pressure casting enum to int... I should read the language specs
		private readonly IStatProvider<TStatType>[] _statsMap;
		
		//We can use an IEnumerable because O(1) for arrays (since array implements IList magically)
		protected IEnumerable<IStatProvider<TStatType>> statsMap => _statsMap;

		/// <summary>
		/// Creates a partially initialized immutable container for stats.
		/// </summary>
		/// <param name="values">Values key-store for <see cref="TStatType"/>.</param>
		public ImmutableStatsContainer(IDictionary<TStatType, int> values)
			: this()
		{
			if (values == null) throw new ArgumentNullException(nameof(values));

			//Set each keypair to be in the flat cache-quick array of nullable ints
			//map to the enum int codes
			values.ToList().ForEach(kvp => _statsMap[ConvertStatToKey(kvp.Key)] = new ImmutableStatsContainerDefaultStatProvider(kvp));
		}

		/// <summary>
		/// Creates an empty-immutable container for stats.
		/// </summary>
		public ImmutableStatsContainer()
		{
			//We need a new array of atleast the size of the largest value in CombatStatType
			_statsMap = new IStatProvider<TStatType>[maxMapKeyValue + 1];
		}


		//An ugly nested ternary but basically if it's within bounds we'll check to see if there is a value
		//If there is then we provide it otherwise we provide null
		//This is as expected, it's not in the collection then the caller recieves null.
		//the reason we don't implement this generically is because this would cause boxing due to the inability to simply cast from enum to int
		/// <summary>
		/// Readonly index accessor that takes in <typeparamref name="TStateType"/>s
		/// and returns the contained value with proper units. Does not throw if the container doesn't contain
		/// <paramref name="statIndex"/>; returns null instead.
		/// </summary>
		/// <param name="statIndex">The state type to query the container for.</param>
		/// <returns>The corresponding value with units for the <paramref name="statIndex"/> or null if the container doesn't contain it.</returns>
		public IStatProvider<TStatType> this[TStatType statIndex] => isWithinBounds(statIndex) ? _statsMap[ConvertStatToKey(statIndex)] : null;

		//the reason we don't implement this generically is because this would cause boxing due to the inability to simply cast from enum to int
		/// <summary>
		/// Indicates if the container has a value for the given <paramref name="statType"/>
		/// </summary>
		/// <param name="statType">The <typeparamref name="TStatType"/> to check the contained status of.</param>
		/// <returns>True if the stat is in the container.</returns>
		public bool Contains(TStatType statType)
		{
			//Gets the key value (int) and checks the length and if there is a value in the map
			return isWithinBounds(statType) && _statsMap[ConvertStatToKey(statType)] != null;
		}

		/// <summary>
		/// Method should convert the <paramref name="statType"/> to a key-value; implementation is
		/// deferred to implementing concrete (non-generic) types.
		/// </summary>
		/// <param name="statType">The stat type to convert to int.</param>
		/// <returns>A key value for the given <paramref name="statType"/>.</returns>
		protected int ConvertStatToKey(TStatType statType)
		{
			return Operator.Convert<TStatType, int>(statType);
		}

		//Basically this is a more efficient version of Enum.IsBlahBlah
		/// <summary>
		/// Indicates if the provided stat type value is within the known bounds.
		/// </summary>
		/// <param name="statType">Stat type value.</param>
		/// <returns>True if it's within the bounds.</returns>
		private bool isWithinBounds(TStatType statType)
		{
			return ConvertStatToKey(statType) > -1 && ConvertStatToKey(statType) < statsMap.Count();
		}
	}
}
