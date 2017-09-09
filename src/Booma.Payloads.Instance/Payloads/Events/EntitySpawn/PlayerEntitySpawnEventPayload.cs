using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// An event that contains information about an entity that should be spawned.
	/// </summary>
	[GladNetSerializationContract]
	public class PlayerEntitySpawnEventPayload : EntitySpawnEventPayload
	{
		public PlayerEntitySpawnEventPayload(NetworkEntityGuid entityGuid, Vector3Surrogate position, QuaternionSurrogate rotation) 
			: base(entityGuid, position, rotation)
		{
			//Nothing at the moment. Can extend for future use.
		}

		protected PlayerEntitySpawnEventPayload()
		{

		}
	}
}
