using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// Response sent by server as a response to <see cref="BoomaPayloadMessageType.PlayerSpawnRequest"/>.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.PlayerSpawnResponse)]
	public class PlayerSpawnResponsePayload : PacketPayload, IResponseStatus<PlayerSpawnResponseCode>
	{
		/// <summary>
		/// Response status; the response code of the request.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public PlayerSpawnResponseCode ResponseCode { get; }

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.PlayerSpawnResponse"/> payload.
		/// </summary>
		public PlayerSpawnResponsePayload(PlayerSpawnResponseCode code)
		{
			ResponseCode = code;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected PlayerSpawnResponsePayload()
		{

		}

		
	}
}
