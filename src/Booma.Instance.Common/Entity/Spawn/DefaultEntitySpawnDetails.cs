using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Basic or default implementiation of <see cref="IEntitySpawnDetails"/>.
	/// </summary>
	public class DefaultEntitySpawnDetails : IEntitySpawnDetails
	{
		/// <summary>
		/// Unique Identifier of the Entity.
		/// </summary>
		public int EntityId { get; }

		/// <summary>
		/// Position the Entity was spawned at.
		/// </summary>
		public Vector3 Position { get; }

		/// <summary>
		/// Rotation the Entity was spawned at.
		/// </summary>
		public Quaternion Rotation { get; }

		/// <summary>
		/// The <see cref="GameObject"/> that represents the Entity in the engine.
		/// </summary>
		public GameObject EntityGameObject { get; }

		public DefaultEntitySpawnDetails(int id, Vector3 position, Quaternion rotation, GameObject gameObject)
		{
			if (gameObject == null)
				throw new ArgumentNullException(nameof(gameObject), $"Provided {nameof(gameObject)} {nameof(GameObject)} argument must not be null.");

			EntityId = id;
			Position = position;
			Rotation = rotation;
			EntityGameObject = gameObject;
		}
	}
}
