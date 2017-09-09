using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// Response sent by server as a response to <see cref="BoomaPayloadMessageType.ClaimSessionRequest"/>.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.ClaimSessionResponse)]
	public class ClaimSessionResponsePayload : PacketPayload, IResponseStatus<PlayerSpawnResponseCode>
	{
		/// <summary>
		/// Represents the unique entity indentifier.
		/// (This will indicate what our GUID is on the server)
		/// </summary>
		[GladNetMember(4)]
		public NetworkEntityGuid EntityGuid { get; private set; }

		/// <summary>
		/// Response status; the response code of the request.
		/// </summary>
		[GladNetMember(1)]
		public PlayerSpawnResponseCode ResponseCode { get; private set; } //no readonly props until new protobuf-net

		/// <summary>
		/// Represents the position of the Entity.
		/// </summary>
		[GladNetMember(2)]
		public Vector3Surrogate Position { get; private set; }

		/// <summary>
		/// Represents the rotation of the Entity
		/// </summary>
		[GladNetMember(3)]
		public QuaternionSurrogate Rotation { get; private set; }

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.ClaimSessionResponse"/> payload.
		/// </summary>
		public ClaimSessionResponsePayload(Vector3Surrogate position, QuaternionSurrogate rotation, NetworkEntityGuid entityGuid)
		{
			if (position == null) throw new ArgumentNullException(nameof(position));
			if (rotation == null) throw new ArgumentNullException(nameof(rotation));
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));

			ResponseCode = PlayerSpawnResponseCode.Success;
			EntityGuid = entityGuid;
			Position = position;
			Rotation = rotation;
		}

		/// <summary>
		/// Creates a new <see cref="BoomaPayloadMessageType.ClaimSessionResponse"/> payload.
		/// </summary>
		public ClaimSessionResponsePayload(PlayerSpawnResponseCode code)
		{
			if(!Enum.IsDefined(typeof(PlayerSpawnResponseCode), code)) throw new InvalidEnumArgumentException(nameof(code), (int)code, typeof(PlayerSpawnResponseCode));
			if(code == PlayerSpawnResponseCode.Success) throw new ArgumentException("Cannot provide sucess code for failed response constructor.", nameof(code));

			ResponseCode = code;
		}


		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected ClaimSessionResponsePayload()
		{

		}
	}
}
