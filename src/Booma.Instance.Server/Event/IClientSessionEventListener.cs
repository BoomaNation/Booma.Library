using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IClientSessionEventListener
	{
		/// <summary>
		/// Target for an event.
		/// </summary>
		/// <param name="session">The session.</param>
		void OnEvent(ClientPeerSession session);
	}
}
