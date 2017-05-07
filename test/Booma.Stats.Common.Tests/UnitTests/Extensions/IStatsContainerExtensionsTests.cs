using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	[TestFixture]
	public static class IStatsContainerExtensionsTests
	{
		[Test]
		public static void Test_GetMapSize_Value_From_Enum()
		{
			//assert
			Assert.True(IStatsContainerExtensions.GetMaxMapKeyValue<CombatStatType>(null) > 0);
			Assert.True(IStatsContainerExtensions.GetMaxMapKeyValue<CombatStatType>(null) == (int)((IEnumerable<CombatStatType>)Enum.GetValues(typeof(CombatStatType))).Max());
		}
	}
}
