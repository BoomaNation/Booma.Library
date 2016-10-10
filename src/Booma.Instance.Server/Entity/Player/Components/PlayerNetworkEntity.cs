using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Network tag/component for Entities on the server that are partially controlled by remote players.
	/// </summary>
	public class PlayerNetworkEntity : MonoBehaviour
	{
		/// <summary>
		/// <see cref="INetPeer"/> associated with the player Entity.
		/// </summary>
		public INetPeer Peer { get; private set; }

		/// <summary>
		/// Indicates if the component has been initialized.
		/// </summary>
		private bool isInitialized = false;

		public void Initialize(INetPeer peer)
		{
			if (isInitialized)
				throw new InvalidOperationException($"Cannot initialize the {nameof(PlayerNetworkEntity)} multiple times.");

			if (peer == null)
				throw new ArgumentNullException(nameof(peer), $"The provided {nameof(INetPeer)} should not be null.");

			Peer = peer;

			isInitialized = true;
		}
	}
}
