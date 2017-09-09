using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using SceneJect.Common;
using UnityEngine;
using GladNet.Common;

namespace Booma
{
	public class NetworkInteractableEntity : NetworkMessageSender, IWorldInteractable
	{
		[SerializeField]
		private IEntityGuidContainer guidContainer;

		public bool TryInteract(NetworkEntityGuid entityInteracting)
		{
			//Send the interaction request for the server to handle.
			SendRequest(new EntityInteractionRequestPayload(guidContainer.NetworkGuid), DeliveryMethod.ReliableOrdered, false, 0);

			return true;
		}

		//TODO: Demo code
		void OnMouseDown()
		{
			TryInteract(null);
		}

		/// <inheritdoc />
		public bool TryInteract()
		{
			//Send the interaction request for the server to handle.
			SendRequest(new EntityInteractionRequestPayload(guidContainer.NetworkGuid), DeliveryMethod.ReliableOrdered, false, 0);

			return true;
		}
	}
}
