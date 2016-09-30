using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Engine.Server;
using UnityEngine;
using SceneJect.Common;
using Booma.Instance.Common;
using Booma.Payloads.Instance;
using Booma.Payloads.Surrogates.Unity;
using GladNet.Common;

namespace Booma.Instance.Server
{
	[Injectee]
	public class EntitySpawnBroadcaster : MonoBehaviour, IClientSessionEventListener
	{
		[Inject]
		private readonly IPlayerEntityCollection playerEntityCollection;

		public void OnEvent(ClientPeerSession session)
		{
			Debug.Log("Sending entity spawn events for other players.");

			//TODO: Send proper positions
			foreach (var kvp in playerEntityCollection)
				session.SendEvent(new PlayerSpawnEventPayload(kvp.Key, new Vector3Surrogate(0, 0, 0), new QuaternionSurrogate(0, 0, 0, 0), "TestName"), DeliveryMethod.ReliableUnordered, false, 0);
		}
	}
}