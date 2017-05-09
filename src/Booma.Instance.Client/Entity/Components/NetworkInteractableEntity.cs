using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using Booma.Instance.NetworkObject;
using SceneJect.Common;
using GladBehaviour.Common;
using UnityEngine;
using Booma.Payloads.Instance;
using GladNet.Common;

namespace Booma.Instance.Client
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
