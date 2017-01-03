using Booma.Instance.Common;
using Booma.Instance.Data;
using Booma.Instance.Server;
using GladBehaviour.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	[Injectee]
	public class NetworkPlayerEntityFactory : GladMonoBehaviour, IPlayerEntityFactory
	{
		[SerializeField]
		private IEntityPrefabProvider prefabProvider;

		[SerializeField]
		private readonly ISpawnPointStrategy playerSpawnStrategy;

		[Inject]
		private readonly INetworkPlayerEntityCollection playerEntityCollection;

		private void Start()
		{
			if (prefabProvider == null)
				throw new InvalidOperationException($"Set {nameof(prefabProvider)} in the inspector with the player entity prefab.");
		}

		public IEntitySpawnDetails SpawnPlayerEntity(int id, Vector3 position, Quaternion rotation)
		{
			GameObject playerGo = GameObject.Instantiate(prefabProvider.GetPrefab(EntityType.Player), position, rotation) as GameObject;

			//Once created we should add the entity to the server player entity collection.
			playerEntityCollection.Add(id, playerGo);

			return new DefaultEntitySpawnDetails(id, position, rotation, PostProcessEntityGameObject(playerGo, id, EntityType.Player));
		}

		public GameObject PostProcessEntityGameObject(GameObject playerGameObject, int id, EntityType type)
		{
			NetworkEntityIdentifierTag identifierComponent = playerGameObject.GetComponent<NetworkEntityIdentifierTag>();

			//Initialize the component that contains the info about the entity.
			identifierComponent.Initialize(id, type);

			return playerGameObject;
		}

		public IEntitySpawnDetails SpawnPlayerEntity(int id)
		{
			Transform spawnTransform = playerSpawnStrategy.GetSpawnpoint();

			if (spawnTransform == null)
				throw new InvalidOperationException($"Unable to produce {nameof(Transform)} from {nameof(ISpawnPointStrategy)}.");

			return SpawnPlayerEntity(id, spawnTransform.position, spawnTransform.rotation);
		}
	}
}
