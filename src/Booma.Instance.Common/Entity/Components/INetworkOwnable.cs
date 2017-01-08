using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface INetworkOwnable
	{
		INetPeer OwnerPeer { get; }
	}
}
