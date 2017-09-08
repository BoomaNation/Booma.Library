using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.NameResolution;
using HaloLive.Network;
using HaloLive.Network.Common;
using SceneJect.Common;
using TypeSafe.Http.Net;
using UnityEngine;

namespace GaiaOnline
{
	public abstract class HttpServiceWithDiscoveryRegisterationModule : NonBehaviourDependency
	{
		//We try our best to stay effiicent by cacheing the service.
		private static readonly ConcurrentDictionary<string, IServiceDiscoveryService> ServiceUrlToDiscoveryServiceMap = new ConcurrentDictionary<string, IServiceDiscoveryService>();

		[SerializeField]
		[Tooltip("This should be the URL/URI that points to the base path of the service discovery service.")]
		private string ServiceDiscoveryEndpoint;

		[SerializeField]
		[Tooltip("The locale for the service request.")]
		private ClientRegionLocale Locale;

		protected async Task<string> GetDeclaredServiceUrl(string serviceType)
		{
			if (string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			//We can't do this async. It's a required dependency before things can move forward
			if (ServiceUrlToDiscoveryServiceMap.ContainsKey(ServiceDiscoveryEndpoint))
			{
				return await QueryServiceForEndpoint(serviceType)
					.ConfigureAwait(false);
			}
			else
			{
				ServiceUrlToDiscoveryServiceMap[ServiceDiscoveryEndpoint] = TypeSafeHttpBuilder<IServiceDiscoveryService>.Create()
					.RegisterJsonNetSerializer()
					.RegisterDotNetHttpClient(ServiceDiscoveryEndpoint)
					.Build();

				return await GetDeclaredServiceUrl(serviceType)
					.ConfigureAwait(false);
			}
		}

		private async Task<string> QueryServiceForEndpoint(string serviceType)
		{
			if (string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			IServiceDiscoveryService service = ServiceUrlToDiscoveryServiceMap[ServiceDiscoveryEndpoint];

			ResolveServiceEndpointResponseModel response = await service.Discover(new ResolveServiceEndpointRequestModel(Locale, serviceType))
				.ConfigureAwait(false);

			if (!response.isSuccessful)
				throw new InvalidOperationException($"Failed to query {nameof(IServiceDiscoveryService)} for the Service: {serviceType} in Locale: {Locale} for Reason: {response.ResultCode}.");

			return $"{response.Endpoint.EndpointAddress}:{response.Endpoint.EndpointPort}";
		}
	}
}
