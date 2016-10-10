using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	public interface IPlayerPeerProvider
	{
		/// <summary>
		/// Returns an <see cref="IEnumerable{T}"/> of <see cref="INetPeer"/> that is associated with the current players.
		/// </summary>
		/// <returns>Collection of <see cref="INetPeer"/>.</returns>
		IEnumerable<INetPeer> AllPeers();
	}
}
