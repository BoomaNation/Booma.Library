using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Booma
{
	public sealed class DatabaseCharacterRepository : ICharacterRepository
	{
		private CharacterDatabaseContext Context { get; }

		private ISectionIdCalculatorStrategy SectionIdCalculator { get; }

		/// <inheritdoc />
		public DatabaseCharacterRepository(CharacterDatabaseContext context, ISectionIdCalculatorStrategy sectionIdCalculator)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(sectionIdCalculator == null) throw new ArgumentNullException(nameof(sectionIdCalculator));

			Context = context;
			SectionIdCalculator = sectionIdCalculator;
		}

		/// <inheritdoc />
		public async Task<bool> DoesNameExist(string characterName)
		{
			return await Context.Characters
				.AnyAsync(c => c.CharacterName == characterName);
		}

		/// <inheritdoc />
		public async Task<string> GetCharacterName(int id)
		{
			return (await Context.Characters.FirstOrDefaultAsync(c => c.CharacterId == id))?.CharacterName;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<int>> LoadAssociatedCharacterIds(int accountId)
		{
			return await Context.Characters
				.Where(c => c.AccountId == accountId)
				.Select(c => c.CharacterId)
				.ToArrayAsync();
		}

		/// <inheritdoc />
		public async Task<CharacterCreationResult> TryCreateNewCharacter(int accountId, string creationIp, CharacterCreationInformation characterCreationInformation)
		{
			//First we should add the character to the database
			EntityEntry<CharacterDatabaseModel> entry = await Context.Characters
				.AddAsync(new CharacterDatabaseModel(accountId, characterCreationInformation.CharacterName, creationIp));

			//Then we need to compute the section id and create an entry for the appearance
			SectionId sectionId = SectionIdCalculator.Compute(characterCreationInformation.CharacterName, characterCreationInformation.CharacterClass);

			await Context.CharacterAppearances
				.AddAsync(new CharacterAppearanceModel(entry.Entity.CharacterId, sectionId, characterCreationInformation.CharacterClass));

			//TODO: Can the entry be invalid?
			bool successfullyAdded = (await Context.SaveChangesAsync() > 0);

			return new CharacterCreationResult(sectionId, successfullyAdded);
		}
	}
}
