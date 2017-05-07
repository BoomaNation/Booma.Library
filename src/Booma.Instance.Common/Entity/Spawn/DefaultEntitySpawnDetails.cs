using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Basic or default implementiation of <see cref="IEntitySpawnResult"/>.
	/// </summary>
	public class DefaultEntitySpawnDetails : IEntitySpawnResults
	{
		/// <summary>
		/// The <see cref="GameObject"/> that represents the Entity in the engine.
		/// </summary>
		public GameObject EntityGameObject { get; }

		/// <summary>
		/// Indicates the result of the spawn.
		/// </summary>
		public SpawnResult Result { get; }

		public static DefaultEntitySpawnDetails Fail(SpawnResult reason)
		{
			//If the result failed
			return new DefaultEntitySpawnDetails(reason);
		}

		//Force use through Fail
		protected DefaultEntitySpawnDetails(SpawnResult failureReason)
		{
			if (failureReason == SpawnResult.Success)
				throw new ArgumentException($"Provided {nameof(SpawnResult)} was successful but should have been a failure.");

			Result = failureReason;
		}

		public DefaultEntitySpawnDetails(GameObject gameObject)
		{
			if (gameObject == null)
				throw new ArgumentNullException(nameof(gameObject), $"Provided {nameof(gameObject)} {nameof(GameObject)} argument must not be null.");

			EntityGameObject = gameObject;

			//alaways success if provided game object
			Result = SpawnResult.Success;
		}
	}
}
