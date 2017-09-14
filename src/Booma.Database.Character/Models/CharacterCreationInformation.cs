using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	public sealed class CharacterCreationInformation : ICharacterCreationInformation
	{
		/// <inheritdoc />
		public string CharacterName { get; }

		/// <inheritdoc />
		public CharacterClassRace CharacterClass { get; }

		public CharacterCreationInformation(string characterName, CharacterClassRace characterClass)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));
			if(!Enum.IsDefined(typeof(CharacterClassRace), characterClass)) throw new ArgumentOutOfRangeException(nameof(characterClass), "Value should be defined in the CharacterClassRace enum.");

			CharacterName = characterName;
			CharacterClass = characterClass;
		}
	}
}
