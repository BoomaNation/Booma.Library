using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public interface IEntityFactory
	{
		/// <summary>
		/// Creates an entity in the world.
		/// </summary>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>Details that represents the spawn result.</returns>
		IEntitySpawnResults TrySpawnEntity(EntityType entityType, Vector3 position, Quaternion rotation, ISpawnContext context);

		/// <summary>
		/// Creates a entity in the world.
		/// </summary>
		/// <returns>Details that represents the spawn result.</returns>
		IEntitySpawnResults TrySpawnEntity(EntityType entityType, ISpawnContext context);
	}
}
