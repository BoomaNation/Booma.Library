using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using Booma.Entity.Identity;
using Booma.Payloads.Surrogates.Unity;

namespace Booma.Payloads.Instance
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
