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
	[GladLivePayload(BoomaPayloadMessageType.GetGameServerListResponse)]
	public class GameServerListResponsePayload : PacketPayload, IResponseStatus<GameServerListResponseCode>
	{
		/// <summary>
		/// Indicates the status of the response.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public GameServerListResponseCode ResponseCode { get; private set; }

		//private and hidden. For serializer.
		[GladNetMember(GladNetDataIndex.Index2)]
		private SimpleGameServerDetailsModel[] gameServerDetails;

		/// <summary>
		/// Collection of available gameservers.
		/// </summary>
		public IEnumerable<SimpleGameServerDetailsModel> GameServerDetails { get { return gameServerDetails; } }

		/// <summary>
		/// Creates a new gameserver list response with only a response code.
		/// </summary>
		/// <param name="code">response code.</param>
		public GameServerListResponsePayload(GameServerListResponseCode code)
		{
			ResponseCode = code;
		}

		/// <summary>
		/// Creates a new gameserver list response with the server list.
		/// </summary>
		/// <param name="code">Response code.</param>
		/// <param name="details">Details collection.</param>
		public GameServerListResponsePayload(GameServerListResponseCode code, IEnumerable<SimpleGameServerDetailsModel> details)
		{
			ResponseCode = code;

			//set to internal field
			gameServerDetails = details.ToArray();
		}

		/// <summary>
		/// Creates a new payload for the <see cref="BoomaPayloadMessageType.GetGameServerListResponse"/> packet.
		/// </summary>
		public GameServerListResponsePayload()
		{

		}
	}
}
