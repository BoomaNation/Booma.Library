using Booma.Instance.Common;
using Booma.Payloads.Instance;
using GladBehaviour.Common;
using GladNet.Common;
using GladNet.Engine.Common;
using SceneJect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Booma.Instance.Data;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Component that broadcasts its transform information.
	/// </summary>
	[Injectee]
	public class NetworkTransformBroadcaster : GladMonoBehaviour
	{
		//TODO: Maybe flags?
		public enum State
		{
			/// <summary>
			/// Initial default state.
			/// </summary>
			Disabled,

			/// <summary>
			/// State the broadcaster is in if it's broadcasting.
			/// </summary>
			Running,

			/// <summary>
			/// State the broadcaster is in if it has paused broadcasting.
			/// </summary>
			Paused,

			/// <summary>
			/// State the broadcaster is in if it is stopped.
			/// </summary>
			Stopped,
		}

		/// <summary>
		/// Tag/metdata that contains UID information.
		/// </summary>
		[SerializeField]
		public IEntityIdentifiable identifierTag;

		//TODO: Interest management
		//In the future we'll probably broadcast only to interested recievers (interest management)
		/// <summary>
		/// Player/Peer collection service. 
		/// </summary>
		[Inject]
		private readonly INetworkPlayerEntityCollection playerCollection;

		/// <summary>
		/// Indicates if broadcasting should start as soon as the broadcaster exists.
		/// </summary>
		[SerializeField]
		private bool startBroadcastingOnStart;

		[SerializeField]
		private float broadcastIntervalInSeconds = 0.1f;

		/// <summary>
		/// The transform that the entity should broadcast abou.
		/// </summary>
		[SerializeField]
		private readonly Transform entityTransform;

		/// <summary>
		/// Represents the current state of the broadcaster
		/// </summary>
		public State CurrentState { get; private set; }

		private WaitForSeconds cachedWaitTime;

		private void Start()
		{
			if (broadcastIntervalInSeconds <= 0.0f)
				throw new ArgumentOutOfRangeException(nameof(broadcastIntervalInSeconds), $"Declare {nameof(broadcastIntervalInSeconds)} must be greater than 0.");

			CurrentState = State.Disabled;
			cachedWaitTime = new WaitForSeconds(broadcastIntervalInSeconds);

			//Start a broadcast couroutine that sends the position to all peers
			//We use coroutines in Unity3D to avoid the needless overhead of constant Update calls

			if (startBroadcastingOnStart)
				StartCoroutine(BroadcastPosition());
		}

		private IEnumerator BroadcastPosition()
		{
			//Set the state as running to broadcast
			CurrentState = State.Running;

			//TODO: Handle all states
			while(CurrentState == State.Running)
			{
				if (playerCollection == null)
				{
					Debug.Log("Player collection was null.");

					yield return null;
					continue;
				}

				//TODO: Handle time
				EntityPositionUpdateEvent positionPacket = new EntityPositionUpdateEvent(entityTransform.position.ToSurrogate(), identifierTag.EntityId, 0);

				//Broadcast position
				//TODO: hide this iteration. Eventually we'll have broadcast support
				foreach (INetPeer peer in playerCollection.AllPeers())
					peer.TrySendMessage(OperationType.Event, positionPacket, DeliveryMethod.ReliableDiscardStale, false, 1); //TODO: Better channel handling

				yield return cachedWaitTime;
			}

			CurrentState = State.Stopped;
		}
	}
}
