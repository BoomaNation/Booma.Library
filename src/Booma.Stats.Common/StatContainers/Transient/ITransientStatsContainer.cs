using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	/// <summary>
	/// Simplified interface that extends a <see cref="TransientStatType"/>
	/// <see cref="IStatsContainer{TStatType}"/> interace.
	/// </summary>
	public interface ITransientStatsContainer : IStatsContainer<TransientStatType>
	{

	}
}
