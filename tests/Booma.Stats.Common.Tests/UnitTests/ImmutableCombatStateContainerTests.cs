using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class ImmutableCombatStateContainerTests
	{
		[Test]
		public static void Ctor_Doesnt_throw()
		{
			//assert: doesn't throw
			Assert.DoesNotThrow(() => new ImmutableCombatStatsContainer());
		}

		[Test]
		[TestCaseSource(nameof(CombatEnumValues))]
		public static void Default_Ctor_Should_Not_Have_Contained_Values(CombatStatType stat)
		{
			//arrange
			ImmutableCombatStatsContainer container = new ImmutableCombatStatsContainer();

			//assert
			Assert.False(container.Contains(stat));
		}

		[Test]
		[TestCaseSource(nameof(CombatEnumValues))]
		public static void Default_Ctor_Should_Provide_Null_On_Access(CombatStatType stat)
		{
			//arrange
			ImmutableCombatStatsContainer container = new ImmutableCombatStatsContainer();

			//act
			int? value = container[stat];

			//assert
			Assert.IsNull(value);
		}

		[Test]
		[TestCase(-1)]
		[TestCase(int.MaxValue)]
		public static void Default_Ctor_Should_Not_Throw_On_Invalid_Stat_Access_Should_Also_Be_Null(CombatStatType stat)
		{
			//arrange
			ImmutableCombatStatsContainer container = new ImmutableCombatStatsContainer();

			//act
			int? value = container[stat];

			//assert
			Assert.IsNull(value);
		}

		public static IEnumerable<CombatStatType> CombatEnumValues()
		{
			return Enum.GetValues(typeof(CombatStatType)).Cast<CombatStatType>();
		}
	}
}
