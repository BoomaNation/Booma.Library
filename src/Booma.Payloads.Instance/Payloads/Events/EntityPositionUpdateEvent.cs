﻿using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// An event that contains information about a position update for an entity.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.EntityPositionUpdateEvent)]
	public class EntityPositionUpdateEvent : PacketPayload
	{
		/// <summary>
		/// Represents the unique entity indentifier.
		/// </summary>
		[GladNetMember(1)]
		public NetworkEntityGuid EntityGuid { get; private set; }

		/// <summary>
		/// Represents the current known position of the entity.
		/// </summary>
		[GladNetMember(2)]
		public Vector3Surrogate Position { get; private set; }

		//WARNING: Send the server timestamp. Not the unique player timestamp. The less info malicious actors have to spoof other player's
		//packets the better
		/// <summary>
		/// The timestamp of this update.
		/// </summary>
		[GladNetMember(3)]
		public float CurrentTimeStamp { get; private set; }

		public EntityPositionUpdateEvent(Vector3Surrogate position, NetworkEntityGuid entityGuid, float timeStamp)
		{
			if (position == null) throw new ArgumentNullException(nameof(position));
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));
			if (timeStamp < 0) throw new ArgumentOutOfRangeException(nameof(timeStamp));

			//TODO: Check refs
			//TODO: Verify args
			EntityGuid = entityGuid;
			Position = position;
			CurrentTimeStamp = timeStamp;
		}

		protected EntityPositionUpdateEvent()
		{

		}
	}
}
