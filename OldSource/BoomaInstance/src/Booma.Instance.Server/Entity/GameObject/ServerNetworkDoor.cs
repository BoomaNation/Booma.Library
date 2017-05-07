using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using Booma.Instance.NetworkObject;
using UnityEngine;

namespace Booma.Instance.Server
{
	[RequireComponent(typeof(DoorEntityStateTag))] //require this state tag
	public class ServerNetworkDoor : NetworkDoor, IUnlockable
	{
		/// <inheritdoc />
		public bool isLocked { get; }

		public void Unlock()
		{
			if (StateContainer.State != DoorState.Unlocked)
				StateContainer.State = DoorState.Unlocked;
			else
				OnDoorUnlocked?.Invoke();
		}

		void Update()
		{
			Debug.Log($"Door State: {StateContainer.State.ToString()}");

			if (Input.GetKeyDown(KeyCode.A))
			{
				StateContainer.State = DoorState.Unlocked;
				Debug.Log("Unlocked door.");
			}
		}

		protected override void OnEntityStateChanged(DoorState newState)
		{
			switch (newState)
			{
				case DoorState.Unlocked:
					Unlock();
					break;
				case DoorState.Locked:
					break;
				default:
					break;
			}
		}

		/// <inheritdoc />
		protected override void OnStart(DoorState initialState)
		{
			//We setup in editor to start as locked. So if the default is unlocked just unlock.
			if (StateContainer.State == DoorState.Unlocked)
				Unlock();
		}

		/// <inheritdoc />
		public void Unlock(NetworkEntityGuid entityInteracting)
		{
			if(entityInteracting == null)
				Unlock();
			
			//TODO: Implement entity handling
		}
	}
}
