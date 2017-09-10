using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace Booma
{
	[Table("character_appearance")]
	public class CharacterAppearanceModel
	{
		/// <summary>
		/// The unique id for the character.
		/// Is the foreign key to the <see cref="CharacterDatabaseModel"/> table.
		/// </summary>
		[Key]
		[Required]
		[ForeignKey(nameof(CharacterEntry))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Foreign reference to the characters table.
		/// </summary>
		[Required]
		public virtual CharacterDatabaseModel CharacterEntry { get; private set; }

		/// <summary>
		/// Section id for the character.
		/// </summary>
		[Required]
		[Range(0, int.MaxValue)] //TODO: Can we constrain this better?
		public SectionId SectionId { get; private set; }

		/// <summary>
		/// The character's class.
		/// </summary>
		[Required]
		[Column("Class")]
		[Range(0, int.MaxValue)] //TODO: Can we constrain this better?
		public CharacterClassRace CharacterClass { get; private set; }

		//TODO: add the actual visual details
		public CharacterAppearanceModel(int characterId, SectionId sectionId, CharacterClassRace characterClass)
		{
			if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(!Enum.IsDefined(typeof(SectionId), sectionId)) throw new ArgumentOutOfRangeException(nameof(sectionId), "Value should be defined in the SectionId enum.");
			if(!Enum.IsDefined(typeof(CharacterClassRace), characterClass)) throw new ArgumentOutOfRangeException(nameof(characterClass), "Value should be defined in the CharacterClassRace enum.");

			CharacterId = characterId;
			SectionId = sectionId;
			CharacterClass = characterClass;
		}

		protected CharacterAppearanceModel()
		{
			
		}
	}
}
