using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// Indicates the response result.
	/// </summary>
	public enum PlayerSpawnResponseCode : byte
	{
		/// <summary>
		/// Represents an unknown <see cref="BoomaPayloadMessageType.ClaimSessionResponse"/> state.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Represents that the <see cref="BoomaPayloadMessageType.ClaimSessionRequest"/> was successful.
		/// </summary>
		Success = 1,

		//TODO: Add future potential response results. I can't think of any now
	}
}
