using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this class isn't generic, and trust me I have the wizard skills to create such an amalgation,
	//is because I expect the ITransientStatsContainer to eventually have functionality different from resistence
	//Either explictly or maybe through extensions (though this could be supported without the marker interface)
	/// <summary>
	/// Immutable, readonly implementation of the <see cref="ITransientStatsContainer"/> interface/contract.
	/// </summary>
	public class ImmutableTransientStatsContainer : ImmutableStatsContainer<TransientStatType>, ITransientStatsContainer
	{
		/// <summary>
		/// Creates a partially initialized immutable container for stats.
		/// </summary>
		/// <param name="values">Values key-store for <see cref="ResistanceStatType"/>.</param>
		public ImmutableTransientStatsContainer(IDictionary<TransientStatType, int> values)
			: base(values)
		{

		}

		/// <summary>
		/// Creates an empty-immutable container for stats.
		/// </summary>
		public ImmutableTransientStatsContainer()
			: base()
		{

		}
	}
}
