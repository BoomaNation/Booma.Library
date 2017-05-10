using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Booma.Entity.Character
{
	/// <summary>
	/// Strategy for computing the section ID for a given character.
	/// </summary>
	public interface ISectionIdCalculatorStrategy
	{
		/// <summary>
		/// Computes the <see cref="SectionId"/> from the provided <see cref="inputName"/> and
		/// the character <see cref="classRace"/>.
		/// </summary>
		/// <param name="inputName">The character's name.</param>
		/// <param name="classRace">The character's class.</param>
		/// <exception cref="ArgumentNullException">Throws if <see cref="inputName"/> is null.</exception>
		/// <exception cref="ArgumentException">Throws if <see cref="inputName"/> empty or non-alphanumeric.</exception>
		/// <exception cref="InvalidEnumArgumentException">Throws if <see cref="CharacterClassRace"/> is out of range.</exception>
		/// <returns>The <see cref="SectionId"/> based on the provided inputs.</returns>
		SectionId Compute([NotNull] string inputName, CharacterClassRace classRace);
	}
}
