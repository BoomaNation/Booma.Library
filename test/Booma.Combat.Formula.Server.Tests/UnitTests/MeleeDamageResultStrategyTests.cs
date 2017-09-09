using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Combat.Formula.Server.Tests
{
	[TestFixture]
	public static class MeleeDamageResultStrategyTests
	{
		[Test]
		public static void Ctor_Throws_On_InvalidStatType_First_Arg()
		{
			//arrange
			Mock<IStatProvider<CombatStatType>> providerATP = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerATP.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);

			Mock<IStatProvider<CombatStatType>> providerDFP = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerDFP.SetupGet(p => p.StatType).Returns(CombatStatType.DefensivePower);

			//assert
			Assert.Throws<ArgumentException>(() => new MeleeDamageResultStrategy(providerDFP.Object, providerDFP.Object, Mock.Of<IMultiplierProvider>()));
		}

		[Test]
		public static void Ctor_Throws_On_InvalidStatType_Second_Arg()
		{
			//arrange
			Mock<IStatProvider<CombatStatType>> providerATP = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerATP.SetupGet(p => p.StatType).Returns(CombatStatType.AttackPower);

			Mock<IStatProvider<CombatStatType>> providerDFP = new Mock<IStatProvider<CombatStatType>>(MockBehavior.Loose);
			providerDFP.SetupGet(p => p.StatType).Returns(CombatStatType.DefensivePower);

			//assert
			Assert.Throws<ArgumentException>(() => new MeleeDamageResultStrategy(providerATP.Object, providerATP.Object, Mock.Of<IMultiplierProvider>()));
		}
	}
}
