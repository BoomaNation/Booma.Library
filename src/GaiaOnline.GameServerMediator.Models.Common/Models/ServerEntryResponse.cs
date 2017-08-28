using System;
using HaloLive.Models;
using HaloLive.Models.NameResolution;
using Newtonsoft.Json;

namespace GaiaOnline
{
	[JsonObject]
	public sealed class ServerEntryResponse : IResponseModel<ServerEntryResponseCode>, ISucceedable
	{
		/// <summary>
		/// The GUID that can be used to a claim a session.
		/// This is not a cryptographically secure guid. It doesn't need to be.
		/// We just need something slightly complex to claim the short lived open session on a server.
		/// </summary>
		[JsonProperty]
		public Guid SessionClaimGuid { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public ServerEntryResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ServerEntryResponseCode.Success;

		/// <summary>
		/// The endpoint that client should connect to.
		/// </summary>
		[JsonProperty]
		public ResolvedEndpoint Endpoint { get; }

		public ServerEntryResponse(Guid sessionClaimGuid, ResolvedEndpoint endpoint)
		{
			if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

			SessionClaimGuid = sessionClaimGuid;
			Endpoint = endpoint;
			ResultCode = ServerEntryResponseCode.Success;
		}

		public ServerEntryResponse(ServerEntryResponseCode resultCode)
		{
			if (!Enum.IsDefined(typeof(ServerEntryResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the ServerEntryResponseCode enum.");

			ResultCode = resultCode;
		}
	}
}