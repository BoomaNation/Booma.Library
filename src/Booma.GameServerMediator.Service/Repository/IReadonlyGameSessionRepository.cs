using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booma
{
	//TODO: Refactor into writable and readonly interface
	public interface IReadOnlyGameSessionRepository
	{
		/// <summary>
		/// Indicates if the <see cref="userId"/> is associated with an active gamesession.
		/// </summary>
		/// <param name="userId">The user id to check.</param>
		/// <returns>True if a game session exists for the user.</returns>
		Task<bool> HasSession(int userId);

		/// <summary>
		/// Indicates if the <see cref="sessionGuid"/> is associated with an active gamesession.
		/// </summary>
		/// <param name="sessionGuid">Session guid to check for.</param>
		/// <returns>True if a game session exists with the specified session guid..</returns>
		Task<bool> HasSession(Guid sessionGuid);

		/// <summary>
		/// Gets the <see cref="GameSessionModel"/> associated with a specified guid.
		/// </summary>
		/// <param name="sessionGuid">The session guid to use for lookup.</param>
		/// <returns>The session model.</returns>
		Task<GameSessionModel> GetSessionByGuid(Guid sessionGuid);

		/// <summary>
		/// Gets the <see cref="GameSessionModel"/> associated with a specified user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns>The session model.</returns>
		Task<GameSessionModel> GetSessionById(int userId);
	}
}
