using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Booma
{
	/// <summary>
	/// Contains information for a character creation request.
	/// </summary>
	[JsonObject]
	public sealed class CharacterCreationRequest : ICharacterCreationInformation
	{
		/// <summary>
		/// The character's name.
		/// </summary>
		[JsonProperty]
		public string CharacterName { get; private set; }

		/// <summary>
		/// The class to request a creation for.
		/// </summary>
		[JsonProperty]
		public CharacterClassRace CharacterClass { get; private set; }

		//TODO: Add visual data here. Such as hair, color, costumes and etc.

		public CharacterCreationRequest(string characterName, CharacterClassRace characterClass)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));
			if(!Enum.IsDefined(typeof(CharacterClassRace), characterClass)) throw new ArgumentOutOfRangeException(nameof(characterClass), "Value should be defined in the CharacterClassRace enum.");

			CharacterName = characterName;
			CharacterClass = characterClass;
		}
	}
}
