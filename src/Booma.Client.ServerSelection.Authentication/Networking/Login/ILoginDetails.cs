using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Client.ServerSelection.Authentication
{
	public interface ILoginDetails
	{
		/// <summary>
		/// String required for a login/authentication
		/// (Ex. Username, Email, One-off token)
		/// </summary>
		string LoginString { get; }

		/// <summary>
		/// Password used for authentication
		/// </summary>
		string Password { get; }
	}
}
