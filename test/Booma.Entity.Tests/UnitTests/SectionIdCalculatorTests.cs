using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Entity.Character;
using NUnit.Framework;

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

		[Test]
		public static void Test_Player_Strategy_Doesnt_Throw_On_Valid_Input()
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//assert
			Assert.DoesNotThrow(() => strat.Compute("Hello", CharacterClassRace.FOmar));
		}

		[Test]
		public static void Test_Player_Strategy_Throws_On_AltCode_Characters()
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//assert
			Assert.Throws<ArgumentException>(() => strat.Compute("Hellá", CharacterClassRace.FOmar));
		}

		[Test]
		[TestCase(SectionId.Oran, "Glader", CharacterClassRace.HUnewearl)]
		[TestCase(SectionId.Purplenum, "Glader", CharacterClassRace.FOnewearl)]
		[TestCase(SectionId.Redria, "Glader", CharacterClassRace.HUmar)]
		[TestCase(SectionId.Skyly, "3064", CharacterClassRace.HUcast)]
		public static void Test_Player_Strategy_Produces_PSOBB_Expected_SectionId_Values(SectionId expectedId, string name, CharacterClassRace classRace)
		{
			//arrange
			PlayerSectionIdCalculatorStrategy strat = new PlayerSectionIdCalculatorStrategy();

			//act
			SectionId sectionId = strat.Compute(name, classRace);

			//assert
			Assert.AreEqual(expectedId, sectionId, $"Expected {expectedId} for {name} {classRace} but computed {sectionId}.");
		}
	}
}
