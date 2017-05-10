using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Character
{
	/// <summary>
	/// Enumeration of all section IDs.
	/// See: http://phantasystar.wikia.com/wiki/Section_ID
	/// </summary>
	public enum SectionId : int
	{
		//The attributes mark section ID calculation weights
		//See: http://phantasystar.wikia.com/wiki/Section_ID

		[SectionIdWeight(0)]
		Viridia,

		[SectionIdWeight(1)]
		Greenill,

		[SectionIdWeight(2)]
		Skyly,

		[SectionIdWeight(3)]
		Bluefull,

		[SectionIdWeight(4)]
		Purplenum,

		[SectionIdWeight(5)]
		Pinkal,

		[SectionIdWeight(6)]
		Redria,

		[SectionIdWeight(7)]
		Oran,

		[SectionIdWeight(8)]
		Yellowboze,

		[SectionIdWeight(9)]
		Whitill
	}
}
