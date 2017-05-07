using Booma.Entity.Identity;
using Booma.Payloads.Common;
using Booma.Payloads.Surrogates.Unity;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// Payload sent by players who want to move to a new position.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.EntityInteractionRequest)]
	public class EntityInteractionRequestPayload : PacketPayload
	{
		/// <summary>
		/// Represents the new position the player wants to move to.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public NetworkEntityGuid NetworkGuid { get; private set; }

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.PlayerMoveRequestPayload"/> payload.
		/// </summary>
		public EntityInteractionRequestPayload(NetworkEntityGuid guidOfEntityToInteractWith)
		{
			//TODO: Check refs
			NetworkGuid = guidOfEntityToInteractWith;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected EntityInteractionRequestPayload()
		{

		}
	}
}
