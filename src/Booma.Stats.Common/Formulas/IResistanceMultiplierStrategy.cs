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
		/// <summary>
		/// Generates a resistance multiplier for <paramref name="resistType"/>.
		/// </summary>
		/// <param name="resistType">Type of resistance.</param>
		/// <returns>Float values (1.0 if no resist) that acts as a multiplier for resistance.</returns>
		float ResistanceMultiplier(ResistanceStatType resistType);
	}
}
