using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Simplified interface that extends a <see cref="CombatStatType"/>
	/// <see cref="IStatsContainer{TStatType}"/> interace.
	/// </summary>
	public interface ICombatStatsContainer : IStatsContainer<CombatStatType>
	{

	}
}
