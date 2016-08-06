using GladNet.Common;
using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Booma.Client.Network.Common
{
	/// <summary>
	/// Serialization hack for UnityEvent.
	/// Has args <see cref="INetPeer"/>
	/// </summary>
	[Serializable]
	public class NetworkUnityEvent : UnityEvent<INetPeer>
	{

	}
}
