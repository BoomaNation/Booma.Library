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
		    IPlayerEntityCollection playerCollection = new ServerPlayerEntityCollection();

			register.Register<IPlayerEntityCollection>(playerCollection, RegistrationType.SingleInstance);
		}
	}
}
