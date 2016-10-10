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
	/// An event that contains information about an entity that should be spawned.
	/// </summary>
	[GladNetSerializationContract]
	[BoomaPayload(BoomaPayloadMessageType.EntitySpawnEvent)]
	[GladNetSerializationInclude(GladNetIncludeIndex.Index1, typeof(PlayerSpawnEventPayload), true)]
	public abstract class EntitySpawnEventPayload : PacketPayload, IEntityIdentifiable
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index2)] //use higher index so we don't overlap with the include
		public int EntityId { get; private set; }

		/// <summary>
		/// Represents the position of the Entity.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index3)]
		public Vector3Surrogate Position { get; private set; }

		/// <summary>
		/// Represents the rotation of the Entity
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index4)]
		public QuaternionSurrogate Rotation { get; private set; }

		/// <summary>
		/// Creates a new payload for the <see cref="BoomaPayloadMessageType.EntitySpawnEvent"/> packet.
		/// </summary>
		/// <param name="entityId">The entity's ID.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		public EntitySpawnEventPayload(int entityId, Vector3Surrogate position, QuaternionSurrogate rotation)
		{
			if (position == null)
				throw new ArgumentNullException(nameof(position), $"Provided {nameof(Vector3Surrogate)} must not be null.");

			if (rotation == null)
				throw new ArgumentNullException(nameof(rotation), $"Provided {nameof(QuaternionSurrogate)} must not be null.");

			EntityId = entityId;
			Position = position;
			Rotation = rotation;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected EntitySpawnEventPayload()
		{

		}
	}
}
