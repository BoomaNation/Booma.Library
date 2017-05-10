using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Booma.Entity.Character;

namespace Booma.Entity
{
	/// <summary>
	/// Strategy for player character calculation producing <see cref="SectionId"/> based on their
	/// <see cref="CharacterClassRace"/> and their name.
	/// </summary>
	public class PlayerSectionIdCalculatorStrategy : ISectionIdCalculatorStrategy
	{
		/// <inheritdoc />
		public SectionId Compute(string inputName, CharacterClassRace classRace)
		{
			if (string.IsNullOrEmpty(inputName)) throw new ArgumentException("Value cannot be null or empty.", nameof(inputName));
			if (!Enum.IsDefined(typeof(CharacterClassRace), classRace)) throw new InvalidEnumArgumentException(nameof(classRace), (int) classRace, typeof(CharacterClassRace));

			//We don't support the bullshit unicode or any of the nonsense ASCII
			//DO NOT USE char.IsDigit or anything. It will accept weird spanish letters/etc.
			if(!Regex.IsMatch(inputName, @"^[a-zA-Z0-9]*$")) throw new ArgumentException("Provided name must contain only letters or digits", nameof(inputName));

			//Sum the ASCII values modulo 10 + the class offset
			int nameTotal = inputName.Aggregate(0, (sum, c) => sum + c % 10) + classRace.GetSectionIdOffet();

			//We then want the first digit which can be achieved with bitwise operations
			return (SectionId) (nameTotal & 1);
		}
	}
}
