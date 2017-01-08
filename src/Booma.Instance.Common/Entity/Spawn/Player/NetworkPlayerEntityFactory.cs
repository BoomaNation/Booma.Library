using Booma.Instance.Common;
using GladBehaviour.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Booma.Entity.Identity;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Factory that builds world representations for networked players.
	/// </summary>
	[Injectee]
	public class NetworkPlayerEntityFactory : GladMonoBehaviour, IPlayerEntityFactory
	{
		/// <summary>
		/// Service that produces prefabs.
		/// </summary>
		[SerializeField]
		private IEntityPrefabProvider prefabProvider;

		/// <summary>
		/// Service that issues spawn points.
		/// </summary>
		[SerializeField]
		private readonly ISpawnPointStrategy playerSpawnStrategy;

		/// <summary>
		/// IoC/DI Managed <see cref="GameObject"/> factory.
		/// </summary>
		[Inject]
		private readonly IGameObjectFactory gameobjectFactory;

		private void Start()
		{
			if (prefabProvider == null)
				throw new InvalidOperationException($"Set {nameof(prefabProvider)} in the inspector with the player entity prefab.");
		}

		public IEntitySpawnResults TrySpawnEntity(EntityType entityType, Vector3 position, Quaternion rotation, ISpawnContext context)
		{
			//We can only handle players
			if (entityType != EntityType.Player)
				return DefaultEntitySpawnDetails.Fail(SpawnResult.PrefabUnavailable);

			GameObject entityObject = gameobjectFactory.CreateBuilder()
				.With(context)
				.Create(prefabProvider.GetPrefab(PrefabType.NetworkPlayer), position, rotation);

			return new DefaultEntitySpawnDetails(entityObject);
		}

		public IEntitySpawnResults TrySpawnEntity(EntityType entityType, ISpawnContext context)
		{
			//Grabs a spawn point from the spawn point service.
			Transform spawnTransform = playerSpawnStrategy.GetSpawnpoint();

			return this.TrySpawnEntity(entityType, spawnTransform.position, spawnTransform.rotation, context);
		}
	}
}
