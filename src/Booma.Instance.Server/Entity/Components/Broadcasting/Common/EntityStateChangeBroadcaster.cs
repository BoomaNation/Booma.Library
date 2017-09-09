using GladNet.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma
{
	[Injectee]
	public class EntityStateChangeBroadcaster : NetworkMessageBroadcaster, IEntityStateListener
	{
		[Required]
		[SerializeField]
		private IEntityGuidContainer guidContainer;

#if !DEPLOY
		[Button("Init")]
		private void InitializationButton()
		{
			if(guidContainer == null)
				guidContainer = GetComponent(typeof(IEntityGuidContainer))
					as IEntityGuidContainer;
		}
#endif

		public void OnEntityStateChanged(byte value)
		{
			//Broadcasts the state change to all peers.
			this.MessageBroadcaster.SendEvent(new EntityStateChangedEvent(value, guidContainer.NetworkGuid, 0),
				DeliveryMethod.ReliableOrdered, true, 0);
		}
	}
}
