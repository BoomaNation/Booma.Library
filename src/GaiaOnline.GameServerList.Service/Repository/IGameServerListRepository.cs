using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Network.Common;

namespace GaiaOnline
{
	//TODO: Should probably expand this to dev vs test vs public servers
	public interface IGameServerListRepository
	{
		/// <summary>
		/// Loads all of the <see cref="GameServerListEntryModel"/>.
		/// </summary>
		/// <returns>The collection of all gameservers.</returns>
		Task<IEnumerable<GameServerListEntryModel>> RetrieveAll();
	}
}
