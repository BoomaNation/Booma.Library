using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GaiaOnline
{
	public interface IReadonlyGaiaNameRepository
	{
		/// <summary>
		/// Gets the name of the user by their corresponding username.
		/// </summary>
		/// <param name="userId">The userId</param>
		/// <returns>The avatar name that corresponds to the user id.</returns>
		Task<string> GetNameById(int userId);

		/// <summary>
		/// Indicates if the user id is known.
		/// </summary>
		/// <param name="userId">User id to check.</param>
		/// <returns>True if the user id is known.</returns>
		Task<bool> DoesEntryExist(int userId);

		/// <summary>
		/// Indicates if the username is known.
		/// </summary>
		/// <param name="avatarName">Username to check.</param>
		/// <returns>True if the username is known.</returns>
		Task<bool> DoesEntryExist(string avatarName);
	}
}
