using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Database table/model for the characters in the Booma project.
	/// </summary>
	[Table("characters")]
	public class CharacterDatabaseModel
	{
		/// <summary>
		/// The primary unique integer key for the character.
		/// </summary>
		[Key]
		[Range(0, int.MaxValue)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Key for the account associated with the character.
		/// </summary>
		[Required]
		[Range(0, int.MaxValue)]
		public int AccountId { get; private set; }

		//TODO: How long should a character name be?
		/// <summary>
		/// The character's name.
		/// </summary>
		[Required]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "CharacterName exceeded the bound limitation.")]
		public string CharacterName { get; private set; }

		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreationTime { get; private set; }
		
		[Required]
		[StringLength(15, MinimumLength = 7)] //IP constraints
		public string CreationIp { get; private set; }

		[Required]
		[DefaultValue(false)]
		public bool isLoggedIn { get; private set; }

		public CharacterDatabaseModel(int accountId, string characterName, string creationIp)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));
			if(!CharacterNameValidator.isNameValid(characterName)) throw new ArgumentException($"Provided Name: {characterName} must contain only letters or digits.", nameof(characterName));
			if(string.IsNullOrWhiteSpace(creationIp)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(creationIp));

			//TODO: Should we validate it's an IpAddress?

			AccountId = accountId;
			CharacterName = characterName;
			CreationIp = creationIp;
		}

		protected CharacterDatabaseModel()
		{
			
		}
	}
}
