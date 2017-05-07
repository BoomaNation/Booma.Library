using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Generates multiplier values for resistances.
	/// </summary>
	public interface IResistanceMultiplierStrategy
	{
		//The reason we return an IMultiplierProvider is because all multipliers fit this interface
		//However, not all strategies that produce multipliers can fit the same interface because sometimes they require
		//stat type data and etc, like this case, so we return a multiplier provider that contains the value.

		/// <summary>
		/// Generates a resistance multiplier for <paramref name="resistType"/>.
		/// </summary>
		/// <param name="resistType">Type of resistance.</param>
		/// <returns>Float values (1.0 if no resist) that acts as a multiplier for resistance.</returns>
		IMultiplierProvider ResistanceMultiplier(ResistanceStatType resistType);
	}
}
