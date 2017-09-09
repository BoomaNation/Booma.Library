using System;
using HaloLive.Models.NameResolution;
using HaloLive.Network.Common;
using Newtonsoft.Json;

namespace Booma
{
	/// <summary>
	/// Data model that represents information about a gameserver.
	/// </summary>
	[JsonObject]
	public sealed class GameServerInfo
	{
		/// <summary>
		/// The name of the server.
		/// </summary>
		[JsonProperty]
		public string ServerName { get; private set; }

		/// <summary>
		/// The locale of the server.
		/// </summary>
		[JsonProperty]
		public ClientRegionLocale ServerLocale { get; private set; }

		/// <summary>
		/// The enpoint pair of the server.
		/// </summary>
		[JsonProperty]
		public ResolvedEndpoint Endpoint { get; private set; }

		public GameServerInfo(string serverName, ClientRegionLocale serverLocale, ResolvedEndpoint endpoint)
		{
			if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
			if (string.IsNullOrWhiteSpace(serverName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serverName));
			if (!Enum.IsDefined(typeof(ClientRegionLocale), serverLocale)) throw new ArgumentOutOfRangeException(nameof(serverLocale), "Value should be defined in the ClientRegionLocale enum.");

			ServerName = serverName;
			ServerLocale = serverLocale;
			Endpoint = endpoint;
		}
	}
}