using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Common.ServerSelection;
using UnityEngine.UI;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	public class GameServerDetailsButtonFactory : MonoBehaviour, IGameServerDetailsGameObjectFactory
	{
		[SerializeField]
		private GameObject DetailsPrefab;

		[SerializeField]
		private GameObject RootDetailsObject;

		[SerializeField]
		private int SpaceInbetweenButtons;

		private int buttonCount = 0;

		private void Start()
		{
			if (SpaceInbetweenButtons < 1)
				throw new ArgumentOutOfRangeException(nameof(SpaceInbetweenButtons), $"Space must be positive.");

			if(DetailsPrefab == null)
				throw new InvalidOperationException($"{nameof(DetailsPrefab)} for the {nameof(GameServerDetailsButtonFactory)} must not be null.");

			if(RootDetailsObject == null)
				throw new InvalidOperationException($"{nameof(RootDetailsObject)} for the {nameof(GameServerDetailsButtonFactory)} must not be null.");
		}

		public GameObject Create(string name, ServerRegion region)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("Value cannot be null or empty.", nameof(name));

			GameObject newButton = GameObject.Instantiate(DetailsPrefab, DetailsPrefab.transform.position, DetailsPrefab.transform.rotation) as GameObject;
			newButton.SetActive(true);

			//Make it a child
			newButton.transform.parent = RootDetailsObject.transform;

			//Shift it down so buttons don't overlap
			newButton.transform.position = new Vector3(newButton.transform.position.x, newButton.transform.position.y + buttonCount * SpaceInbetweenButtons, newButton.transform.position.z);

			GameServerDetailsDispatcher detailsDispatcher = newButton.GetComponent<GameServerDetailsDispatcher>();

			if (detailsDispatcher == null)
				throw new InvalidOperationException($"The {nameof(DetailsPrefab)} should contain a {nameof(GameServerDetailsDispatcher)} component.");

			detailsDispatcher.Dispatch(name, region);

			buttonCount++;

			return newButton;
		}
	}
}
