using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// Payload sent by players who want to spawn in an instance.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.PlayerSpawnRequest)]
	public class PlayerSpawnRequestPayload : PacketPayload
	{
		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.PlayerSpawnRequest"/> payload.
		/// </summary>
		protected PlayerSpawnRequestPayload()
		{
			//We don't need anything in this payload yet
			//Future may have a signed message.
		}
	}
}
