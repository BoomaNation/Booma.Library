using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.Common
{
	public abstract class NetworkButton : NetworkGameObject<NetworkButton.ButtonState>, IWorldInteractable
	{
		/// <summary>
		/// State for all network buttons.
		/// </summary>
		public enum ButtonState
		{
			/// <summary>
			/// Represents an activated (or pressed) state.
			/// </summary>
			Activated = 0,

			/// <summary>
			/// Represents a deactivated (or unpressed) state.
			/// </summary>
			Deactivated = 1,
		}

		[SerializeField]
		private UnityEvent OnPressed;

		[SerializeField]
		private UnityEvent OnDeactivated;

		protected override void OnEntityStateChanged(ButtonState newState)
		{
			switch (newState)
			{
				case ButtonState.Activated:
					OnPressed?.Invoke();
					break;
				case ButtonState.Deactivated:
					OnDeactivated?.Invoke();
					break;

					//TODO: Log
				default:
					break;
			}
		}

		public bool TryInteract(NetworkEntityGuid entityInteracting)
		{
			//TODO: Distance checking.
			if (this.State != ButtonState.Activated)
			{
				State = ButtonState.Activated;

				return true;
			}
			else
				return false;
		}
	}
}
