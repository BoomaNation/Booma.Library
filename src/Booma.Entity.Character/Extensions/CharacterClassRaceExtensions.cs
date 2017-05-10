using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace Booma.Entity.Character
{
	/// <summary>
	/// Extension methods for the <see cref="CharacterClassRace"/> enumeration Type.
	/// </summary>
	public static class CharacterClassRaceExtensions
	{
		//TODO: When Unity3D supports netstandard or net46 use https://github.com/HelloKitty/Reflect.Extent for generic enum attribute reading
		/// <summary>
		/// Dictionary that maps a <see cref="CharacterClassRace"/> value to the <see cref="FieldInfo"/>.
		/// </summary>
		private static Dictionary<CharacterClassRace, FieldInfo> ClassRaceToFieldMap { get; } = new Dictionary<CharacterClassRace, FieldInfo>(Enum.GetValues(typeof(CharacterClassRace)).Length);

		/// <summary>
		/// Gets the offset value for the section ID.
		/// </summary>
		/// <param name="classRace">The class race value.</param>
		/// <exception cref="InvalidEnumArgumentException">Throws if <see cref="classRace"/> is out of range.</exception>
		/// <returns>The offset value.</returns>
		public static int GetSectionIdOffet(this CharacterClassRace classRace)
		{
			if (!Enum.IsDefined(typeof(CharacterClassRace), classRace)) throw new InvalidEnumArgumentException(nameof(classRace), (int) classRace, typeof(CharacterClassRace));

			if (ClassRaceToFieldMap.ContainsKey(classRace))
				return ClassRaceToFieldMap[classRace].Attribute<SectionIdWeightAttribute>().SectionIdWeight;
			else
			{
				//Use reflection to read the metdata tagged on the class race
				ClassRaceToFieldMap[classRace] = typeof(CharacterClassRace).GetField(classRace.ToString());

				//just recur
				return GetSectionIdOffet(classRace);
			}
		}
	}
}
