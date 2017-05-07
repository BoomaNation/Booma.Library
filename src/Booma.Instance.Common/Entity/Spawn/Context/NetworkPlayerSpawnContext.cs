using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using GladNet.Engine.Common;
using SceneJect.Common;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Context for a spawning entity.
	/// </summary>
	public class NetworkPlayerSpawnContext : IPlayerSpawnContext, ISpawnContext
	{
		/// <summary>
		/// Network GUID context.
		/// </summary>
		public NetworkEntityGuid NetworkGuid { get; }

		/// <summary>
		/// Network peer context.
		/// </summary>
		public INetPeer Peer { get; }

		public NetworkPlayerSpawnContext(NetworkEntityGuid guid, INetPeer peer)
		{
			//TODO: Null tests

			NetworkGuid = guid;
			Peer = peer;
		}

		public IGameObjectContextualBuilder ProvideContext(IGameObjectContextualBuilder builder)
		{
			return builder.With(Service<NetworkEntityGuid>.As(NetworkGuid))
				.With(Service<INetPeer>.As(Peer));
		}
	}
}
