using GladNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Contract for types that provide request sending functionality
	/// without input.
	/// </summary>
	public interface IRequestSender
	{
		/// <summary>
		/// Attempts to send a request.
		/// </summary>
		/// <returns>Returns the <see cref="SendResult"/> of the request sending operation.</returns>
		SendResult SendRequest();
	}
}
