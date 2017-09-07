using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;
using GladNet.Engine.Common;
using JetBrains.Annotations;
using SceneJect.Common;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Context for a spawning entity.
	/// </summary>
	public class NetworkPlayerSpawnContext : IPlayerSpawnContext
	{
		/// <summary>
		/// Network GUID context.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <summary>
		/// Network peer context.
		/// </summary>
		public INetPeer OwnerPeer { get; }

		public NetworkPlayerSpawnContext([NotNull] NetworkEntityGuid guid, [NotNull] INetPeer peer)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));
			if(peer == null) throw new ArgumentNullException(nameof(peer));

			EntityGuid = guid;
			OwnerPeer = peer;
		}

		public IGameObjectContextualBuilder ProvideContext([NotNull] IGameObjectContextualBuilder builder)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			return builder.With(Service<NetworkEntityGuid>.As(EntityGuid))
				.With(Service<INetPeer>.As(OwnerPeer));
		}
	}
}
