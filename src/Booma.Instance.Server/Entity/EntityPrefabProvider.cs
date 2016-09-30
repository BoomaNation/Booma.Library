using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Booma.Instance.Data;

namespace Booma.Instance.Server
{
	public class EntityPrefabProvider : ScriptableObject, IEntityPrefabProvider
	{
		[SerializeField]
		private GameObject PlayerPrefab;

		public GameObject GetPrefab(EntityType entityType)
		{
			if (entityType == EntityType.Player)
				return PlayerPrefab;
			else
				throw new InvalidOperationException($"Failed to generate prefab for EntityType: {entityType}.");
		}
	}
}
