using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
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
		[GladNetMember(1)]
		public NetworkEntityGuid NetworkGuid { get; private set; }

		/// <summary>
		/// Creates a new <see cref="PlayerMoveRequestPayload"/> payload.
		/// </summary>
		public EntityInteractionRequestPayload(NetworkEntityGuid guidOfEntityToInteractWith)
		{
			if (guidOfEntityToInteractWith == null) throw new ArgumentNullException(nameof(guidOfEntityToInteractWith));

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
