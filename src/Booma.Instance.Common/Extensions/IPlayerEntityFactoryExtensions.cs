using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public static class IPlayerEntityFactoryExtensions
	{
		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		public static IEntitySpawnResults SpawnPlayerEntity(this IPlayerEntityFactory factory, Vector3 position, Quaternion rotation, ISpawnContext context)
		{
			return factory.TrySpawnEntity(position, rotation, context);
		}

		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		public static IEntitySpawnResults SpawnPlayerEntity(this IPlayerEntityFactory factory, ISpawnContext context)
		{
			return factory.TrySpawnEntity(context);
		}
	}
}
