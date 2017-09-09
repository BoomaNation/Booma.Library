using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using UnityEngine;

namespace Booma
{
	public class ServerNetworkDoor : NetworkDoor, IUnlockable
	{
		/// <inheritdoc />
		public bool isLocked { get; }

		public void Unlock()
		{
			Debug.Log($"Door {nameof(Unlock)} called.");

			if (StateContainer.State != DoorState.Unlocked)
				StateContainer.State = DoorState.Unlocked;
			else
				OnDoorUnlocked?.Invoke();
		}

		void Update()
		{
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
			Debug.Log($"Door initial state {initialState}.");

			//We setup in editor to start as locked. So if the default is unlocked just unlock.
			if (StateContainer.State == DoorState.Unlocked)
				Unlock();
		}

		/// <inheritdoc />
		public void Unlock(NetworkEntityGuid entityInteracting)
		{
			if (entityInteracting != null)
			{
				Debug.Log($"Door {nameof(Unlock)} called by Entity: {entityInteracting.EntityId}.");
				Unlock();
			}
				
			
			//TODO: Implement entity handling
		}
	}
}
