using Booma.Instance.Anim;
using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Client
{
	[RequireComponent(typeof(DoorEntityStateTag))]
	public class ClientNetworkDoor : NetworkDoor, IPhysicsTriggerable
	{
		//TODO: Implement a better animation system
		[SerializeField]
		private Animator[] animationControllers;

		int entityOccuptance = 0;

		public void OnTriggerEnter(Collider other)
		{
			//If the door is locked we can do nothing.
			if (this.State == DoorState.Locked)
				return;

			entityOccuptance++;
			foreach(Animator a in animationControllers)
				a.SetTrigger(DefaultDoorAnimationTrigger.Open.ToString());
		}

		public void OnTriggerExit(Collider other)
		{
			//If the door is locked we can do nothing.
			if (this.State == DoorState.Locked)
				return;

			//TODO: This will break if entities leave the world
			entityOccuptance--;

			if (entityOccuptance == 0)
				foreach (Animator a in animationControllers)
					a.SetTrigger(DefaultDoorAnimationTrigger.Close.ToString());
		}

		protected override void HandleInitialState(DoorState state)
		{
			//TODO: Handle both states.
			switch (state)
			{
				case DoorState.Unlocked:
					this.OnDoorUnlocked?.Invoke();
					foreach (Animator a in animationControllers)
						a.SetTrigger(DefaultDoorAnimationTrigger.Unlock.ToString());
					break;
				case DoorState.Locked:
					foreach (Animator a in animationControllers)
						a.SetTrigger(DefaultDoorAnimationTrigger.Lock.ToString());
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
