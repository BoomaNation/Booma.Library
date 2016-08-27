using Booma.Client.Network.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladNet.Serializer;
using GladNet.Message;
using Booma.Payloads.Instance;

namespace Booma.Instance.Client
{
	public class InstanceClientPeer : BoomaNetworkClientPeer<InstanceClientPeer>
	{
		public override void OnStatusChanged(NetStatus status)
		{
			//TODO: Implement status handling
		}

		public override void RegisterPayloadTypes(ISerializerRegistry registry)
		{
			registry.Register(typeof(NetworkMessage));
			registry.Register(typeof(EntitySpawnEventPayload));
		}
	}
}
