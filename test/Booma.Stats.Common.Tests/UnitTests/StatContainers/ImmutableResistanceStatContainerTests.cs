using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common.Tests.UnitTests
{
	[TestFixture]
	public class ImmutableResistanceStatContainerTests : ImmutableGenericContainerTests<ImmutableStatsContainer<ResistanceStatType>, ResistanceStatType> //the basic tests are handled in a generic base class
	{
		protected override IDictionary<ResistanceStatType, int> StatEnumTestInitValues()
		{
			return new Dictionary<ResistanceStatType, int>()
			{
				{ResistanceStatType.ElementalDark, 1},
				{ResistanceStatType.ElementalFire, 2},
				{ResistanceStatType.ElementalIce, 3},
				{ResistanceStatType.ElementalLight, 4},
				{ResistanceStatType.ElementalThunder, 5}
			};
		}
	}
}
