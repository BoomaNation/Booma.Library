using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneJect.Common;

namespace GaiaOnline
{
	public class CachedNameQueryService : IAvatarNameQueryService
	{
		[Inject]
		private IGameServerMediatorService NameQueryService { get; }

		/// <summary>
		/// Local cache of results for translating the avatar id to name.
		/// </summary>
		private ConcurrentDictionary<int, string> cachedAvatarMap { get; }

		public CachedNameQueryService()
		{
			cachedAvatarMap = new ConcurrentDictionary<int, string>();
		}

		/// <inheritdoc />
		public async Task<string> GetNameById(int id)
		{
			if(cachedAvatarMap.ContainsKey(id))
				return cachedAvatarMap[id];

			NameQueryResponse nameQueryResponse = await NameQueryService.GetAvatarNameById(id);

			if(nameQueryResponse.isSuccessful)
				return cachedAvatarMap[id] = nameQueryResponse.AvatarName;

			//It's possible to fail this request, though it shouldn't happen
			throw new InvalidOperationException($"Failed to query name for Entity: {id}");
		}
	}
}
