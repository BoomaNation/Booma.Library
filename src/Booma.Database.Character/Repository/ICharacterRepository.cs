using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public interface ICharacterRepository : IReadonlyCharacterRepository
	{

	}

	public interface IReadonlyCharacterRepository
	{
		/// <summary>
		/// Indicates if the name is in the character database.
		/// </summary>
		/// <param name="characterName">The character name.</param>
		/// <returns>True if there is an entry with that name.</returns>
		Task<bool> DoesNameExist(string characterName);
	}
}
