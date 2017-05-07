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

			GameObject newButton = GameObject.Instantiate(DetailsPrefab, RootDetailsObject.transform) as GameObject;
			newButton.SetActive(true);

			GameServerDetailsDispatcher detailsDispatcher = newButton.GetComponent<GameServerDetailsDispatcher>();

			newButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * buttonCount * SpaceInbetweenButtons);
			if (detailsDispatcher == null)
				throw new InvalidOperationException($"The {nameof(DetailsPrefab)} should contain a {nameof(GameServerDetailsDispatcher)} component.");

			detailsDispatcher.Dispatch(name, region);

			buttonCount++;

			return newButton;
		}
	}
}
