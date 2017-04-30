using Booma.Entity.Identity;
using Booma.Instance.Common;
using Booma.Payloads.Instance;
using GladNet.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Instance.NetworkObject;
using UnityEngine;

namespace Booma.Instance.Server
{
	[Injectee]
	public class EntityStateChangeBroadcaster : NetworkMessageBroadcaster, IEntityStateListener
	{
		[SerializeField]
		private IEntityGuidContainer guidContainer;

		public void OnEntityStateChanged(byte value)
		{
			//Broadcasts the state change to all peers.
			this.messageBroadcaster.SendEvent(new EntityStateChangedEvent(value, guidContainer.NetworkGuid, 0),
				DeliveryMethod.ReliableOrdered, true, 0);
		}
	}
}
