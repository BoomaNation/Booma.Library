using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Booma.Entity.Identity;
using Booma.Payloads.Surrogates.Unity;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// An event that contains information about an entity that should be spawned.
	/// </summary>
	[GladNetSerializationContract]
	public class GameObjectEntitySpawnEventPayload : EntitySpawnEventPayload
	{
		/// <summary>
		/// The scale of the object.
		/// </summary>
		[GladNetMember(1)]
		public Vector3Surrogate Scale { get; private set; }

		//TODO: Replace prefab id
		/// <summary>
		/// The prefab ID.
		/// </summary>
		//[GladNetMember(2)]
		//public GameObjectPrefab PrefabId { get; private set; }

		/// <summary>
		/// The current state or starting state of the object.
		/// </summary>
		[GladNetMember(3)]
		public byte CurrentState { get; private set; }

		public GameObjectEntitySpawnEventPayload(NetworkEntityGuid entityGuid, Vector3Surrogate position, QuaternionSurrogate rotation,
			Vector3Surrogate scale/*, GameObjectPrefab prefabId*/, byte state) 
			: base(entityGuid, position, rotation)
		{
			if (scale == null) throw new ArgumentNullException(nameof(scale));
			//if (!Enum.IsDefined(typeof(GameObjectPrefab), prefabId)) throw new InvalidEnumArgumentException(nameof(prefabId), (int) prefabId, typeof(GameObjectPrefab));

			//TODO: Check values/refs
			Scale = scale;
			//PrefabId = prefabId;
			CurrentState = state;
		}

		protected GameObjectEntitySpawnEventPayload()
		{

		}
	}
}
