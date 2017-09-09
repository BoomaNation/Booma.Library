using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Enumeration of all response codes for the game server list request.
	/// </summary>
	public enum GameServerListResponseCode
	{
		/// <summary>
		/// Indicates that the request was successful.
		/// </summary>
		Successful = 0,

		/// <summary>
		/// Indicates that no gameservers are available.
		/// </summary>
		NoGameServersAvailable = 1,

		//TODO: more
	}
}
