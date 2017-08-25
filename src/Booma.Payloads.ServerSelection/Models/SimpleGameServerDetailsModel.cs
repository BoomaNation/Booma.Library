using Booma.ServerSelection.Common;
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
		[GladNetMember(1)]
		public string Name { get; private set; }

		/// <summary>
		/// Serializable byte array representing the servers's <see cref="IPAddress"/>.
		/// </summary>
		[GladNetMember(2)]
		private byte[] serverIPBytes;

		private Lazy<IPAddress> _ServerIP { get; }

		/// <summary>
		/// Represents the IP address of the server.
		/// </summary>
		public IPAddress ServerIP => _ServerIP.Value;

		/// <summary>
		/// Port incoming client connections should be on.
		/// </summary>
		[GladNetMember(3)]
		public int ServerPort { get; private set; }

		/// <summary>
		/// Region of the game server.
		/// </summary>
		[GladNetMember(4)]
		public ServerRegion Region { get; private set; }

		public SimpleGameServerDetailsModel(string name, IPAddress address, int port, ServerRegion region)
		{
			Name = name;
			serverIPBytes = address.GetAddressBytes();
			ServerPort = port;
			Region = region;

			_ServerIP = new Lazy<IPAddress>(CreateAddressFromBytes, true);
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		protected SimpleGameServerDetailsModel()
		{
			_ServerIP = new Lazy<IPAddress>(CreateAddressFromBytes, true);
		}

		private IPAddress CreateAddressFromBytes()
		{
			if (serverIPBytes == null || serverIPBytes.Length == 0)
				throw new InvalidOperationException($"Cannot create address. Serialized bytes {serverIPBytes} was nutll.");

			return new IPAddress(serverIPBytes);
		}
	}
}
