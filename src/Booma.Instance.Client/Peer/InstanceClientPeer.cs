using Booma.Client.Network.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladNet.Serializer;
using GladNet.Message;
using Booma.Payloads.Instance;
using UnityEngine;
using System.Collections;

namespace Booma.Instance.Client
{
	public class InstanceClientPeer : BoomaNetworkClientPeer<InstanceClientPeer>
	{
		public override void OnStatusChanged(NetStatus status)
		{
			//TODO: Implement status handling
			Debug.Log(status);

			switch (status)
			{
				case NetStatus.Connecting:
					break;
				case NetStatus.Connected:
					Debug.Log("Sending spawn request.");
					//On connect request a spawn in the world
					this.SendRequest(new PlayerSpawnRequestPayload(), DeliveryMethod.ReliableOrdered);
					break;
				case NetStatus.EncryptionEstablished:
					break;
				case NetStatus.Disconnecting:
					break;
				case NetStatus.Disconnected:
					break;
				default:
					break;
			}
		}

		//For testing
		public IEnumerator PollNetwork()
		{
			yield return new WaitForSeconds(0.1f);

			while(true)
			{
				yield return new WaitForSeconds(0.1f);
				Poll();
			}
		}

		public void ConnectToServer()
		{
			if (Connect())
				StartCoroutine(PollNetwork());
			else
				throw new InvalidOperationException($"Failed to connect.");
		}

		public override void RegisterPayloadTypes(ISerializerRegistry registry)
		{
			registry.Register(typeof(NetworkMessage));
			registry.Register(typeof(EntitySpawnEventPayload));
			registry.Register(typeof(PlayerSpawnResponsePayload));
			registry.Register(typeof(PlayerSpawnRequestPayload));
			registry.Register(typeof(EntityPositionUpdateEvent));
			registry.Register(typeof(PlayerMoveRequestPayload));
			registry.Register(typeof(EntityInteractionRequestPayload));
			registry.Register(typeof(EntityStateChangedEvent));
		}
	}
}
