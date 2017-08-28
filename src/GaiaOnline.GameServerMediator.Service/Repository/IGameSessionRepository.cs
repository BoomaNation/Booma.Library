using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaiaOnline
{
	public interface IGameSessionRepository
	{
		/// <summary>
		/// Indicates if the <see cref="userId"/> is associated with an active gamesession.
		/// </summary>
		/// <param name="userId">The user id to check.</param>
		/// <returns>True if a game session exists for the user.</returns>
		Task<bool> HasSession(int userId);

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
