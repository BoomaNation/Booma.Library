using Booma.Instance.Data;
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
	/// An event that contains information about a position update for an entity.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.EntityPositionUpdateEvent)]
	public class EntityPositionUpdateEvent : PacketPayload, IEntityIdentifiable
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)] //use higher index so we don't overlap with the include
		public int EntityId { get; private set; }

		/// <summary>
		/// Represents the current known position of the entity.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index2)]
		public Vector3Surrogate Position { get; private set; }

		//WARNING: Send the server timestamp. Not the unique player timestamp. The less info malicious actors have to spoof other player's
		//packets the better
		/// <summary>
		/// The timestamp of this update.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index4)]
		public float CurrentTimeStamp { get; private set; }

		public EntityPositionUpdateEvent(Vector3Surrogate position, int entityId, float timeStamp)
		{
			//TODO: Check refs
			//TODO: Verify args
			EntityId = entityId;
			Position = position;
			CurrentTimeStamp = timeStamp;
		}

		protected EntityPositionUpdateEvent()
		{

		}
	}
}
