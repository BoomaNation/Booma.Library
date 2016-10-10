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
	public interface IEntitySpawnDetails
	{
		/// <summary>
		/// Unique Identifier of the Entity.
		/// </summary>
		int EntityId { get; }

		/// <summary>
		/// Position the Entity was spawned at.
		/// </summary>
		Vector3 Position { get; }

		/// <summary>
		/// Rotation the Entity was spawned at.
		/// </summary>
		Quaternion Rotation { get; }

		/// <summary>
		/// The <see cref="GameObject"/> that represents the Entity in the engine.
		/// </summary>
		GameObject EntityGameObject { get; }
	}
}
