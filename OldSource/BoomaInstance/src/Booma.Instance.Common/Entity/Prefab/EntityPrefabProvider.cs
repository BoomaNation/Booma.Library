using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	//TODO: Implement handling for any prefab type. Not hardcoded.
	public class EntityPrefabProvider : MonoBehaviour, IEntityPrefabProvider
	{
		//TODO: Serialize a dictionary to the editor to setup prefab instances.
		[SerializeField]
		private GameObject LocalPlayerPrefab;

		//TODO: Serialize a dictionary to the editor to setup prefab instances.
		[SerializeField]
		private GameObject NetworkPlayerPrefab;

		public GameObject GetPrefab(PrefabType prefabType)
		{
			switch(prefabType)
			{
				case PrefabType.LocalPlayer:
					return LocalPlayerPrefab;
				case PrefabType.NetworkPlayer:
					return NetworkPlayerPrefab;
				default:
					throw new InvalidOperationException($"Failed to generate prefab for EntityType: {prefabType}.");
			}
		}
	}
}
