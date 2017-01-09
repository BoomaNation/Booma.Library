using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public class ServerNetworkDoor : NetworkDoor, IUnlockable
	{
		public void Unlock()
		{
			if (State != DoorState.Unlocked)
				State = DoorState.Unlocked;
			else
				OnDoorUnlocked?.Invoke();
		}

		protected override void HandleInitialState(DoorState state)
		{
			//We setup in editor to start as locked. So if the default is unlocked just unlock.
			if (state == DoorState.Unlocked)
				Unlock();
		}

		void Update()
		{
			Debug.Log($"Door State: {State.ToString()}");

			if (Input.GetKeyDown(KeyCode.A))
			{
				State = DoorState.Unlocked;
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
	}
}
