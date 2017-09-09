using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Enumeration of all potential transient stats.
	/// These are stats that change often and are transient in the sense that they aren't saved.
	/// </summary>
	public enum TransientStatType
	{
		/// <summary>
		/// HP - Hit Points
		/// </summary>
		HitPoints,

		/// <summary>
		/// TP - Technique Points
		/// </summary>
		TechniquePoints,

		/// <summary>
		/// PB - Photon Blast
		/// </summary>
		PhotonBlastCharge,
	}
}
