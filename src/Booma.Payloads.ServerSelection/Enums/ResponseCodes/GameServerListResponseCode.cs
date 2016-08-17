using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.ServerSelection
{
	/// <summary>
	/// Indicates the status of the <see cref="BoomaPayloadMessageType.GetGameServerListRequest"/>.
	/// </summary>
	public enum GameServerListResponseCode : int
	{
		/// <summary>
		/// Represents an unknown <see cref="BoomaPayloadMessageType.GetGameServerListRequest"/> state.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Indicates the the remote service is not available to services list requests.
		/// </summary>
		ServiceUnavailable = 1,

		/// <summary>
		/// Indicates that the <see cref="BoomaPayloadMessageType.GetGameServerListRequest"/> was successful.
		/// </summary>
		Success = 3,
	}
}
