using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Interface that contracts implementers to expose stat information
	/// </summary>
	public interface IStatsQueryable
	{
		/// <summary>
		/// Immutable container for entity's base combat stats.
		/// </summary>
		ImmutableStatsContainer<CombatStatType> BaseCombatStats { get; }

		/// <summary>
		/// Immutable container for entity's base resistance stats.
		/// </summary>
		ImmutableStatsContainer<ResistanceStatType> BaseResistanceStats { get; }
	}
}
