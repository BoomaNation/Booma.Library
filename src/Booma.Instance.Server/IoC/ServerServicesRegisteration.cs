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
			IPlayerEntityCollection playerCollection = new IPlayerEntityCollection();

			//Test
			playerCollection.Add(55, null);

			register.Register<IPlayerEntityCollection>(playerCollection, RegistrationType.SingleInstance); //for testing
		}
	}
}
