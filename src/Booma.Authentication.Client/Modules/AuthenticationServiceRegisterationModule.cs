using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Network;
using HaloLive.Network.Common;
using SceneJect.Common;
using TypeSafe.Http.Net;
using UnityEngine;

namespace Booma.Client
{
	/// <summary>
	/// Registers the <see cref="IAuthenticationService"/> service with Sceneject.
	/// </summary>
	public sealed class AuthenticationServiceRegisterationModule : HttpServiceWithDiscoveryRegisterationModule
	{
		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

			IAuthenticationService authService = TypeSafeHttpBuilder<IAuthenticationService>.Create()
				.RegisterDefaultSerializers()
				.RegisterJsonNetSerializer()
				.RegisterDotNetHttpClient(GetDeclaredServiceUrl("AuthenticationService")) //TODO: Central the naming of these services
				.Build();

			//TODO: Adjust registeration
			register.RegisterInstance<IAuthenticationService, IAuthenticationService>(authService);
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		private bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
		}
	}
}
