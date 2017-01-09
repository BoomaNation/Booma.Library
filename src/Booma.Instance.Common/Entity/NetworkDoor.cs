using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using SceneJect.Common;
using UnityEngine;
using Booma.Instance.Common;
using UnityEngine.Events;

namespace Booma.Instance.Common
{
	[Injectee]
	public abstract class NetworkDoor : NetworkGameObject<NetworkDoor.DoorState>
	{
		[SerializeField]
		protected UnityEvent OnDoorUnlocked;

		/// <summary>
		/// Enumeration of all states.
		/// </summary>
		public enum DoorState : byte
		{
			/// <summary>
			/// Unlocked state.
			/// </summary>
			Unlocked = 0,

			/// <summary>
			/// Locked state.
			/// </summary>
			Locked = 1,
		}
	}
}
