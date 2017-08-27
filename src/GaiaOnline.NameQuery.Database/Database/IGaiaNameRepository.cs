using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GaiaOnline
{
	public interface IGaiaNameRepository : IReadonlyGaiaNameRepository
	{
		/// <summary>
		/// Tries to insert a new entry with the provided <see cref="avatarUsername"/>
		/// and <see cref="userId"/>.
		/// </summary>
		/// <param name="avatarUsername">The username of the avatar.</param>
		/// <param name="userId">The user id.</param>
		/// <returns>True if it exists or was added successfully.</returns>
		Task<bool> InsertEntry(string avatarUsername, int userId);
	}
}
