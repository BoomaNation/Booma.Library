﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet.Message;
using SceneJect.Common;
using GladNet.Engine.Common;
using GladNet.Common;
using UnityEngine;
using Booma;
using GladNet.Engine.Server;
using Unitysync.Async;

namespace Booma
{
	/// <summary>
	/// Abstract implementation for the logic 
	/// </summary>
	[Injectee]
	public abstract class ClaimSessionRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, ClaimSessionRequestPayload>
	{
		/// <summary>
		/// Factory service for the player entities.
		/// </summary>
		[Inject]
		private IPlayerEntityFactory PlayerEntityFactory { get; }

		protected sealed override void OnIncomingHandlableMessage(IRequestMessage message, ClaimSessionRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			TryClaimSession(payload.SessionClaimGuid, peer.PeerDetails.RemoteIP.ToString())
				.UnityAsyncContinueWith(this, g => OnSessionClaimResponse(g, peer));
		}

		/// <summary>
		/// Dispatched when a guid is generated.
		/// </summary>
		/// <typeparam name="TPeer">The peer type.</typeparam>
		/// <param name="response"></param>
		/// <param name="peer">The peer.</param>
		protected virtual void OnSessionClaimResponse<TPeer>(ServerSessionClaimResponse response, TPeer peer)
			where TPeer : INetPeer, IResponsePayloadSender
		{
			//TODO: Logging
			//TODO: Should we disconnect them? Should we just prevent them from sending more requests?
			//If the session inquiry failed then we should fail the session claim request.
			if(!response.isSuccessful)
			{
				peer.SendResponse(new ClaimSessionResponsePayload(PlayerSpawnResponseCode.Unknown), DeliveryMethod.ReliableUnordered, true, 0);
				return;
			}

			NetworkEntityGuid guid = NetworkEntityGuidBuilder.New()
				.WithId(response.UserId)
				.WithType(EntityType.Player)
				.Build();

			IEntitySpawnResults details = PlayerEntityFactory.SpawnPlayerEntity(new NetworkPlayerSpawnContext(guid, peer));

			if(details.Result != SpawnResult.Success)
				throw new InvalidOperationException($"Failed to create Entity for {peer}. Failure: {details.Result}");

			//TODO: Clean this up
			Vector3Surrogate pos = details.EntityGameObject.transform.position.ToSurrogate();

			QuaternionSurrogate rot = details.EntityGameObject.transform.rotation.ToSurrogate();

			//Send the response to the player who requested to spawn
			peer.SendResponse(new ClaimSessionResponsePayload(pos, rot, guid), DeliveryMethod.ReliableUnordered, true, 0);
		}

		/// <summary>
		/// Creates the entity GUID.
		/// Can be overriden for special GUID handling.
		/// </summary>
		/// <param name="sessionGuid">The session GUID.</param>
		/// <returns></returns>
		protected abstract Task<ServerSessionClaimResponse> TryClaimSession(Guid sessionGuid, string ip);
	}
}
