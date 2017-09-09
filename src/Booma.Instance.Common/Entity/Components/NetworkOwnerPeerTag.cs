using GladNet.Engine.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	/// <summary>
	/// Network tag/component that indicates network ownership.
	/// </summary>
	[Injectee]
	public class NetworkOwnerPeerTag : MonoBehaviour, INetworkOwnable
	{
		/// <summary>
		/// <see cref="INetPeer"/> associated with the object.
		/// </summary>
		[Inject]
		public INetPeer OwnerPeer { get; private set; }
	}
}
