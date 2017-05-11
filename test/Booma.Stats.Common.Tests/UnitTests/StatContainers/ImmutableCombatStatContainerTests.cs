using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class ImmutableCombatStatContainerTests : ImmutableGenericContainerTests<ImmutableStatsContainer<CombatStatType>, CombatStatType> //the basic tests are handled in a generic base class
	{
		protected override IDictionary<CombatStatType, int> StatEnumTestInitValues()
		{
			return new Dictionary<CombatStatType, int>()
			{
				{CombatStatType.AttackAccuracy, 1},
				{CombatStatType.AttackPower, 2},
				{CombatStatType.DefensivePower, 3},
				{CombatStatType.EvasivePower, 4},
				{CombatStatType.HitPoints, 5},
				{CombatStatType.Luck, 6},
				{CombatStatType.MentalStrength, 7},
				{CombatStatType.TechniquePoints, 8}
			};
		}
	}
}
