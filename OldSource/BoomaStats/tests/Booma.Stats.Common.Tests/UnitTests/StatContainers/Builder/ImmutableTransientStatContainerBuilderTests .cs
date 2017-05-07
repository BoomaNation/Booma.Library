using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests
{
	[TestFixture]
	public class ImmutableTransientStatContainerBuilderTests : ImmutableStatContainerBuilderTestsGeneric<TransientStatType>
	{
		protected override ImmutableStatsContainer<TransientStatType> BuildForGeneric(ImmutableStatsContainerBuilder<TransientStatType>.IBuilder builder)
		{
			return builder.Build();
		}

		[Test]
		[TestCase(1,2,3)]
		public static void Test_Individual_Transient_Builder_Extensions_Set_Value(int hp, int tp, int pb)
		{
			//arrange
			var builder = GetNewBuilder();

			//act
			ImmutableStatsContainer<TransientStatType> container =
				builder.WithHitPoints(hp)
					.WithTechniquePoints(tp)
					.WithPhotonBlastCharge(pb)
					.Build();

			//assert
			Assert.AreEqual(container[TransientStatType.HitPoints].Value, hp);
			Assert.AreEqual(container[TransientStatType.PhotonBlastCharge].Value, pb);
			Assert.AreEqual(container[TransientStatType.TechniquePoints].Value, tp);
		}

		[Test]
		[TestCase(1, 2)]
		public static void Test_Individual_Transient_Builder_BuiltContainer_Returns_Null_If_Not_Set(int hp, int tp)
		{
			//arrange
			var builder = GetNewBuilder();

			//act: In this test we don't init pb
			ImmutableStatsContainer<TransientStatType> container =
				builder.WithHitPoints(hp)
					.WithTechniquePoints(tp)
					.Build();

			//assert
			Assert.AreEqual(container[TransientStatType.HitPoints].Value, hp);
			Assert.AreEqual(container[TransientStatType.TechniquePoints].Value, tp);

			Assert.Null(container[TransientStatType.PhotonBlastCharge]);
		}
	}
}
