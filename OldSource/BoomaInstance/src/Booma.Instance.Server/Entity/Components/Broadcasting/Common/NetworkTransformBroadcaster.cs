using Booma.Entity.Identity;
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

namespace Booma.Instance.Server
{
	/// <summary>
	/// Component that broadcasts its transform information.
	/// </summary>
	[Injectee]
	public class NetworkTransformBroadcaster : NetworkMessageBroadcaster
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
		public IEntityGuidContainer networkGuidContainer;

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
		private Transform entityTransform;

		/// <summary>
		/// Represents the current state of the broadcaster
		/// </summary>
		public State CurrentState { get; private set; }

		private WaitForSeconds cachedWaitTime;

		protected override void Start()
		{
			base.Start();

			if (broadcastIntervalInSeconds <= 0.0f)
				throw new ArgumentOutOfRangeException(nameof(broadcastIntervalInSeconds), $"Declare {nameof(broadcastIntervalInSeconds)} must be greater than 0.");

			CurrentState = State.Disabled;
			cachedWaitTime = new WaitForSeconds(broadcastIntervalInSeconds);

			//Start a broadcast couroutine that sends the position to all peers
			//We use coroutines in Unity3D to avoid the needless overhead of constant Update calls

			if (this.networkGuidContainer.NetworkGuid == null)
				Debug.LogError("Network guid was null.");
			else
				Debug.Log($"{networkGuidContainer.NetworkGuid.RawGuidValue}");

			if (this.messageBroadcaster == null)
				Debug.LogError("Broadcaster null.");

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
				//TODO: Handle time
				EntityPositionUpdateEvent positionPacket = new EntityPositionUpdateEvent(entityTransform.position.ToSurrogate(), networkGuidContainer.NetworkGuid, 0);

				this.messageBroadcaster.SendEvent(positionPacket, DeliveryMethod.ReliableDiscardStale, false, 1);

				yield return cachedWaitTime;
			}

			CurrentState = State.Stopped;
		}
	}
}
