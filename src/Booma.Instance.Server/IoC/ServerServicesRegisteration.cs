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

			register.Register(entityCollection, RegistrationType.SingleInstance);
			register.Register(guidFactory, RegistrationType.SingleInstance, typeof(INetworkGuidFactory));
		}
	}
}
