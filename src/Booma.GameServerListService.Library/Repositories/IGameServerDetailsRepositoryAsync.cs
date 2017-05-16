using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booma.GameServerList.Lib
{
	/// <summary>
	/// Repository service for accessing GameServer list/details data async.
	/// </summary>
	public interface IGameServerDetailsRepositoryAsync
	{
		/// <summary>
		/// Retrivices a collection of game server details that are
		/// public game servers.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<GameServerDetailsModel>> GetAllPublic();
	}
}
