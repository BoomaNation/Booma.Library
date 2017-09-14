using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// The creation result of trying to create a new character.
	/// </summary>
	public sealed class CharacterCreationResult
	{
		/// <summary>
		/// The section ID of the newly created character.
		/// </summary>
		public SectionId CharacterSectionId { get; }

		/// <summary>
		/// Indicates if the creation result is successful.
		/// </summary>
		public bool IsSuccessful { get; }

		public CharacterCreationResult(SectionId characterSectionId, bool isSuccessful)
		{
			if(!Enum.IsDefined(typeof(SectionId), characterSectionId)) throw new ArgumentOutOfRangeException(nameof(characterSectionId), "Value should be defined in the SectionId enum.");

			CharacterSectionId = characterSectionId;
			IsSuccessful = isSuccessful;
		}
	}
}
