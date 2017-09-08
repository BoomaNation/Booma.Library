using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;

namespace GaiaOnline
{
	/// <summary>
	/// HTTP client to get the gameserver list.
	/// </summary>
	public interface IGameServiceListQueryClient
	{
		/// <summary>
		/// Gets a list of available gameservers.
		/// </summary>
		/// <returns>A list of all available gameservers.</returns>
		[Get("/api/gameserver/list")]
		GameServerListResponse GetGameServerList(); //no parameters required
	}
}
