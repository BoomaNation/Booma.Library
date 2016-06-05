using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Stats.Common.Tests
{
	[TestFixture]
	public class ImmutableResistanceStatContainerBuilderTests : ImmutableStatContainerBuilderTestsGeneric<ResistanceStatType>
	{
		protected override ImmutableStatsContainer<ResistanceStatType> BuildForGeneric(ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder)
		{
			return builder.Build();
		}
	}
}
