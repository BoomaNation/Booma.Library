using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	public interface ISpawnPointStrategy
	{
		/// <summary>
		/// Generates a spawnpoint.
		/// </summary>
		/// <returns>A non-null Transform.</returns>
		Transform GetSpawnpoint();
	}
}
