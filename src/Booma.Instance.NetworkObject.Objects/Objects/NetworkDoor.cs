using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.NetworkObject
{
	/// <summary>
	/// Base Type for any Door networked <see cref="GameObject"/>.
	/// </summary>
	[Injectee]
	public abstract class NetworkDoor : NetworkGameObject<NetworkDoor.DoorState>
	{
		[SerializeField]
		[Tooltip("Event subscription for when the" + nameof(NetworkDoor) + "unlocks.")]
		private UnityEvent onDoorUnlocked;

		/// <summary>
		/// Event subscription list for a Door Opening Event.
		/// </summary>
		protected UnityEvent OnDoorUnlocked => onDoorUnlocked;

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
