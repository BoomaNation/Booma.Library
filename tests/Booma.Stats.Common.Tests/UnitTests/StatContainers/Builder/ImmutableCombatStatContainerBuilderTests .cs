using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests
{
	[TestFixture]
	public class ImmutableCombatStatContainerBuilderTests : ImmutableStatContainerBuilderTestsGeneric<CombatStatType>
	{
		protected override ImmutableStatsContainer<CombatStatType> BuildForGeneric(ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder)
		{
			return builder.Build();
		}

		[Test]
		[TestCase(1,2,3,4,5,6,7,8)]
		public static void Test_Individual_Combat_Builder_Extensions_Set_Value(int ata, int atp, int dtp, int evp, int hp, int luck, int mst, int tp)
		{
			//arrange
			var builder = GetNewBuilder();

			//act
			ImmutableStatsContainer<CombatStatType> container =
				builder.WithAttackAccuracy(ata)
					.WithAttackPower(atp)
					.WithDefensivePower(dtp)
					.WithEvasivePower(evp)
					.WithHitPoints(hp)
					.WithLuck(luck)
					.WithMentalStrength(mst)
					.WithTechniquePoints(tp)
					.Build();

			//assert
			Assert.AreEqual(container[CombatStatType.AttackAccuracy].Value, ata);
			Assert.AreEqual(container[CombatStatType.AttackPower].Value, atp);
			Assert.AreEqual(container[CombatStatType.DefensivePower].Value, dtp);
			Assert.AreEqual(container[CombatStatType.EvasivePower].Value, evp);
			Assert.AreEqual(container[CombatStatType.HitPoints].Value, hp);
			Assert.AreEqual(container[CombatStatType.Luck].Value, luck);
			Assert.AreEqual(container[CombatStatType.MentalStrength].Value, mst);
			Assert.AreEqual(container[CombatStatType.TechniquePoints].Value, tp);
		}

		[Test]
		[TestCase(1, 2, 3, 4, 5, 6, 7)]
		public static void Test_Individual_Combat_Builder_BuiltContainer_Returns_Null_If_Not_Set(int ata, int atp, int dtp, int evp, int hp, int luck, int mst)
		{
			//arrange
			var builder = GetNewBuilder();

			//act: We don't init tp here in this test
			ImmutableStatsContainer<CombatStatType> container =
				builder.WithAttackAccuracy(ata)
					.WithAttackPower(atp)
					.WithDefensivePower(dtp)
					.WithEvasivePower(evp)
					.WithHitPoints(hp)
					.WithLuck(luck)
					.WithMentalStrength(mst)
					.Build();

			//assert
			Assert.AreEqual(container[CombatStatType.AttackAccuracy].Value, ata);
			Assert.AreEqual(container[CombatStatType.AttackPower].Value, atp);
			Assert.AreEqual(container[CombatStatType.DefensivePower].Value, dtp);
			Assert.AreEqual(container[CombatStatType.EvasivePower].Value, evp);
			Assert.AreEqual(container[CombatStatType.HitPoints].Value, hp);
			Assert.AreEqual(container[CombatStatType.Luck].Value, luck);
			Assert.AreEqual(container[CombatStatType.MentalStrength].Value, mst);

			Assert.IsNull(container[CombatStatType.TechniquePoints]);
		}
	}
}
