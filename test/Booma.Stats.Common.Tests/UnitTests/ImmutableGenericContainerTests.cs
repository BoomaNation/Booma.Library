using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests.UnitTests
{
	//This is a generic test class that'll test the expected implementation of the container type
	//This helps consolidate code between combat and resist state containers
	[TestFixture]
	public abstract class ImmutableGenericContainerTests<TImmutableContainerType, TStatType>
		where TImmutableContainerType : ImmutableStatsContainer<TStatType> where TStatType : struct, IConvertible
	{
		#region Default Ctor (empty) tests

		[Test]
		public void Default_Ctor_Doesnt_throw()
		{
			//assert: doesn't throw
			Assert.DoesNotThrow(() => Activator.CreateInstance<TImmutableContainerType>());
		}

		[Test]
		[TestCaseSource(nameof(StatEnumValues))]
		public void Default_Ctor_Should_Not_Have_Contained_Values(TStatType stat)
		{
			//arrange
			TImmutableContainerType container = Activator.CreateInstance<TImmutableContainerType>();

			//assert
			Assert.False(container.Contains(stat));
		}

		[Test]
		[TestCaseSource(nameof(StatEnumValues))]
		public void Default_Ctor_Should_Provide_Null_On_Access(TStatType stat)
		{
			//arrange
			TImmutableContainerType container = Activator.CreateInstance<TImmutableContainerType>();

			//act
			IStatProvider<TStatType> value = container[stat];

			//assert
			Assert.IsNull(value);
		}

		[Test]
		[TestCase(-1)]
		[TestCase(int.MaxValue)]
		public void Default_Ctor_Should_Not_Throw_On_Invalid_Stat_Access_Should_Also_Be_Null(TStatType stat)
		{
			//arrange
			TImmutableContainerType container = Activator.CreateInstance<TImmutableContainerType>();

			//act
			IStatProvider<TStatType> value = container[stat];

			//assert
			Assert.IsNull(value);
		}

		[Test]
		[TestCase(-1)]
		[TestCase(int.MaxValue)]
		public void Default_Ctor_Should_Not_Throw_On_Invalid_Values_For_Contains_Method(TStatType stat)
		{
			//arrange
			TImmutableContainerType container = Activator.CreateInstance<TImmutableContainerType>();

			//assert
			Assert.False(container.Contains(stat));
		}

		#endregion

		[Test]
		public void Initialization_Ctor_Doesnt_throw()
		{
			//assert: doesn't throw
			Assert.DoesNotThrow(() => Activator.CreateInstance(typeof(TImmutableContainerType), StatEnumTestInitValues()));
		}

		protected abstract IDictionary<TStatType, int> StatEnumTestInitValues();

		public static IEnumerable<TStatType> StatEnumValues()
		{
			return Enum.GetValues(typeof(TStatType)).Cast<TStatType>();
		}
	}
}
