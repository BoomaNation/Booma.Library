using GladNet.Engine.Common;
using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Event for new session creation.
	/// </summary>
	public class OnNewSessionCreatedEvent : UnityEvent<ClientPeerSession> { }
}
