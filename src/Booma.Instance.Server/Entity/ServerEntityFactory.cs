using Booma.Instance.Common;
using Booma.Instance.Data;
using GladNet.Engine.Server;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	[Injectee]
	public class ServerEntityFactory : MonoBehaviour, IEntityFactory
	{
		//TODO: Handle multiple entities by creating a prefab lookup service
		[SerializeField]
		private GameObject playerEntityPrefab;

		[Inject]
		private readonly PlayerEntityCollection playerEntityCollection;

		//TODO: Remove defaults; Have client send desired location. Check bounds.
		//Mostly here for testing purposes
		[SerializeField]
		private Vector3 defaultEntityPosition;

		[SerializeField]
		private Quaternion defaultEntityRotation;

		private void Start()
		{
			if (playerEntityPrefab == null)
				throw new InvalidOperationException($"Set {nameof(playerEntityPrefab)} in the inspector with the player entity prefab.");
		}

		//Target for UnityEvent: OnNewSessionCreated
		public void CreateEntityFromSession(ClientPeerSession session)
		{
			CreateEntity(session.PeerDetails.ConnectionID, EntityType.Player);
		}

		public GameObject CreateEntity(int id, EntityType type, Vector3 position, Quaternion rotation)
		{
			if (playerEntityCollection.ContainsKey(id))
				throw new InvalidOperationException($"The entity ID: {id} is already assigned to an entity. Cannot share IDs.");

			GameObject playerGo = GameObject.Instantiate(playerEntityPrefab) as GameObject;

			//TODO: Handle entity collections differently with multiple entity types
			playerEntityCollection.Add(id, playerGo);

			return PostProcessGameObject(playerGo, id, type);
		}

		public GameObject CreateEntity(int id, EntityType type)
		{
			return CreateEntity(id, type, defaultEntityPosition, defaultEntityRotation);
		}

		public GameObject PostProcessGameObject(GameObject playerGameObject, int id, EntityType type)
		{
			EntityIdentifier identifierComponent = playerGameObject.GetComponent<EntityIdentifier>();

			//Initialize the component that contains the info about the entity.
			identifierComponent.Initialize(id, type);

			return playerGameObject;
		}
	}
}
