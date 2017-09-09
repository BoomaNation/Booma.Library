using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using UnityEngine;

namespace Booma
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
			if (StateContainer.State == DoorState.Locked)
				return;

			entityOccuptance++;
			foreach(Animator a in animationControllers)
				a.SetTrigger(DefaultDoorAnimationTrigger.Open.ToString());
		}

		public void OnTriggerExit(Collider other)
		{
			//If the door is locked we can do nothing.
			if (StateContainer.State == DoorState.Locked)
				return;

			//TODO: This will break if entities leave the world
			entityOccuptance--;

			if (entityOccuptance == 0)
				foreach (Animator a in animationControllers)
					a.SetTrigger(DefaultDoorAnimationTrigger.Close.ToString());
		}

		protected override void OnEntityStateChanged(DoorState newState)
		{
			//For now we can just call handle initial. May not work in the future.
			OnStart(newState);
		}

		/// <inheritdoc />
		protected override void OnStart(DoorState initialState)
		{
			//TODO: Handle both states.
			switch (StateContainer.State)
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
	}
}
