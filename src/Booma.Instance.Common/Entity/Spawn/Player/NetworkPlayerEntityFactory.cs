﻿using Booma;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Booma
{
	/// <summary>
	/// Factory that builds world representations for networked players.
	/// </summary>
	[Injectee]
	public class NetworkPlayerEntityFactory : SerializedMonoBehaviour, IPlayerEntityFactory
	{
		[Required]
		[SerializeField]
		private GameObject NetworkPlayerPrefab;

		/// <summary>
		/// Service that issues spawn points.
		/// </summary>
		[Required]
		[SerializeField]
		private ISpawnPointStrategy PlayerSpawnStrategy { get; }

		/// <summary>
		/// IoC/DI Managed <see cref="GameObject"/> factory.
		/// </summary>
		[Inject]
		private IGameObjectFactory GameobjectFactory { get; }

		/// <inheritdoc />
		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, IPlayerSpawnContext context)
		{
			GameObject entityObject = OnEntityConstruction(context, GameobjectFactory.CreateBuilder().With(context))
				.Create(NetworkPlayerPrefab, position, rotation);

			return OnEntityConstructionCompleted(context, new DefaultEntitySpawnDetails(entityObject));
		}

		protected virtual IGameObjectContextualBuilder OnEntityConstruction(IPlayerSpawnContext context, IGameObjectContextualBuilder builder)
		{
			//Access point that allows implementers of this type to extend the construction process.
			return builder;
		}

		protected virtual IEntitySpawnResults OnEntityConstructionCompleted(IPlayerSpawnContext context, IEntitySpawnResults result)
		{
			//Access point that allows implementers of this type to extend the construction process.
			return result;
		}

		/// <inheritdoc />
		public IEntitySpawnResults TrySpawnEntity(IPlayerSpawnContext context)
		{
			//Grabs a spawn point from the spawn point service.
			Transform spawnTransform = PlayerSpawnStrategy.GetSpawnpoint();

			return this.TrySpawnEntity(spawnTransform.position, spawnTransform.rotation, context);
		}

		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, Vector3 scale, IPlayerSpawnContext context)
		{
			IEntitySpawnResults results = TrySpawnEntity(position, rotation, context);

			if (results.Result == SpawnResult.Success)
				results.EntityGameObject.transform.localScale = scale;

			return results;
		}
	}
}
