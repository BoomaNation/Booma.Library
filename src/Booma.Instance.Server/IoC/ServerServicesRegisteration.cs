using Booma.Instance.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	public class ServerServicesRegisteration : NonBehaviourDependency
	{
		public override void Register(IServiceRegister register)
		{
			//Just create a new collection and register it
			NetworkEntityCollection entityCollection = new NetworkEntityCollection();
			IncrementalNetworkGuidFactory guidFactory = new IncrementalNetworkGuidFactory();
			NetPeerCollection peerCollection = new NetPeerCollection();
			ConnectionToPlayerGuidLookupService lookupService = new ConnectionToPlayerGuidLookupService();

			register.Register(entityCollection, RegistrationType.SingleInstance, null);
			register.Register(guidFactory, RegistrationType.SingleInstance, typeof(INetworkGuidFactory));
			register.Register(peerCollection, RegistrationType.SingleInstance, typeof(IReadonlyPeerCollection));
			register.Register(peerCollection, RegistrationType.SingleInstance, typeof(IPeerCollection));
			register.Register(lookupService, RegistrationType.SingleInstance, typeof(IConnectionToGuidLookupService));
			register.Register(lookupService, RegistrationType.SingleInstance, typeof(IConnectionToGuidRegistry));
		}
	}
}
