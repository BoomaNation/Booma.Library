using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booma
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

		/// <summary>
		/// Attempts to claim the session with the associated guid.
		/// If it doesn't exist it will fail or if it is already claimed it will fail.
		/// It will also fail if the user is logged in.
		/// </summary>
		/// <param name="sessionGuid">The session guid to claim.</param>
		/// <returns>True if the session is claimed.</returns>
		Task<bool> TryClaimSession(Guid sessionGuid);
	}
}
