using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class ImmutableTransientStatsContainerTests : ImmutableGenericContainerTests<ImmutableStatsContainer<TransientStatType>, TransientStatType> //the basic tests are handled in a generic base class
	{
		protected override IDictionary<TransientStatType, int> StatEnumTestInitValues()
		{
			return new Dictionary<TransientStatType, int>()
			{
				{TransientStatType.HitPoints, 1},
				{TransientStatType.PhotonBlastCharge, 2},
				{TransientStatType.TechniquePoints, 3}
			};
		}
	}
}
