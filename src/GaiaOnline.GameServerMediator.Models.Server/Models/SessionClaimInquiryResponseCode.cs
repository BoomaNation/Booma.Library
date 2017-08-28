namespace GaiaOnline
{
	public enum SessionClaimInquiryResponseCode
	{
		/// <summary>
		/// Indicates success.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that no session was found.
		/// </summary>
		FailedNoSessionRegistered = 1,

		/// <summary>
		/// Indicates that the session was expired.
		/// </summary>
		FailedSessionExpired = 2,

		/// <summary>
		/// Indicates some general server error occured causing a failure.
		/// </summary>
		FailedGeneralServerError = 3
	}
}