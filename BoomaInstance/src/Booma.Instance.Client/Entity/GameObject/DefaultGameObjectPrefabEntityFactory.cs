using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Instance.Common;
using UnityEngine;
using SceneJect.Common;
using Booma.Entity.Prefab;

namespace Booma.Instance.Client
{
	[Injectee]
	public class DefaultGameObjectPrefabEntityFactory : MonoBehaviour, IGameObjectPrefabEntityFactory
	{
		//TODO: This is temp demo code
		[Serializable]
		public class PrefabPair
		{
			[SerializeField]
			public GameObjectPrefab PrefabId;

			[SerializeField]
			public GameObject PrefabReference;
		}

		[Inject]
		private readonly IGameObjectFactory gameobjectFactory;

		//TODO: This is a demo hack
		[SerializeField]
		private PrefabPair[] Prefabs;

		void Awake()
		{
			IEnumerable<PrefabPair> pairs = Prefabs.Distinct();

			if (Prefabs.Count() != pairs.Count())
				Debug.LogWarning($"The field {nameof(Prefabs)} has duplicates.");

			Prefabs = pairs.ToArray();
		}

		public IEntitySpawnResults TrySpawnEntity(IGameObjectPrefabSpawnContext context)
		{
			return TrySpawnEntity(Vector3.zero, Quaternion.identity, Vector3.zero, context);
		}

		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, IGameObjectPrefabSpawnContext context)
		{
			return TrySpawnEntity(position, rotation, Vector3.zero, context);
		}

		public IEntitySpawnResults TrySpawnEntity(Vector3 position, Quaternion rotation, Vector3 scale, IGameObjectPrefabSpawnContext context)
		{
			if (Prefabs.FirstOrDefault(x => x.PrefabId == context.PrefabId) == null)
				return DefaultEntitySpawnDetails.Fail(SpawnResult.PrefabUnavailable);

			 GameObject go = gameobjectFactory.CreateBuilder()
				.With(context)
				.Create(Prefabs.First(x => x.PrefabId == context.PrefabId)?.PrefabReference, position, rotation);

			go.transform.localScale = scale;

			return new DefaultEntitySpawnDetails(go);
		}
	}
}
