using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;

namespace Booma
{
	/// <summary>
	/// Client that provides acess to the mediator service for a gameserver
	/// allowing you to enter the gameserver or move between zones in one.
	/// </summary>
	public interface IGameServerMediatorService
	{
		/// <summary>
		/// Attempts to enter the gameserver. Requesting access through the mediator.
		/// It could fail for several reasons.
		/// </summary>
		/// <param name="token">The access token.</param>
		/// <returns>The result of the attempted entry.</returns>
		[Post("/api/gateway/enter")]
		Task<ServerEntryResponse> TryEnterGameServer([DynamicHeader("Authorization")] string token);

		/// <summary>
		/// Attempts to translate an <see cref="entityId"/> to a corresponding username.
		/// </summary>
		/// <param name="entityId">The entity id to check.</param>
		/// <returns>The name of the user associated with the entity id.</returns>
		[Get("/api/avatar/namequery/{id}")]
		Task<NameQueryResponse> GetAvatarNameById([AliasAs("id")] int entityId);
	}
}
