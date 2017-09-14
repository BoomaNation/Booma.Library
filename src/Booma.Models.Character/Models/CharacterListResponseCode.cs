namespace Booma
{
	/// <summary>
	/// Enumeration of possible character list response codes.
	/// </summary>
	public enum CharacterListResponseCode
	{
		/// <summary>
		/// Indicates that the request was successful.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the account doesn't exist.
		/// </summary>
		AccountDoesNotExist = 1,

		/// <summary>
		/// Indicates that an unknown/general error occured.
		/// </summary>
		GeneralServerError = 2,
	}
}