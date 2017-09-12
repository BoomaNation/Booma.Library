namespace Booma
{
	public enum CharacterNameValidationResponseCode
	{
		/// <summary>
		/// Means the validation was successful.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Means that the name is already taken or unavailable.
		/// </summary>
		NameAlreadyTaken = 1,

		/// <summary>
		/// Means the name contained invalid characters.
		/// </summary>
		NameContainsInvalidCharacters = 2,

		/// <summary>
		/// Means the name was an invalid length.
		/// </summary>
		NameIsInvalidLength = 3,

		/// <summary>
		/// Indicates a server error.
		/// </summary>
		GeneralServerError = 4,
	}
}