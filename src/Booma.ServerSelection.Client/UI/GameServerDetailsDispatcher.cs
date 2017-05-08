using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.ServerSelection.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client.ServerSelection.Authentication
{
	public class GameServerDetailsDispatcher : MonoBehaviour
	{
		[Serializable]
		public class OnServerDetailsChangedEvent : UnityEvent<string> { }

		[SerializeField]
		private OnServerDetailsChangedEvent OnServerDetailsChanged;

		public void Dispatch(string name, ServerRegion region)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

			//TODO: Extract this into a formatter object
			OnServerDetailsChanged?.Invoke($"{region}/{name}");
		}
	}
}
