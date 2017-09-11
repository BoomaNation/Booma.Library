using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Booma
{
	/// <summary>
	/// 
	/// </summary>
	[JsonObject]
	public sealed class CharacterNameValidationRequest
	{
		/// <summary>
		/// The character name to validate.
		/// </summary>
		[JsonProperty]
		public string CharacterName { get; private set; }

		public CharacterNameValidationRequest(string characterName)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));

			CharacterName = characterName;
		}
	}
}
