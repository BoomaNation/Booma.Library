using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Network.Common;

namespace Booma
{
	/// <summary>
	/// Data model that represents an entry for a game server.
	/// </summary>
	[Table("GameServers")]
	public sealed class GameServerListEntryModel
	{
		/// <summary>
		/// PKey for the table
		/// </summary>
		[Key]
		public int Id { get; private set; }

		/// <summary>
		/// The name of the server.
		/// </summary>
		[Required]
		public string ServerName { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the region of the endpoint.
		/// </summary>
		[Required]
		public ClientRegionLocale Region { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the endpoint address.
		/// </summary>
		[Required]
		public string EndpointAddress { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the endpoint's port.
		/// </summary>
		[Required]
		public int EndpointPort { get; private set; } //must have setter for EF

		public GameServerListEntryModel(ClientRegionLocale region, string endpointAddress, int endpointPort)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");
			if (string.IsNullOrWhiteSpace(endpointAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpointAddress));

			Region = region;
			EndpointAddress = endpointAddress;
			EndpointPort = endpointPort;
		}

		/// <summary>
		/// Ef required constructor.
		/// </summary>
		public GameServerListEntryModel()
		{

		}
	}
}
