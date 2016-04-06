using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this class isn't generic, and trust me I have the wizard skills to create such an amalgation,
	//is because I expect the ICombatStatsContainer to eventually have functionality different from resistence
	//Either explictly or maybe through extensions (though this could be supported without the marker interface)
	/// <summary>
	/// Immutable, readonly implementation of the <see cref="ICombatStatsContainer"/> interface/contract.
	/// </summary>
	public class ImmutableCombatStatsContainer : ImmutableStatsContainer<CombatStatType>, ICombatStatsContainer
	{
		public ImmutableCombatStatsContainer(IDictionary<CombatStatType, int> values)
			: base(values)
		{

		}

		public ImmutableCombatStatsContainer()
			: base()
		{

		}

		protected override int ConvertStatToKey(CombatStatType statType)
		{
			return statType.ToKey();
		}
	}
}
