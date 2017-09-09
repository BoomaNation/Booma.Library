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
	[BoomaPayload(BoomaPayloadMessageType.PlayerMoveRequest)]
	public class PlayerMoveRequestPayload : PacketPayload
	{
		/// <summary>
		/// Represents the new position the player wants to move to.
		/// </summary>
		[GladNetMember(1)]
		public Vector3Surrogate Position { get; private set; }

		/// <summary>
		/// Represents the time delta since the client start.
		/// Consider this to be a timestamp that is unique to the client that is connected.
		/// </summary>
		[GladNetMember(2)]
		public float TimeSinceStart { get; private set; }

		/// <summary>
		/// Creates a new <see cref="PlayerMoveRequestPayload"/> payload.
		/// </summary>
		public PlayerMoveRequestPayload(Vector3Surrogate position, float timeStamp)
		{
			if (position == null) throw new ArgumentNullException(nameof(position));
			if (timeStamp < 0) throw new ArgumentOutOfRangeException(nameof(timeStamp));

			Position = position;
			TimeSinceStart = timeStamp;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected PlayerMoveRequestPayload()
		{

		}
	}
}
