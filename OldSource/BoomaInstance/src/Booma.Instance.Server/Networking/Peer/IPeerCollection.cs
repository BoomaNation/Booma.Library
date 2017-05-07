using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	public interface IPeerCollection : IList<INetPeer>, IReadonlyPeerCollection
	{

	}

	public interface IReadonlyPeerCollection : IEnumerable<INetPeer>
	{

	}
}
