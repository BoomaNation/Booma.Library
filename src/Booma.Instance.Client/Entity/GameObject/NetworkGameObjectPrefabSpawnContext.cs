using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Prefab;
using SceneJect.Common;
using Booma.Entity.Identity;
using GladNet.Engine.Common;

namespace Booma.Instance.Client
{
	public class NetworkGameObjectPrefabSpawnContext : IGameObjectPrefabSpawnContext
	{
		public GameObjectPrefab PrefabId { get; }

		public NetworkEntityGuid NetworkGuid { get; }

		public INetPeer NetworkPeer { get; }

		public NetworkGameObjectPrefabSpawnContext(NetworkEntityGuid guid, GameObjectPrefab prefabId, INetPeer peer)
		{
			//TODO: Check refs

			PrefabId = prefabId;
			NetworkGuid = guid;
			NetworkPeer = peer;
		}

		public IGameObjectContextualBuilder ProvideContext(IGameObjectContextualBuilder builder)
		{
			return builder.With(Service<NetworkEntityGuid>.As(NetworkGuid))
				.With(Service<INetPeer>.As(NetworkPeer));
		}
	}
}
