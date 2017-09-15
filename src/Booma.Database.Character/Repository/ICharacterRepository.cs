using System;
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
		Task<CharacterCreationResult> TryCreateNewCharacter(int accountId, string creationIp, CharacterCreationInformation characterCreationInformation);
	}
}
