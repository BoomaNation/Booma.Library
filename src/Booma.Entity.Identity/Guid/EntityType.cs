using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Identity
{
	/// <summary>
	/// Enumeration of all Entity types.
	/// </summary>
	public enum EntityType : byte
	{
		/// <summary>
		/// Player entity.
		/// </summary>
		Player = 0,

		/// <summary>
		/// GameObject entity.
		/// (Such as a Door or a Button)
		/// </summary>
		GameObject = 1,
	}
}
