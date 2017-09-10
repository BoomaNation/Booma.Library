using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TypeSafe.Http.Net;

namespace Booma
{
	public interface IGameServerSessionService
	{
		//TODO: We should authorize with OAuth2 so that only servers can validate
		/// <summary>
		/// Inquires about a session.
		/// </summary>
		/// <param name="inquiry">The inquiry.</param>
		/// <returns>The result of the inquiry.</returns>
		[Post("/api/validate")]
		Task<ServerSessionClaimRequest> TryClaimSession([JsonBody] ServerSessionClaimRequest inquiry);
	}
}
