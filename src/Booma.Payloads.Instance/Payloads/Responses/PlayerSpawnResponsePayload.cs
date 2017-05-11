using Booma.Entity.Identity;
using Booma.Payloads.Common;
using Booma.Payloads.Surrogates.Unity;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		/// Represents the unique entity indentifier.
		/// (This will indicate what our GUID is on the server)
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index4)]
		public NetworkEntityGuid EntityGuid { get; private set; }

		/// <summary>
		/// Response status; the response code of the request.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public PlayerSpawnResponseCode ResponseCode { get; private set; } //no readonly props until new protobuf-net

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
		public PlayerSpawnResponsePayload(PlayerSpawnResponseCode code, Vector3Surrogate position, QuaternionSurrogate rotation, NetworkEntityGuid entityGuid)
		{
			if (position == null) throw new ArgumentNullException(nameof(position));
			if (rotation == null) throw new ArgumentNullException(nameof(rotation));
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));
			if (!Enum.IsDefined(typeof(PlayerSpawnResponseCode), code)) throw new InvalidEnumArgumentException(nameof(code), (int) code, typeof(PlayerSpawnResponseCode));

			ResponseCode = code;
			EntityGuid = entityGuid;
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
