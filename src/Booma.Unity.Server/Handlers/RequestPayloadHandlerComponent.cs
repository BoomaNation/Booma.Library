using Common.Logging;
using GladNet.Engine.Common;
using GladNet.Message;
using GladNet.Message.Handlers;
using GladNet.Payload;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	/// <summary>
	/// Scene-based <see cref="IRequestMessageHandler{TPeerType}"/>
	/// </summary>
	/// <typeparam name="TPeerType">Type of peer this event was recieved on.</typeparam>
	/// <typeparam name="TPayloadType">Type of payload that this handler can handle.</typeparam>
	[Injectee]
	public abstract class RequestPayloadHandlerComponent<TPeerType, TPayloadType> : MonoBehaviour, IRequestMessageHandler<TPeerType>, IClassLogger
		where TPeerType : INetPeer
		where TPayloadType : PacketPayload
	{
		/// <summary>
		/// Logging service for handlers.
		/// </summary>
		[Inject]
		public ILog Logger { get; private set; }

		/// <summary>
		/// Handles the <see cref="IRequestMessage"/> and specified <typeparamref name="TPayloadType"/>.
		/// </summary>
		/// <param name="payload">Packet payload to be handled.</param>
		/// <param name="parameters">Message parameters (null with Photon)</param>
		/// <param name="peer">The peer that recieved this <see cref="TPayloadType"/> payload.</param>
		protected abstract void OnIncomingHandlableMessage(IRequestMessage message, TPayloadType payload, IMessageParameters parameters, TPeerType peer);

		public bool TryProcessMessage(IRequestMessage message, IMessageParameters parameters, TPeerType peer)
		{
			TPayloadType payload = message.Payload.Data as TPayloadType;

			//if it's the not the payload type this handler handles then we
			//indicate non-consumption
			if (payload == null)
				return false;
			else
				OnIncomingHandlableMessage(message, payload, parameters, peer);

			//If an exception wasn't thrown we'll be indicating that the payload has been consumed.
			return true;
		}
	}
}
