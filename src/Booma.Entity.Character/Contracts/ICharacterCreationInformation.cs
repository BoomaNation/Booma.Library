using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma
{
	/// <summary>
	/// Contracts that contain information for character creation.
	/// </summary>
	public interface ICharacterCreationInformation
	{
		/// <summary>
		/// The character's name.
		/// </summary>
		string CharacterName { get; }

		/// <summary>
		/// The class to request a creation for.
		/// </summary>
		CharacterClassRace CharacterClass { get; }

		//TODO: Appearance stuff
	}
}
