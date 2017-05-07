using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	[TestFixture]
	public abstract class ImmutableStatContainerBuilderTestsGeneric<TStatType>
		where TStatType : struct, IConvertible
	{
		protected static ImmutableStatsContainerBuilder<TStatType>.IBuilder GetNewBuilder()
		{
			return ImmutableStatsContainerBuilder<TStatType>.Factory.Create();
		}

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

		[Test]
		[TestCase(-1)]
		[TestCase(1)]
		[TestCase(0)]
		[TestCase(int.MaxValue)]
		[TestCase(int.MinValue)]
		public void Test_Builder_Inits_To_Zero_With_AllSame(int value)
		{
			//arrange
			ImmutableStatsContainer<TStatType> container = BuildForGeneric(ImmutableStatsContainerBuilder<TStatType>.Factory.Create().WithAllValuesAs(value));

			foreach (TStatType statType in Enum.GetValues(typeof(TStatType)))
			{
				//assert: That all values are present and zero
				IStatProvider<TStatType> provider = container[statType];

				Assert.NotNull(provider);
				Assert.AreEqual(value, provider.Value);
			}
		}

		[Test]
		[TestCase(-1)]
		[TestCase(1)]
		[TestCase(0)]
		[TestCase(int.MaxValue)]
		[TestCase(int.MinValue)]
		public void Test_Builder_Inits_To_Zero_With_WithStat(int value)
		{
			//arrange
			var builder = ImmutableStatsContainerBuilder<TStatType>.Factory.Create();
			ImmutableStatsContainer<TStatType> container = null;

			//act
			foreach (TStatType statType in Enum.GetValues(typeof(TStatType)))
			{
				builder.WithStat(statType, value);	
			}


			container = BuildForGeneric(builder);

			foreach (TStatType statType in Enum.GetValues(typeof(TStatType)))
			{
				//assert: That all values are present and zero
				IStatProvider<TStatType> provider = container[statType];

				Assert.NotNull(provider);
				Assert.AreEqual(value, provider.Value);
			}
		}

		protected abstract ImmutableStatsContainer<TStatType> BuildForGeneric(ImmutableStatsContainerBuilder<TStatType>.IBuilder builder);
	}
}
