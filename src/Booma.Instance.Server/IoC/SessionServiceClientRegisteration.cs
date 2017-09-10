using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using TypeSafe.Http.Net;

namespace Booma
{
	public sealed class SessionServiceClientRegisteration : NonBehaviourDependency
	{
		//TODO: Handle HTTPS cert issue
		public override void Register(IServiceRegister register)
		{
			register.RegisterInstance(TypeSafeHttpBuilder<IGameServerSessionService>.Create()
				.RegisterDefaultSerializers()
				.RegisterJsonNetSerializer()
				.RegisterDotNetHttpClient(@"http://localhost:5003")
				.Build()); //TODO: How should an instance server find its mediator?
		}
	}
}
