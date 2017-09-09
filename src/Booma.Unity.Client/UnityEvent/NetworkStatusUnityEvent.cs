using GladNet.Common;
using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Booma
{
	/// <summary>
	/// Serialization hack for UnityEvent.
	/// Has args <see cref="INetPeer"/> and <see cref="NetStatus"/>
	/// </summary>
	[Serializable]
	public class NetworkStatusUnityEvent : UnityEvent<INetPeer, NetStatus>
	{

	}
}