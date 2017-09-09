using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// Payload sent by players who want to spawn in an instance.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.ClaimSessionRequest)]
	public class ClaimSessionRequestPayload : PacketPayload
	{
		//WARNING: Don't rely on ONLY the guid. You need to check IP as well and timeframe. Otherwise sessions can be stolen.
		/// <summary>
		/// The unique GUID that is able to claim a session.
		/// </summary>
		[GladNetMember(1)]
		public Guid SessionClaimGuid { get; private set; }

		public ClaimSessionRequestPayload(Guid sessionClaimGuid)
		{
			SessionClaimGuid = sessionClaimGuid;
		}

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.ClaimSessionRequest"/> payload.
		/// </summary>
		public ClaimSessionRequestPayload()
		{
			//We don't need anything in this payload yet
			//Future may have a signed message.
		}
	}
}
