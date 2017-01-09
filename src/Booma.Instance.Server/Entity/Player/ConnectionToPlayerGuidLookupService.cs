using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Entity.Identity;

namespace Booma.Instance.Server
{
	//TODO: document; thread safety
	public class ConnectionToPlayerGuidLookupService : IConnectionToGuidLookupService, IConnectionToGuidRegistry
	{
		private Dictionary<NetworkEntityGuid, int> GuidToConnectionMap;

		private Dictionary<int, NetworkEntityGuid> ConnectionToGuidMap;

		public ConnectionToPlayerGuidLookupService()
		{
			GuidToConnectionMap = new Dictionary<NetworkEntityGuid, int>(4);
			ConnectionToGuidMap = new Dictionary<int, NetworkEntityGuid>(4);
		}

		public bool Contains(int connectionId)
		{
			return ConnectionToGuidMap.ContainsKey(connectionId);
		}

		public bool Contains(NetworkEntityGuid guid)
		{
			return GuidToConnectionMap.ContainsKey(guid);
		}

		public int Lookup(NetworkEntityGuid guid)
		{
			return GuidToConnectionMap[guid];
		}

		public NetworkEntityGuid Lookup(int connectionId)
		{
			return ConnectionToGuidMap[connectionId];
		}

		public void Register(int connectionId, NetworkEntityGuid guid)
		{
			GuidToConnectionMap[guid] = connectionId;
			ConnectionToGuidMap[connectionId] = guid;
		}
	}
}
