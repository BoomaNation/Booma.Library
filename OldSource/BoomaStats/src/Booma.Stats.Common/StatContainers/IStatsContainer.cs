using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Contract for container that provides read-only access 
	/// to a collection of <paramref name="TStatsType"/>.
	/// </summary>
	/// <typeparam name="TStatType">The stat type of the container.</typeparam>
	public interface IStatsContainer<TStatType>
		where TStatType : struct, IConvertible
	{
		/// <summary>
		/// Indicates if the container has a value for the given <paramref name="statType"/>
		/// </summary>
		/// <param name="statType">The <typeparamref name="TStatType"/> to check the contained status of.</param>
		/// <returns>True if the stat is in the container.</returns>
		bool Contains(TStatType statType);

		/// <summary>
		/// Readonly index accessor that takes in <typeparamref name="TStateType"/>s
		/// and returns the contained value with proper units. Does not throw if the container doesn't contain
		/// <paramref name="statIndex"/>; returns null instead.
		/// </summary>
		/// <param name="statIndex">The state type to query the container for.</param>
		/// <returns>The corresponding value with units for the <paramref name="statIndex"/> or null if the container doesn't contain it.</returns>
		IStatProvider<TStatType> this[TStatType statIndex]
		{
			get;
		}
	}
}
