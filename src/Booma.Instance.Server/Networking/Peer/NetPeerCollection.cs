using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	//TODO: Add O(1) lookup functionality.
	/// <summary>
	/// Simple collection of <see cref="INetPeer"/>
	/// </summary>
	public class NetPeerCollection : List<INetPeer>, IPeerCollection
	{

	}
}
