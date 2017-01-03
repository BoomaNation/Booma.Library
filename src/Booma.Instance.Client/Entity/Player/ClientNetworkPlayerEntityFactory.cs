using Booma.Instance.Common;
using Booma.Instance.Data;
using GladBehaviour.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Client
{
	[Injectee]
	public class ClientNetworkPlayerEntityFactory : GladMonoBehaviour, IPlayerEntityFactory
	{
		[SerializeField]
		private IEntityPrefabProvider prefabProvider;

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

			//Once created we should add the entity to player collection
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
			//We don't have spawn points so if a spawn is requested with just ID use defaults
			return SpawnPlayerEntity(id, Vector3.zero, Quaternion.identity);
		}
	}
}
