using Booma.Stats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Server
{
	/// <summary>
	/// Interface that contracts implementers to expose transient or semi-mutable stats.
	/// </summary>
	public interface IActiveStatsQueryable
	{
		/// <summary>
		/// Immutable container for entity's transient stats.
		/// </summary>
		ImmutableStatsContainer<TransientStatType> TransientStats { get; }
	}
}
