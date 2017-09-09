using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	public enum ZoneType
	{
		/// <summary>
		/// An undefined zone type reserved for only
		/// representing deserialization errors.
		/// </summary>
		Undefined = 0,

		/// <summary>
		/// Indicates an unshared zone or instance for a specific
		/// event, player, or etc.
		/// </summary>
		Instance = 1,

		/// <summary>
		/// Indicates the zone is apart of the public world.
		/// </summary>
		World = 2,
	}
}
