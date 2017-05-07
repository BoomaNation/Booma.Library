using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Contract for details of a spawn.
	/// </summary>
	public interface IEntitySpawnResults
	{
		SpawnResult Result { get; }

		/// <summary>
		/// The <see cref="GameObject"/> that represents the Entity in the engine.
		/// </summary>
		GameObject EntityGameObject { get; }
	}
}
