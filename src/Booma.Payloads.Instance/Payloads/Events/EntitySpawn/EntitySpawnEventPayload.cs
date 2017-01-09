using Booma.Entity.Identity;
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
	[GladNetSerializationInclude(GladNetIncludeIndex.Index1, typeof(PlayerEntitySpawnEventPayload))]
	[GladNetSerializationInclude(GladNetIncludeIndex.Index2, typeof(GameObjectEntitySpawnEventPayload))]
	[BoomaPayload(BoomaPayloadMessageType.EntitySpawnEvent)]
	public class EntitySpawnEventPayload : PacketPayload
	{
		/// <summary>
		/// Represents the unique entity indentifier.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index3)] //use higher index so we don't overlap with the include
		public NetworkEntityGuid EntityGuid { get; private set; }

		/// <summary>
		/// Represents the position of the Entity.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index4)]
		public Vector3Surrogate Position { get; private set; }

		/// <summary>
		/// Represents the rotation of the Entity
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index5)]
		public QuaternionSurrogate Rotation { get; private set; }

		/// <summary>
		/// Creates a new payload for the <see cref="BoomaPayloadMessageType.EntitySpawnEvent"/> packet.
		/// </summary>
		/// <param name="entityGuid">The entity's GUID.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		public EntitySpawnEventPayload(NetworkEntityGuid entityGuid, Vector3Surrogate position, QuaternionSurrogate rotation)
		{
			if (position == null)
				throw new ArgumentNullException(nameof(position), $"Provided {nameof(Vector3Surrogate)} must not be null.");

			if (rotation == null)
				throw new ArgumentNullException(nameof(rotation), $"Provided {nameof(QuaternionSurrogate)} must not be null.");

			EntityGuid = entityGuid;
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
