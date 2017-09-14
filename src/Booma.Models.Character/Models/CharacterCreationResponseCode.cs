namespace Booma
{
	/// <summary>
	/// Enumeration of all response codes for <see cref="CharacterCreationResponse"/>.
	/// </summary>
	public enum CharacterCreationResponseCode
	{
		/// <summary>
		/// Indicates success.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the account has too many characters
		/// associated with it.
		/// </summary>
		TooManyCharacters = 1,

		/// <summary>
		/// Indicates that the requested name is invalid.
		/// </summary>
		NameInvalid = 2,

		/// <summary>
		/// Indicates a general server error.
		/// </summary>
		GeneralServerError = 3,
	}
}