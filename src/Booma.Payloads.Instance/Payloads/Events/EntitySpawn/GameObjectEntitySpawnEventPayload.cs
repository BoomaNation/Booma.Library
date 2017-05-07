using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using Booma.Entity.Identity;
using Booma.Payloads.Surrogates.Unity;
using Booma.Entity.Prefab;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// An event that contains information about an entity that should be spawned.
	/// </summary>
	[GladNetSerializationContract]
	public class GameObjectEntitySpawnEventPayload : EntitySpawnEventPayload
	{
		[GladNetMember(GladNetDataIndex.Index1)]
		public Vector3Surrogate Scale { get; private set; }

		[GladNetMember(GladNetDataIndex.Index2)]
		public GameObjectPrefab PrefabId { get; private set; }

		[GladNetMember(GladNetDataIndex.Index3)]
		public byte CurrentState { get; private set; }

		public GameObjectEntitySpawnEventPayload(NetworkEntityGuid entityGuid, Vector3Surrogate position, QuaternionSurrogate rotation,
			Vector3Surrogate scale, GameObjectPrefab prefabId, byte state) 
			: base(entityGuid, position, rotation)
		{
			//TODO: Check values/refs
			Scale = scale;
			PrefabId = prefabId;
			CurrentState = state;
		}

		protected GameObjectEntitySpawnEventPayload()
		{

		}
	}
}
