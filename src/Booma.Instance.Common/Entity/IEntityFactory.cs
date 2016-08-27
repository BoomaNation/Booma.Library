using Booma.Instance.Data;
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
		/// Creates an entity in the instance world.
		/// </summary>
		/// <param name="id">Id of the entity to create.</param>
		/// <param name="type">Type of entity to create.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <returns>A gameobject that represents the base-entity.</returns>
		GameObject CreateEntity(int id, EntityType type, Vector3 position, Quaternion rotation);
	}
}
