using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common
{
	[TestFixture]
	public abstract class ImmutableStatContainerBuilderTestsGeneric<TStatType>
		where TStatType : struct, IConvertible
	{
		[Test]
		public void Test_Builder_Doesnt_Throw_On_Build()
		{
			//assert
			Assert.DoesNotThrow(() => BuildForGeneric(ImmutableStatsContainerBuilder<TStatType>.Factory.Create().WithDefaults()));
		}

		[Test]
		public void Test_Builder_Inits_To_Zero_With_Defaults()
		{
			//arrange
			ImmutableStatsContainer<TStatType> container = BuildForGeneric(ImmutableStatsContainerBuilder<TStatType>.Factory.Create().WithDefaults());

			foreach(TStatType statType in Enum.GetValues(typeof(TStatType)))
			{
				//assert: That all values are present and zero
				IStatProvider<TStatType> provider =  container[statType];

				Assert.NotNull(provider);
				Assert.AreEqual(0, provider.Value);
			}
		}

		protected abstract ImmutableStatsContainer<TStatType> BuildForGeneric(ImmutableStatsContainerBuilder<TStatType>.IBuilder builder);
	}
}
