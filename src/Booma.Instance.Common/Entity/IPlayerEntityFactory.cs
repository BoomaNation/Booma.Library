using Booma.Instance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public interface IPlayerEntityFactory
	{
		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="id">Id of the entity to create.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		GameObject CreatePlayerEntity(int id, Vector3 position, Quaternion rotation);

		/// <summary>
		/// Creates a player entity in the instance world.
		/// </summary>
		/// <param name="id">Id of the entity to create.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base player entity.</returns>
		GameObject CreatePlayerEntity(int id);
	}
}
