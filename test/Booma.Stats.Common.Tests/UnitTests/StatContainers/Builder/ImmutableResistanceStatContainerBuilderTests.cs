using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common.Tests
{
	[TestFixture]
	public class ImmutableResistanceStatContainerBuilderTests : ImmutableStatContainerBuilderTestsGeneric<ResistanceStatType>
	{
		protected override ImmutableStatsContainer<ResistanceStatType> BuildForGeneric(ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder)
		{
			return builder.Build();
		}

		[Test]
		[TestCase(5, 6, 7, 8, 9)]
		public static void Test_Individual_Resist_Builder_Extensions_Set_Value(int fire, int ice, int dark, int light, int thunder)
		{
			//arrange
			var builder = GetNewBuilder();

			//act
			ImmutableStatsContainer<ResistanceStatType> container =
				builder.WithDarkResist(dark)
					.WithFireResist(fire)
					.WithIceResist(ice)
					.WithThunderResist(thunder)
					.WithLightResist(light)
					.Build();

			//assert
			Assert.AreEqual(container[ResistanceStatType.ElementalFire].Value, fire);
			Assert.AreEqual(container[ResistanceStatType.ElementalDark].Value, dark);
			Assert.AreEqual(container[ResistanceStatType.ElementalIce].Value, ice);
			Assert.AreEqual(container[ResistanceStatType.ElementalLight].Value, light);
			Assert.AreEqual(container[ResistanceStatType.ElementalThunder].Value, thunder);
		}

		[Test]
		[TestCase(5, 6, 7, 8, 9)]
		public static void Test_Individual_Resist_Builder_BuiltContainer_Returns_Null_If_Not_Set(int fire, int ice, int dark, int light, int thunder)
		{
			//arrange
			var builder = GetNewBuilder();

			//act: In this test we removed light.
			ImmutableStatsContainer<ResistanceStatType> container =
				builder.WithDarkResist(dark)
					.WithFireResist(fire)
					.WithIceResist(ice)
					.WithThunderResist(thunder)
					.Build();

			//assert
			Assert.AreEqual(container[ResistanceStatType.ElementalFire].Value, fire);
			Assert.AreEqual(container[ResistanceStatType.ElementalDark].Value, dark);
			Assert.AreEqual(container[ResistanceStatType.ElementalIce].Value, ice);
			Assert.AreEqual(container[ResistanceStatType.ElementalThunder].Value, thunder);

			Assert.IsNull(container[ResistanceStatType.ElementalLight]);
		}
	}
}
