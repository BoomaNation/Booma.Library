using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this class isn't generic, and trust me I have the wizard skills to create such an amalgation,
	//is because I expect the IResistanceStatsContainer to eventually have functionality different from resistence
	//Either explictly or maybe through extensions (though this could be supported without the marker interface)
	/// <summary>
	/// Immutable, readonly implementation of the <see cref="IResistanceStatsContainer"/> interface/contract.
	/// </summary>
	public class ImmutableResistanceStatsContainer : ImmutableStatsContainer<ResistanceStatType>, IResistanceStatsContainer
	{
		/// <summary>
		/// Readonly index accessor that takes in <see cref="ResistanceStatType"/>
		/// and returns the contained value. Does not throw if the container doesn't contain
		/// <paramref name="statIndex"/>; returns null instead.
		/// </summary>
		/// <param name="statIndex">The state type to query the container for.</param>
		/// <returns>The corresponding value for the <paramref name="statIndex"/> or null if the container doesn't contain it.</returns>
		public override int? this[ResistanceStatType statIndex]
		{
			get
			{
				//An ugly nested ternary but basically if it's within bounds we'll check to see if there is a value
				//If there is then we provide it otherwise we provide null
				//This is as expected, it's not in the collection then the caller recieves null.
				return isWithinBounds(statIndex) ? 
					(statsMap.ElementAt(statIndex.ToKey()).HasValue ? (int?)statsMap.ElementAt(statIndex.ToKey()).Value : null) 
					: null;
			}
		}

		/// <summary>
		/// Indicates if the container has a value for the given <see cref="ResistanceStatType"/>
		/// </summary>
		/// <param name="statType">The <typeparamref name="TStatType"/> to check the contained status of.</param>
		/// <returns>True if the stat is in the container.</returns>
		public override bool Contains(ResistanceStatType statType)
		{
			//Gets the key value (int) and checks the length and if there is a value in the map
			return isWithinBounds(statType) && statsMap.ElementAt(statType.ToKey()).HasValue;
		}

		private bool isWithinBounds(ResistanceStatType statType)
		{
			return statType.ToKey() > -1 && statType.ToKey() < statsMap.Count();
		}
	}
}
