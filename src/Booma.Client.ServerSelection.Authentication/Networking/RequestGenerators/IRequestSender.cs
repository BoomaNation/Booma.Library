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
		/// Includes <see cref="SendResult"/> as a return value.
		/// </summary>
		/// <returns>Returns the <see cref="SendResult"/> of the request sending operation.</returns>
		SendResult SendRequestWithResult();

		/// <summary>
		/// Attempts to send a request.
		/// </summary>
		void SendRequest();
	}
}
