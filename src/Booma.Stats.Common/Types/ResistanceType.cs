using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public enum ResistanceType : int //int prevents boxing in dictionaries and such but increases bandwidth usage
	{
		/// <summary>
		/// EFR - Elemental Fire
		/// </summary>
		ElementalFire,

		/// <summary>
		/// EIC - Elemental Ice
		/// </summary>
		ElementalIce,

		/// <summary>
		/// ETH - Elemental Thunder
		/// </summary>
		ElementalThunder,

		/// <summary>
		/// EDK - Elemental Dark
		/// </summary>
		ElementalDark,

		/// <summary>
		/// ELT - Elemental Light
		/// </summary>
		ElementalLight
	}
}
