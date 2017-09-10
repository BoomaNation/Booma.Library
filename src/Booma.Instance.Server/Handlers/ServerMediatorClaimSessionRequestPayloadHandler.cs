using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SceneJect.Common;

namespace Booma
{
	[Injectee]
	public sealed class ServerMediatorClaimSessionRequestPayloadHandler : ClaimSessionRequestPayloadHandler
	{
		/// <summary>
		/// The service that can allow the server to try to claim the session.
		/// </summary>
		[Inject]
		private IGameServerSessionService SessionService { get; }

		protected override async Task<ServerSessionClaimResponse> TryClaimSession(Guid sessionGuid, [NotNull] string ip)
		{
			if(string.IsNullOrWhiteSpace(ip)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(ip));

			return await SessionService.TryClaimSession(new ServerSessionClaimRequest(sessionGuid, ip));
		}
	}
}
