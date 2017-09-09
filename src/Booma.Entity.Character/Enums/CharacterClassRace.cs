using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// PSO doesn't have races and classes. Just a combination of both.
	/// Unlike WoW where you can have a Dwarf Warlock there is only something akin to a DWLock.
	/// </summary>
	public enum CharacterClassRace : int
	{
		[SectionIdWeight(5)]
		HUmar,

		[SectionIdWeight(6)]
		HUnewearl,

		[SectionIdWeight(7)]
		HUcast,

		[SectionIdWeight(4)]
		HUcaseal,

		[SectionIdWeight(8)]
		RAmar,

		[SectionIdWeight(6)]
		RAmarl,

		[SectionIdWeight(9)]
		RAcast,

		[SectionIdWeight(0)]
		RAcaseal,

		[SectionIdWeight(5)]
		FOmar,

		[SectionIdWeight(1)]
		FOmarl,

		[SectionIdWeight(2)]
		FOnewm,

		[SectionIdWeight(3)]
		FOnewearl
	}
}
