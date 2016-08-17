using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.ServerSelection
{
	//TODO: Debate on authorized request
	[GladNetSerializationContract]
	[GladLivePayload(BoomaPayloadMessageType.GetGameServerListRequest)]
	public class GameServerListRequestPayload : PacketPayload
	{
		//We don't need anything in this payload.
		/// <summary>
		/// Creates a new payload for the <see cref="BoomaPayloadMessageType.GetGameServerListRequest"/> packet.
		/// </summary>
		public GameServerListRequestPayload()
		{

		}
	}
}
