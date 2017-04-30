using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.NetworkObject
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
		private UnityEvent OnDeactivated;

		[SerializeField]
		private UnityEvent OnPressed;

		/// <inheritdoc />
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


		public bool TryInteract([NotNull] NetworkEntityGuid entityInteracting)
		{
			if (entityInteracting == null) throw new ArgumentNullException(nameof(entityInteracting));

			//TODO: Distance checking.
			return TryInteract();
		}

		public bool TryInteract()
		{
			
			if (StateContainer.State != ButtonState.Activated)
			{
				StateContainer.State = ButtonState.Activated;

				return true;
			}
			else
				return false;
		}
	}
}
