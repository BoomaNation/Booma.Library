using Booma.Stats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Server
{
	/// <summary>
	/// Interface that contracts implementers to expose stat information
	/// </summary>
	public interface IStatsQueryable
	{
		/// <summary>
		/// Immutable container for entity's combat stats.
		/// </summary>
		ImmutableStatsContainer<CombatStatType> CombatStats { get; }

		/// <summary>
		/// Immutable container for entity's resistance stats.
		/// </summary>
		ImmutableStatsContainer<ResistanceStatType> ResistanceStats { get; }
	}
}
