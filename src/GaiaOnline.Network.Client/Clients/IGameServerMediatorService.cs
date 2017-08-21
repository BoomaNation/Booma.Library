using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;

namespace GaiaOnline
{
	/// <summary>
	/// Client that provides acess to the mediator service for a gameserver
	/// allowing you to enter the gameserver or move between zones in one.
	/// </summary>
	public interface IGameServerMediatorService
	{
		/// <summary>
		/// Attempts to enter the gameserver. Requesting access through the mediator.
		/// It could fail for several reasons.
		/// </summary>
		/// <param name="token">The access token.</param>
		/// <returns>The result of the attempted entry.</returns>
		[Post("/api/gameserver/gateway")]
		Task<ServerEntryResponse> TryEnterGameServer([DynamicHeader("Authorization")] string token);
	}
}
