using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Network tag/component for Entities on the server that are partially owned by a peer.
	/// </summary>
	public class NetworkPeerEntityTag : MonoBehaviour
	{
		/// <summary>
		/// <see cref="INetPeer"/> associated with Entity.
		/// </summary>
		public INetPeer Peer { get; private set; }

		/// <summary>
		/// Indicates if the component has been initialized.
		/// </summary>
		private bool isInitialized = false;

		public void Initialize(INetPeer peer)
		{
			if (isInitialized)
				throw new InvalidOperationException($"Cannot initialize the {nameof(NetworkPeerEntityTag)} multiple times.");

			if (peer == null)
				throw new ArgumentNullException(nameof(peer), $"The provided {nameof(INetPeer)} should not be null.");

			Peer = peer;

			isInitialized = true;
		}
	}
}
