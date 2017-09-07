using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public static class IEntityFactoryExtensions
	{
		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		public static IEntitySpawnResults SpawnPlayerEntity<TEntityFactory, TContextType>(this TEntityFactory factory, Vector3 position, Quaternion rotation, TContextType context)
			where TEntityFactory : IEntityFactory<TContextType>
			where TContextType : ISpawnContext
		{
			return factory.TrySpawnEntity(position, rotation, context);
		}

		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		public static IEntitySpawnResults SpawnPlayerEntity<TEntityFactory, TContextType>(this TEntityFactory factory, TContextType context)
			where TEntityFactory : IEntityFactory<TContextType>
			where TContextType : ISpawnContext
		{
			return factory.TrySpawnEntity(context);
		}
	}
}
