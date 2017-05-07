using Booma.Common.ServerSelection;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Booma.Payloads.ServerSelection
{
	/// <summary>
	/// Wire-ready model of the gameserver details.
	/// </summary>
	[GladNetSerializationContract]
	public class SimpleGameServerDetailsModel
	{
		//Basically a subset of the DB model
		/// <summary>
		/// Name of the server (Ex. Vegas)
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public string Name { get; private set; }

		/// <summary>
		/// Serializable byte array representing the servers's <see cref="IPAddress"/>.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index2)]
		private byte[] serverIPBytes;

		private IPAddress _ServerIP;
		/// <summary>
		/// Represents the IP address of the server.
		/// </summary>
		public IPAddress ServerIP { get { return _ServerIP == null ? _ServerIP = new IPAddress(serverIPBytes) : _ServerIP; } }

		/// <summary>
		/// Port incoming client connections should be on.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index3)]
		public int ServerPort { get; private set; }

		/// <summary>
		/// Region of the game server.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index4)]
		public ServerRegion Region { get; private set; }

		public SimpleGameServerDetailsModel(string name, IPAddress address, int port, ServerRegion region)
		{
			Name = name;
			serverIPBytes = address.GetAddressBytes();
			ServerPort = port;
			Region = region;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		protected SimpleGameServerDetailsModel()
		{

		}
	}
}
