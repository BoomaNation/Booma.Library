using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Booma.ServerSelection.Common;

namespace Booma.GameServerList.Lib
{
	/// <summary>
	/// Represents a simplistic model for a
	/// </summary>
	public class GameServerDetailsModel
	{
		[Key]
		public int GameServerId { get; private set; }

		/// <summary>
		/// Name of the server (Ex. Vegas)
		/// </summary>
		public string Name { get; set; }

		public string Address { get; set; }

		/// <summary>
		/// Port incoming client connections should be on.
		/// </summary>
		public int ServerPort { get; set; }

		/// <summary>
		/// Indicates the status of the server.
		/// </summary>
		public ServerStatus Status { get; set; }

		/// <summary>
		/// Region of the game server.
		/// </summary>
		public ServerRegion Region { get; set; }

		//We don't have any population information so that we don't have to couple this
		//to the actual gameservice
	}
}
