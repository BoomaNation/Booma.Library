using System;
using System.Collections.Generic;
using System.Text;

namespace GaiaOnline
{
	/// <summary>
	/// Enumeration of all response codes for a name query.
	/// </summary>
	public enum NameQueryResponseCode
	{
		/// <summary>
		/// Indicates that the namequery was a sucess.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the provided user id was unknown.
		/// </summary>
		UnknownUserId = 1,

		/// <summary>
		/// Indicates that a general error was encountered.
		/// </summary>
		GeneralServerError = 2,
	}
}
