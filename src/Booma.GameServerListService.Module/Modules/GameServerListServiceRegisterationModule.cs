using GladLive.Module.System.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Booma.GameServerList.Lib;
using GladNet.Serializer;
using Booma.Payloads.ServerSelection;

namespace Booma.GameServerList.Module
{
	/// <summary>
	/// Registers the services in the GameServerList ASP service.
	/// </summary>
	public class GameServerListServiceRegisterationModule : ServiceRegistrationModule
	{
		public GameServerListServiceRegisterationModule(IServiceCollection services, Action<DbContextOptionsBuilder> options = null) 
			: base(services, options)
		{

		}

		public override void Register()
		{
			//Grab a serializer registry to registr payloads.
			ISerializerRegistry registry = serviceCollection.BuildServiceProvider().GetService<ISerializerRegistry>();

			if(registry == null)
				throw new InvalidOperationException($"Tried to build {nameof(ISerializerRegistry)} from {nameof(serviceCollection)} but returned null.");

			//Register the data context
			serviceCollection.AddDbContext<GameServerListDbContext>(DbOptions);

			serviceCollection.AddScoped<IGameServerDetailsRepositoryAsync, GameServerDetailsRepository>();

			//TODO: Fix serializer fault with collection
			registry.Register(typeof(SimpleGameServerDetailsModel));

			//Registers the gameserver selection payloads.
			registry.RegisterServerSelectionPayloads();
		}
	}
}
