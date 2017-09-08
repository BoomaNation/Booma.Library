using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaiaOnline
{
	//TODO: Refactor into writable and readonly interface
	public interface IGameSessionRepository : IReadOnlyGameSessionRepository
	{
		//TODO: The user should be authenticated, and with OAuth can prove it, so we just require id. Switch to OAuth ASAP
		/// <summary>
		/// Tries to create a gamesession for the user.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="ipAddress"></param>
		/// <returns></returns>
		Task<SessionCreationResult> TryCreateSession(int userId, string ipAddress);
	}
}
