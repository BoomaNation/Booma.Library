using Booma.Instance.Anim;
using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Client
{
	public class ClientNetworkDoor : NetworkDoor, IPhysicsTriggerable
	{
		[SerializeField]
		private Animator animationController;

		int entityOccuptance = 0;

		public void OnTriggerEnter(Collider other)
		{
			//If the door is locked we can do nothing.
			if (this.State == DoorState.Locked)
				return;

			entityOccuptance++;
			animationController.SetTrigger(DefaultDoorAnimationTrigger.Open.ToString());
		}

		public void OnTriggerExit(Collider other)
		{
			//If the door is locked we can do nothing.
			if (this.State == DoorState.Locked)
				return;

			//TODO: This will break if entities leave the world
			entityOccuptance--;

			if (entityOccuptance == 0)
				animationController.SetTrigger(DefaultDoorAnimationTrigger.Close.ToString());
		}

		protected override void HandleInitialState(DoorState state)
		{
			//TODO: Handle both states.
			switch (state)
			{
				case DoorState.Unlocked:
					this.OnDoorUnlocked?.Invoke();
					animationController.SetTrigger(DefaultDoorAnimationTrigger.Unlock.ToString());
					break;
				case DoorState.Locked:
					animationController.SetTrigger(DefaultDoorAnimationTrigger.Lock.ToString());
					break;
				default:
					break;
			}
		}

		protected override void OnEntityStateChanged(DoorState newState)
		{
			//For now we can just call handle initial. May not work in the future.
			HandleInitialState(newState);
		}
	}
}
