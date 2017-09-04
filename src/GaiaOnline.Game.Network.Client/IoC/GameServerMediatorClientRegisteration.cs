using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;
using TypeSafe.Http.Net;
using UnityEngine;

namespace GaiaOnline
{
	/// <summary>
	/// IoC registeration component for the <see cref="IGameServerMediatorService"/>.
	/// </summary>
	public class GameServerMediatorClientRegisteration : NonBehaviourDependency
	{
		//TODO: This is kinda hacky, I don't like player prefs but other solutions suck too. Also we can't put this behind IoC since this executes before IoC
		private bool hasStoredGameServer => PlayerPrefs.HasKey(PlayerPreferences.GameServerIp.ToString()) && PlayerPrefs.HasKey(PlayerPreferences.GameServerPort.ToString());

		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			if(hasStoredGameServer)
			{
				//We need to know where the server is first. We can't ask the discovery service.
				register.RegisterInstance<IGameServerMediatorService, IGameServerMediatorService>(TypeSafeHttpBuilder<IGameServerMediatorService>.Create()
					.RegisterDefaultSerializers()
					.RegisterDotNetHttpClient(GenerateGameServerMediatorUrl())
					.Build());
			}
			else
				throw new InvalidOperationException($"Failed to load gameserver from {nameof(PlayerPrefs)}");
		}

		//We don't need to cache this, since it'll only ever be called once per connection
		private string GenerateGameServerMediatorUrl()
		{
			//TODO: This is for testing only
			return @"http://localhost:5003";

			//TODO: This is kinda hacky, I don't like player prefs but other solutions suck too. Also we can't put this behind IoC since this executes before IoC
			//return $"http://{PlayerPrefs.HasKey(PlayerPreferences.GameServerIp.ToString())}:{PlayerPrefs.HasKey(PlayerPreferences.GameServerPort.ToString())}";
		}
	}
}
