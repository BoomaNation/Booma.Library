using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Marks a section ID with a weight for influencing section ID calculation.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class SectionIdWeightAttribute : Attribute
	{
		/// <summary>
		/// The section ID weight the class has.
		/// See: http://phantasystar.wikia.com/wiki/Section_ID about classes
		/// </summary>
		public int SectionIdWeight { get; }

		public SectionIdWeightAttribute(int sectionIdWeight)
		{
			if (sectionIdWeight < 0) throw new ArgumentOutOfRangeException(nameof(sectionIdWeight));

			SectionIdWeight = sectionIdWeight;
		}
	}
}
