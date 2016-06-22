using Common.Logging;
using GladLive.Common;
using GladNet.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Client.Network.Common
{
	/// <summary>
	/// Scene-based <see cref="IResponsePayloadHandler{TPeerType, TPayloadType}"/>
	/// </summary>
	/// <typeparam name="TPeerType">Type of peer this response was recieved on.</typeparam>
	/// <typeparam name="TPayloadType">Type of payload that this handler can handle.</typeparam>
	public abstract class ResponsePayloadHandlerComponent<TPeerType, TPayloadType> : MonoBehaviour, IResponsePayloadHandler<TPeerType, TPayloadType>, IClassLogger
		where TPayloadType : PacketPayload
		where TPeerType : INetPeer
	{
		/// <summary>
		/// Logging service for handlers.
		/// </summary>
		[Inject]
		public ILog Logger { get; private set; }

		/// <summary>
		/// Attempts to handle the loosely-typed payload.
		/// Will return false if <see cref="PacketPayload"/> isn't a <typeparamref name="TPayloadType"/>.
		/// </summary>
		/// <param name="payload">Packet payload to be handled.</param>
		/// <param name="parameters">Message parameters (null with Photon)</param>
		/// <param name="peer">The peer that recieved this <see cref="PacketPayload"/>.</param>
		/// <returns>True if the payload was handled. False if it was not.</returns>
		public bool TryProcessPayload(PacketPayload payload, IMessageParameters parameters, TPeerType peer)
		{
			//Just try to as cast it and chek null in the other
			return TryProcessPayload(payload as TPayloadType, parameters, peer);
		}

		/// <summary>
		/// Attempts to handle the <typeparamref name="TPayloadType"/>.
		/// </summary>
		/// <param name="payload">Packet payload to be handled.</param>
		/// <param name="parameters">Message parameters (null with Photon)</param>
		/// <param name="peer">The peer that recieved this <see cref="TPayloadType"/> payload.</param>
		/// <returns>True if the payload was handled. False if it was not.</returns>
		public bool TryProcessPayload(TPayloadType payload, IMessageParameters parameters, TPeerType peer)
		{
			if (payload == null)
				return false;
			else
				HandleNonNullStronglyTypedPayload(payload, parameters, peer);

			//at this point it SHOULD have been handled since we can handle this type
			return true;
		}

		/// <summary>
		/// Handlesthe <typeparamref name="TPayloadType"/>.
		/// </summary>
		/// <param name="payload">Packet payload to be handled.</param>
		/// <param name="parameters">Message parameters (null with Photon)</param>
		/// <param name="peer">The peer that recieved this <see cref="TPayloadType"/> payload.</param>
		protected abstract void HandleNonNullStronglyTypedPayload(TPayloadType payload, IMessageParameters parameters, TPeerType peer);
	}
}
