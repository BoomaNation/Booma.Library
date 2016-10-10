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
		/// Represents the position of the Entity.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index2)]
		public Vector3Surrogate Position { get; private set; }

		/// <summary>
		/// Represents the rotation of the Entity
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index3)]
		public QuaternionSurrogate Rotation { get; private set; }

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.PlayerSpawnResponse"/> payload.
		/// </summary>
		public PlayerSpawnResponsePayload(PlayerSpawnResponseCode code, Vector3Surrogate position, QuaternionSurrogate rotation)
		{
			//TODO: Check refs

			ResponseCode = code;

			Position = position;
			Rotation = rotation;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected PlayerSpawnResponsePayload()
		{

		}

		
	}
}
