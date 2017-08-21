using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using HaloLive.Models;

namespace GaiaOnline
{
	/// <summary>
	/// The response for a quest for gameservers.
	/// </summary>
	[JsonObject]
	public sealed class GameServerListResponse : IResponseModel<GameServerListResponseCode>, ISucceedable
	{
		/// <summary>
		/// List of gameservers.
		/// </summary>
		[JsonProperty]
		public List<GameServerInfo> Servers { get; private set; }

		/// <summary>
		/// Indicates the response code.
		/// </summary>
		[JsonProperty]
		public GameServerListResponseCode ResultCode { get; private set; }

		/// <summary>
		/// Indicates if the request was successful.
		/// </summary>
		[JsonIgnore]
		public bool isSuccessful => ResultCode == GameServerListResponseCode.Successful;

		/// <summary>
		/// Creates a new response containing the provided <see cref="Servers"/>
		/// with a status code set to Success.
		/// </summary>
		/// <param name="servers">The servers.</param>
		public GameServerListResponse(List<GameServerInfo> servers)
		{
			//TODO: Should we check if none are sent back? Set error code?
			//If you want to send back none then use empty collection.
			if (servers == null) throw new ArgumentNullException(nameof(servers));

			ResultCode = GameServerListResponseCode.Successful;
			Servers = servers;
		}

		/// <summary>
		/// Ctor used when the is an error
		/// </summary>
		/// <param name="code"></param>
		public GameServerListResponse(GameServerListResponseCode code)
		{
			if (!Enum.IsDefined(typeof(GameServerListResponseCode), code)) throw new ArgumentOutOfRangeException(nameof(code), "Value should be defined in the GameServerListResponseCode enum.");

			Servers = Enumerable.Empty<GameServerInfo>().ToList();
			ResultCode = code;
		}
	}
}