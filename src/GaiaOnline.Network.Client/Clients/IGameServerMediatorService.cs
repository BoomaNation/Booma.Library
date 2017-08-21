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
		[Post("/api/gameserver/gateway")]
		Task<ServerEntryResponse> TryEnterGameServer([DynamicHeader("Authorization")] string token);
	}
}
