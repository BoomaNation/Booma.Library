using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SceneJect.Common;
using TypeSafe.Http.Net;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class GaiaOnlineNetworkClientsDependencyRegister : NonBehaviourDependency
	{
		[SerializeField]
		[Tooltip(@"The main query base url. (Ex. http://www.gaiaonline.com")]
		private string GaiaOnlineQueryBaseUrl;

		[SerializeField]
		[Tooltip(@"The CDN base url. (Ex. http://a2.cdn.gaiaonline.com")]
		private string GaiaOnlineImageCDNBaseUrl;

		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			if (string.IsNullOrWhiteSpace(GaiaOnlineQueryBaseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(GaiaOnlineQueryBaseUrl));
			if (string.IsNullOrWhiteSpace(GaiaOnlineImageCDNBaseUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(GaiaOnlineImageCDNBaseUrl));

			register.RegisterInstance<IGaiaOnlineQueryClient, IGaiaOnlineQueryClient>(TypeSafeHttpBuilder<IGaiaOnlineQueryClient>.Create()
				.RegisterDefaultSerializers()
				.RegisterDotNetXmlSerializer()
				.RegisterDotNetHttpClient(GaiaOnlineQueryBaseUrl)
				.Build());

			register.RegisterInstance<IGaiaOnlineImageCDNClient, IGaiaOnlineImageCDNClient>(TypeSafeHttpBuilder<IGaiaOnlineImageCDNClient>.Create()
				.RegisterDefaultSerializers()
				.RegisterUnityTexture2DSerializer()
				.RegisterDotNetHttpClient(GaiaOnlineImageCDNBaseUrl)
				.Build());
		}
	}
}
