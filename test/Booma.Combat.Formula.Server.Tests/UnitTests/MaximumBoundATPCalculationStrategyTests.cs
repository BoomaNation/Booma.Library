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
	public static class MaximumBoundATPCalculationStrategyTests
	{
		[Test]
		public static void Ctor_Throws_On_InvalidStatType_First_Arg()
		{
			//arrange
			Mock<IStatProvider<CombatStatType>> providerWrong = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerWrong.SetupGet(p => p.StatType).Returns(CombatStatType.MentalStrength);

			Mock<IStatProvider<CombatStatType>> providerRight = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerRight.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);

			//assert
			Assert.Throws<ArgumentException>(() => new MaximumBoundATPCalculationStrategy(providerWrong.Object, providerRight.Object, providerRight.Object));
		}

		[Test]
		public static void Ctor_Throws_On_InvalidStatType_Second_Arg()
		{
			//arrange
			Mock<IStatProvider<CombatStatType>> providerWrong = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerWrong.SetupGet(p => p.StatType).Returns(CombatStatType.MentalStrength);

			Mock<IStatProvider<CombatStatType>> providerRight = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerRight.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);

			//assert
			Assert.Throws<ArgumentException>(() => new MaximumBoundATPCalculationStrategy(providerRight.Object, providerWrong.Object, providerRight.Object));
		}

		[Test]
		public static void Ctor_Throws_On_InvalidStatType_Third_Arg()
		{
			//arrange
			Mock<IStatProvider<CombatStatType>> providerWrong = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerWrong.SetupGet(p => p.StatType).Returns(CombatStatType.MentalStrength);

			Mock<IStatProvider<CombatStatType>> providerRight = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerRight.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);

			//assert
			Assert.Throws<ArgumentException>(() => new MaximumBoundATPCalculationStrategy(providerRight.Object, providerRight.Object, providerWrong.Object));
		}
	}
}
