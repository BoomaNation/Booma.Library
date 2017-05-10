using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Entity.Character;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Booma.Entity.Tests
{
	[TestFixture]
	public class SectionIdCalculatorTests
	{
		[Test]
		public static void Test_Player_Strategy_Throws_On_Null_Input()
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//assert
			Assert.Throws<ArgumentException>(() => strat.Compute(null, CharacterClassRace.FOmar));
		}

		[Test]
		public static void Test_Player_Strategy_Throws_On_Empty_Name()
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//assert
			Assert.Throws<ArgumentException>(() => strat.Compute("", CharacterClassRace.FOmar));
		}

		[Test]
		public static void Test_Player_Strategy_Throws_On_Invalid_ClassRace()
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//assert
			Assert.Throws<InvalidEnumArgumentException>(() => strat.Compute("Hello", CharacterClassRace.FOmar + 500));
		}
	}
}
