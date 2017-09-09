using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Enumeration of all spawn results.
	/// </summary>
	public enum SpawnResult : byte
	{
		//TODO: Flesh out all the reasons a spawn could fail.

		Success = 0,

		PrefabUnavailable = 1,

		NoSpace = 2,
	}
}
