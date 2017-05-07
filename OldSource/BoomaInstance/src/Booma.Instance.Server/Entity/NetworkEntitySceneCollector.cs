using Booma.Entity.Identity;
using Booma.Entity.Prefab;
using Booma.Instance.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Component that collects all network entites in the scene.
	/// </summary>
	[Injectee]
	public class NetworkEntitySceneCollector : MonoBehaviour
	{
		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		private void Start()
		{
			//Find every gameobject in the scene that is tagged as an entity.
			foreach(GameObject go in Resources.FindObjectsOfTypeAll<GameObject>()
				.Where(go => go.scene != null && !String.IsNullOrEmpty(go.scene.name))
				.Where(go => go.GetComponent<IEntityGuidContainer>() != null)
				.Where(go => go.GetComponent<ITagProvider<GameObjectPrefab>>() != null))
			{
				IEntityGuidContainer guid = go.GetComponent<IEntityGuidContainer>();

				entityCollection[guid.NetworkGuid] = new EntityContainer(guid.NetworkGuid, go);

				Debug.Log($"Found Entity Name: {go.name} Id: {guid.NetworkGuid.EntityId}.");
			}
		}
	}
}
