using Booma.Stats.Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Combat.Formula.Server.Tests
{
	[TestFixture]
	public static class MeleeDamageAcceptanceTests
	{
		[Test]
		[TestCase(15, 30, 25, 0, 7)]
		[TestCase(15, 0, 0, 0, 2)]
		public static void Test_BasicWeak_Melee_Attacks_From_PSOBB_Against_Code(int PsoBBATP, int PsoBBWeaponATPMax, int PsoBBWeaponATPMin, int TargetDFP, int PsoBBDamage)
		{
			//arrange
			//Setup the weapon provider
			Mock<IStatProvider<CombatStatType>> weaponATPProvider = new Mock<IStatProvider<CombatStatType>>();
			weaponATPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			weaponATPProvider.SetupGet(p => p.Value).Returns(PsoBBWeaponATPMax);

			//Setup the player provider
			Mock<IStatProvider<CombatStatType>> playerATPProvider = new Mock<IStatProvider<CombatStatType>>();
			playerATPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			playerATPProvider.SetupGet(p => p.Value).Returns(PsoBBATP);

			//Setup range
			Mock<IStatProvider<CombatStatType>> rangeATPProvider = new Mock<IStatProvider<CombatStatType>>();
			rangeATPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			rangeATPProvider.SetupGet(p => p.Value).Returns(PsoBBWeaponATPMax - PsoBBWeaponATPMin);

			//Create final atp provider
			IStatProvider<CombatStatType> finalATPMax = new MaximumBoundATPCalculationStrategy(playerATPProvider.Object, weaponATPProvider.Object, rangeATPProvider.Object);
			IStatProvider<CombatStatType> finalATPMin = new MinimumBoundATPCalculationStrategy(playerATPProvider.Object, weaponATPProvider.Object);

			//Create the provider for defense
			Mock<IStatProvider<CombatStatType>> targetDFPProvider = new Mock<IStatProvider<CombatStatType>>();
			targetDFPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.DefensivePower);
			targetDFPProvider.SetupGet(p => p.Value).Returns(TargetDFP);
			
			//act
			MeleeDamageResultStrategy meleeDamageStratMax = new MeleeDamageResultStrategy(finalATPMax, targetDFPProvider.Object, CombatMeleeAttackTypeMultiplier.Base);
			MeleeDamageResultStrategy meleeDamageStratMin = new MeleeDamageResultStrategy(finalATPMin, targetDFPProvider.Object, CombatMeleeAttackTypeMultiplier.Base);


			//assert
			//Check if it's inbetween the max and min damage.
			Assert.IsTrue(PsoBBDamage <= meleeDamageStratMax.Value && PsoBBDamage >= meleeDamageStratMin.Value, $"Failed to compute same value from PSOBB. PSOBB {PsoBBDamage} vs PSORM Range {meleeDamageStratMin.Value} - {meleeDamageStratMax.Value}");
		}
	}
}
