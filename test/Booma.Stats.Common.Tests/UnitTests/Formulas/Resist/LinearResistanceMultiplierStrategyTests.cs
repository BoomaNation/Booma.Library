using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class LinearResistanceMultiplierStrategyTests
	{
		[Test]
		public static void Ctor_Doesnt_Throw()
		{
			//assert
			Assert.DoesNotThrow(() => new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>()));
		}

		[Test]
		[TestCaseSource(nameof(ResistEnumValues))]
		public static void Empty_Resistance_Container_Causes_All_Default_Multipliers_To_Be_Produce(ResistanceStatType resist)
		{
			//arrange
			LinearResistanceMultiplierStrategy strat = new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>());

			//assert: That no resistances should produce 1.0f multipliers
			Assert.AreEqual(1.0f, strat.ResistanceMultiplier(resist).Multiplier);
		}

		[Test]
		[TestCaseSource(nameof(ResistEnumValues))]
		public static void Resistance_Container_With_Zero_Resist_Causes_All_Default_Multipliers(ResistanceStatType resist)
		{
			//arrange
			LinearResistanceMultiplierStrategy strat = new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>(ResistContainerValuesWithZero()));

			//assert: That no resistances should produce 1.0f multipliers
			Assert.AreEqual(1.0f, strat.ResistanceMultiplier(resist).Multiplier);
		}

		[Test]
		[TestCaseSource(nameof(ResistEnumValues))]
		public static void Resistance_Container_With_Hundred_Resist_Causes_All_Zero_Values(ResistanceStatType resist)
		{
			//arrange
			LinearResistanceMultiplierStrategy strat = new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>(ResistContainerValuesWithHundreds()));

			//assert: That no resistances should produce 1.0f multipliers
			Assert.AreEqual(0.0f, strat.ResistanceMultiplier(resist).Multiplier);
		}

		[Test]
		[TestCaseSource(nameof(ResistEnumValues))]
		public static void Resistance_Container_With_50_Resist_Causes_All_Half_Values(ResistanceStatType resist)
		{
			//arrange
			LinearResistanceMultiplierStrategy strat = new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>(ResistContainerValuesWith50()));

			//assert: That no resistances should produce 1.0f multipliers
			Assert.AreEqual(0.5f, strat.ResistanceMultiplier(resist).Multiplier);
		}

		[Test]
		[TestCaseSource(nameof(ResistEnumValues))]
		public static void Resistance_Container_With_200_Resist_Causes_0_And_Not_Negative_Values(ResistanceStatType resist)
		{
			//arrange
			LinearResistanceMultiplierStrategy strat = new LinearResistanceMultiplierStrategy(new ImmutableStatsContainer<ResistanceStatType>(ResistContainerValuesWith200()));

			//assert: That no resistances should produce 1.0f multipliers
			Assert.AreEqual(0.0f, strat.ResistanceMultiplier(resist).Multiplier);
		}

		protected static IDictionary<ResistanceStatType, int> ResistContainerValuesWithZero()
		{
			return new Dictionary<ResistanceStatType, int>()
			{
				{ResistanceStatType.ElementalDark, 0},
				{ResistanceStatType.ElementalFire, 0},
				{ResistanceStatType.ElementalIce, 0},
				{ResistanceStatType.ElementalLight, 0},
				{ResistanceStatType.ElementalThunder, 0}
			};
		}

		protected static IDictionary<ResistanceStatType, int> ResistContainerValuesWithHundreds()
		{
			return new Dictionary<ResistanceStatType, int>()
			{
				{ResistanceStatType.ElementalDark, 100},
				{ResistanceStatType.ElementalFire, 100},
				{ResistanceStatType.ElementalIce, 100},
				{ResistanceStatType.ElementalLight, 100},
				{ResistanceStatType.ElementalThunder, 100}
			};
		}

		protected static IDictionary<ResistanceStatType, int> ResistContainerValuesWith50()
		{
			return new Dictionary<ResistanceStatType, int>()
			{
				{ResistanceStatType.ElementalDark, 50},
				{ResistanceStatType.ElementalFire, 50},
				{ResistanceStatType.ElementalIce, 50},
				{ResistanceStatType.ElementalLight, 50},
				{ResistanceStatType.ElementalThunder, 50}
			};
		}

		protected static IDictionary<ResistanceStatType, int> ResistContainerValuesWith200()
		{
			return new Dictionary<ResistanceStatType, int>()
			{
				{ResistanceStatType.ElementalDark, 200},
				{ResistanceStatType.ElementalFire, 200},
				{ResistanceStatType.ElementalIce, 200},
				{ResistanceStatType.ElementalLight, 200},
				{ResistanceStatType.ElementalThunder, 200}
			};
		}


		protected static IEnumerable<ResistanceStatType> ResistEnumValues()
		{
			return Enum.GetValues(typeof(ResistanceStatType)).Cast<ResistanceStatType>();
		}
	}
}
