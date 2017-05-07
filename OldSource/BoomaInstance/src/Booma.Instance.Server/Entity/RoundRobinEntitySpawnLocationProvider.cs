using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public class RoundRobinEntitySpawnLocationProvider : MonoBehaviour, ISpawnPointStrategy
	{
		/// <summary>
		/// Spawn points for the entity.
		/// </summary>
		[Tooltip("Collection of spawnpoints for entities.")]
		[SerializeField]
		private Transform[] SpawnPointTransforms;

		/// <summary>
		/// Used for the simple round-robin strategy.
		/// </summary>
		private int internalCounter = 0;

		void Awake()
		{
			if (SpawnPointTransforms == null)
				throw new InvalidOperationException($"{nameof(SpawnPointTransforms)} must not be null.");

			if (!SpawnPointTransforms.Any())
				throw new InvalidOperationException($"{nameof(SpawnPointTransforms)} must not be empty.");
		}

		//Not threadsafe but doesn't have to be
		public Transform GetSpawnpoint()
		{
			if (SpawnPointTransforms.Count() > internalCounter)
				return SpawnPointTransforms[internalCounter++]; //increment after access
			else
				return SpawnPointTransforms[(internalCounter = 0)]; //set counter to 0
		}
	}
}
