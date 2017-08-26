using Booma.Entity.Identity;
using Booma.Instance.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Instance.Client
{
	[Injectee]
	public class ClientNetworkPlayerEntityFactory : SerializedMonoBehaviour, IPlayerEntityFactory
	{
		[SerializeField]
		private IEntityPrefabProvider prefabProvider;

		[Inject]
		private readonly IGameObjectFactory gameobjectFactory;

		private void Start()
		{
			if (prefabProvider == null)
				throw new InvalidOperationException($"Set {nameof(prefabProvider)} in the inspector with the player entity prefab.");
		}

		public IEntitySpawnResults SpawnPlayerEntity(int id, Vector3 position, Quaternion rotation)
		{
			GameObject playerGo = GameObject.Instantiate(prefabProvider.GetPrefab(PrefabType.NetworkPlayer), position, rotation) as GameObject;

			return new DefaultEntitySpawnDetails(playerGo);
		}

		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, ISpawnContext context)
		{
			GameObject playerGo = gameobjectFactory.CreateBuilder()
				.With(context)
				.Create(prefabProvider.GetPrefab(PrefabType.NetworkPlayer), position, rotation) as GameObject;

			return new DefaultEntitySpawnDetails(playerGo);
		}

		public IEntitySpawnResults TrySpawnEntity(ISpawnContext context)
		{
			//We don't have spawn points so if a spawn is requested with just ID use defaults
			return TrySpawnEntity(Vector3.zero, Quaternion.identity, context);
		}

		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, Vector3 scale, ISpawnContext context)
		{
			IEntitySpawnResults results = TrySpawnEntity(position, rotation, context);

			if (results.Result == SpawnResult.Success)
				results.EntityGameObject.transform.localScale = scale;

			return results;
		}
	}
}
