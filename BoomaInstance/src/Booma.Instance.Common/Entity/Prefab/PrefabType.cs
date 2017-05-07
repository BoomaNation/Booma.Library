using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Enumeration of prefab types for an instance.
	/// </summary>
	public enum PrefabType : byte
	{
		None = 0,

		/// <summary>
		/// Network player prefab.
		/// </summary>
		NetworkPlayer = 1,

		/// <summary>
		/// Local player prefab.
		/// </summary>
		LocalPlayer = 2,
	}
}
