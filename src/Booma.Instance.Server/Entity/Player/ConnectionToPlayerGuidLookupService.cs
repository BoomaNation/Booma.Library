using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;

namespace Booma
{
	//TODO: document; thread safety
	public class ConnectionToPlayerGuidLookupService : IConnectionToGuidLookupService, IConnectionToGuidRegistry
	{
		//TODO: Use a 2-way map/dict
		private Dictionary<NetworkEntityGuid, int> GuidToConnectionMap { get; }

		private Dictionary<int, NetworkEntityGuid> ConnectionToGuidMap { get; }

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
