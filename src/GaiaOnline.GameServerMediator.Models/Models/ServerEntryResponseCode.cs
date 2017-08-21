namespace GaiaOnline
{
	public enum ServerEntryResponseCode
	{
		/// <summary>
		/// Indicates that the model is successful and thus initialized fully.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the token provided is invalid.
		/// </summary>
		FailedInvalidToken = 1,

		/// <summary>
		/// Connection was actively refused for some reason.
		/// </summary>
		FailedConnectionActivelyRefused = 2,

		/// <summary>
		/// Indicates that the account associated with the token is already playing.
		/// </summary>
		FailedAlreadyLoggedIn = 3,

		/// <summary>
		/// Indicates a general server error.
		/// </summary>
		GeneralServerError = 4,
	}
}