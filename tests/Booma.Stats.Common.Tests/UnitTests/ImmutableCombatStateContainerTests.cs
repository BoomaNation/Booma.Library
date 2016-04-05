using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class ImmutableCombatStateContainerTests
	{
		public static void Ctor_Doesnt_throw()
		{
			//assert: doesn't throw
			Assert.DoesNotThrow(() => new ImmutableCombatStatsContainer());
		}
	}
}
