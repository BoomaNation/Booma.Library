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
		[TestCase(52, 55, 40, 5, 17)] //gobooma
		[TestCase(52, 55, 40, 5, 15)] //gobooma
		[TestCase(52, 55, 40, 25, 12)]
		[TestCase(45, 55, 40, 25, 10)]
		[TestCase(45, 55, 40, 25, 11)]
		[TestCase(45, 55, 40, 0, 17)]
		[TestCase(45, 55, 40, 0, 15)]
		[TestCase(45, 55, 40, 0, 14)]
		//Above is level 1 Humar with Saber
		[TestCase(17, 30, 25, 0, 8)]
		//levled up to 3 above this point
		[TestCase(15, 30, 25, 0, 8)] //monest
		[TestCase(15, 30, 25, 25, 3)] //wolf
		[TestCase(15, 30, 25, 25, 4)]
		[TestCase(15, 30, 25, 0, 6)] //booma
		[TestCase(15, 30, 25, 0, 7)]
		[TestCase(15, 0, 0, 0, 2)]
		public static void Test_BasicWeak_Melee_Attacks_From_PSOBB_Against_Code(int PsoBBATP, int PsoBBWeaponATPMax, int PsoBBWeaponATPMin, int TargetDFP, int PsoBBDamage)
		{
			//arrange
			//Setup the weapon provider
			Mock<IStatProvider<CombatStatType>> weaponATPProviderMax = new Mock<IStatProvider<CombatStatType>>();
			weaponATPProviderMax.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			weaponATPProviderMax.SetupGet(p => p.Value).Returns(PsoBBWeaponATPMax);

			Mock<IStatProvider<CombatStatType>> weaponATPProviderMin = new Mock<IStatProvider<CombatStatType>>();
			weaponATPProviderMin.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			weaponATPProviderMin.SetupGet(p => p.Value).Returns(PsoBBWeaponATPMin);

			//Setup the player provider
			Mock<IStatProvider<CombatStatType>> playerATPProvider = new Mock<IStatProvider<CombatStatType>>();
			playerATPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			playerATPProvider.SetupGet(p => p.Value).Returns(PsoBBATP);

			//Setup range
			Mock<IStatProvider<CombatStatType>> rangeATPProvider = new Mock<IStatProvider<CombatStatType>>();
			rangeATPProvider.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);
			rangeATPProvider.SetupGet(p => p.Value).Returns(PsoBBWeaponATPMax - PsoBBWeaponATPMin);

			//Create final atp provider
			IStatProvider<CombatStatType> finalATPMax = new MaximumBoundATPCalculationStrategy(playerATPProvider.Object, weaponATPProviderMax.Object, rangeATPProvider.Object);
			IStatProvider<CombatStatType> finalATPMin = new MinimumBoundATPCalculationStrategy(playerATPProvider.Object, weaponATPProviderMin.Object);

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
