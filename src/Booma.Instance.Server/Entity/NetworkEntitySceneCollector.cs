﻿using Booma;
using SceneJect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	/// <summary>
	/// Component that collects all network entites in the scene.
	/// </summary>
	[Injectee]
	public class NetworkEntitySceneCollector : MonoBehaviour
	{
		[Inject]
		private NetworkEntityCollection EntityCollection { get; }

		private void Start()
		{
			//We use a coroutine to ensure that initialization is fully complete before we collect.
			StartCoroutine(AddNetworkedObjects());
		}

		public IEnumerator AddNetworkedObjects()
		{
			yield return new WaitForFixedUpdate();

			//TODO: Check if already in collection
			//Find every gameobject in the scene that is tagged as an entity.
			foreach(GameObject go in Resources.FindObjectsOfTypeAll<GameObject>()
				.Where(go => go.scene != null && !String.IsNullOrEmpty(go.scene.name))
				.Where(go => go.GetComponent<IEntityGuidContainer>() != null))
			{
				IEntityGuidContainer guid = go.GetComponent<IEntityGuidContainer>();

				EntityCollection[guid.NetworkGuid] = new EntityContainer(guid.NetworkGuid, go);

				Debug.Log($"Found Entity Name: {go.name} Id: {guid.NetworkGuid.EntityId}.");
			}
		}
	}
}
