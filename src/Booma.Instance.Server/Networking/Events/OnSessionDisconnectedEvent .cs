using GladNet.Engine.Common;
using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Booma
{
	/// <summary>
	/// Event for new session disconnection
	/// </summary>
	[Serializable]
	public class OnSessionDisconnectedEvent : UnityEvent<ClientPeerSession> { }
}
