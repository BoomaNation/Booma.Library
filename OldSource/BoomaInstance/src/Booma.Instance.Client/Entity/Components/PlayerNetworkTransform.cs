using Booma.Payloads.Instance;
using GladNet.Common;
using SceneJect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Client
{
	[Injectee]
	public class PlayerNetworkTransform : NetworkMessageSender
	{
		[SerializeField]
		private Transform playerTransform;

		[SerializeField]
		private float sendsPerSeconds = 1.0f;

		private WaitForSeconds cachedWaitTime;

		void Awake()
		{
			if (playerTransform == null)
				throw new InvalidOperationException($"Transform must not be null in {nameof(PlayerNetworkTransform)}.");

			cachedWaitTime = new WaitForSeconds(sendsPerSeconds);
		}

		void Start()
		{
			StartCoroutine(BroadcastPosition());
		}

		private IEnumerator BroadcastPosition()
		{
			//TODO: Implement state machine
			while (true)
			{
				//TODO: Handle time
				PlayerMoveRequestPayload positionPacket = new PlayerMoveRequestPayload(playerTransform.position.ToSurrogate(), 0);

				this.SendRequest(positionPacket, DeliveryMethod.ReliableDiscardStale, false, 0);

				yield return cachedWaitTime;
			}
		}
	}
}
