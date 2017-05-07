using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this class isn't generic, and trust me I have the wizard skills to create such an amalgation,
	//is because I expect the IResistanceStatsContainer to eventually have functionality different from resistence
	//Either explictly or maybe through extensions (though this could be supported without the marker interface)
	/// <summary>
	/// Immutable, readonly implementation of the <see cref="IResistanceStatsContainer"/> interface/contract.
	/// </summary>
	public class ImmutableResistanceStatsContainer : ImmutableStatsContainer<ResistanceStatType>, IResistanceStatsContainer
	{
		/// <summary>
		/// Creates a partially initialized immutable container for stats.
		/// </summary>
		/// <param name="values">Values key-store for <see cref="ResistanceStatType"/>.</param>
		public ImmutableResistanceStatsContainer(IDictionary<ResistanceStatType, int> values)
			: base(values)
		{

		}

		/// <summary>
		/// Creates an empty-immutable container for stats.
		/// </summary>
		public ImmutableResistanceStatsContainer()
			: base()
		{

		}

		/// <summary>
		/// Converts a <see cref="ResistanceStatType"/> value to an int key value.
		/// </summary>
		/// <param name="statType">The stat type to convert to int.</param>
		/// <returns>A key value for the given <paramref name="statType"/>.</returns>
		protected override int ConvertStatToKey(ResistanceStatType statType)
		{
			return statType.ToKey();
		}
	}
}
