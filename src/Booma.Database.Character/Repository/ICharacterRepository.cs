using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	public interface ICharacterRepository : IReadonlyCharacterRepository
	{
		/// <summary>
		/// Tries to create a new character in the repository.
		/// </summary>
		/// <param name="characterCreationInformation">The character information.</param>
		/// <returns>The result of the request.</returns>
		Task<CharacterCreationResult> TryCreateNewCharacter(CharacterCreationInformation characterCreationInformation);
	}

	public interface IReadonlyCharacterRepository
	{
		/// <summary>
		/// Indicates if the name is in the character database.
		/// </summary>
		/// <param name="characterName">The character name.</param>
		/// <returns>True if there is an entry with that name.</returns>
		Task<bool> DoesNameExist(string characterName);

		/// <summary>
		/// Loads the character's named based on the provided id.
		/// If no name was loaded it returns a null name.
		/// </summary>
		/// <param name="id">The character id to check.</param>
		/// <returns>The character's name or null if no character was found.</returns>
		Task<string> GetCharacterName(int id);

		/// <summary>
		/// Loads all the associated character's Ids that are
		/// associated with the provided <see cref="accountId"/>.
		/// </summary>
		/// <param name="accountId">The account id to load characters for.</param>
		/// <returns>Collection of all character Id's that account is associated with.</returns>
		Task<IEnumerable<int>> LoadAssociatedCharacterIds(int accountId);
	}
}
