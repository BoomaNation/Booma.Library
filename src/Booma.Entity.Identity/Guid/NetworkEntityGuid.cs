using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Identity
{
	//Format: 0xSSEE XXXX IIII IIII
	//[IIII IIII] unsigned 32bit unique identifier.
	//[X] unsued
	//[SS] Server ID. Will probably remain unimplemented for awhile until cross-server is needed.
	//[EE] Entity type. Reserves 255 indicies for possible entity types.
	/// <summary>
	/// Unique GUID identifier for network entites. Based on Blizzard's 64bit GUID implementation
	/// which encodes flags and uses masking to dervive information about a GUID.
	/// See: http://wowwiki.wikia.com/wiki/API_UnitGUID
	/// </summary>
	[GladNetSerializationContract]
	public class NetworkEntityGuid : IEntityIdentifiable
	{
		//Sent over the network.
		/// <summary>
		/// Raw 64bit numerical representation of the GUID.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1, IsRequired = true)]
		private ulong rawGuidValue { get; }

		/// <summary>
		/// Represents an Empty or uninitialized <see cref="NetworkEntityGuid"/>.
		/// </summary>
		public static NetworkEntityGuid Empty { get; } = new NetworkEntityGuid(0);

		/// <summary>
		/// Indicates the <see cref="EntityType"/> that this <see cref="NetworkEntityGuid"/> is for.
		/// </summary>
		public EntityType EntityType { get { return (EntityType)(byte)((rawGuidValue & 0x00FF000000000000) >> 48); } } //mask out to the EE (entity Type) and then shift it down to a byte

		/// <summary>
		/// Indiciates if the GUID is an empty or unitialized GUID.
		/// </summary>
		/// <returns></returns>
		public bool isEmpty { get { return rawGuidValue == 0; } }

		/// <summary>
		/// Indiciates the current GUID of the entity. This is the last chunk represents the actual ID without any type or identifying information.
		/// </summary>
		public int EntityId { get { return (int)(rawGuidValue & 0x00000000FFFFFFFF); } } //FFFF FFFF masks out everything but an unsigned integer. Casts to int. We waste bits this way but we gain considerable perf.

		public NetworkEntityGuid(ulong guidValue)
		{
			rawGuidValue = guidValue;
		}

		/// <summary>
		/// Creates a <see cref="NetworkEntityGuid"/> building service.
		/// </summary>
		/// <returns>A non-null <see cref="NetworkEntityGuidBuilder"/> service.</returns>
		public NetworkEntityGuidBuilder CreateBuilder()
		{
			return new NetworkEntityGuidBuilder();
		}
	}
}
