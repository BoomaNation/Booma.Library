using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GaiaOnline
{
	/// <summary>
	/// Enumeration of possible <see cref="PlayerPrefs"/> entries.
	/// </summary>
	public enum PlayerPreferences
	{
		/// <summary>
		/// The stored IP of the gameserver.
		/// </summary>
		GameServerIp = 0,

		/// <summary>
		/// The stored port of the gameserver.
		/// </summary>
		GameServerPort = 1,

		/// <summary>
		/// The stored name of the gameserver.
		/// </summary>
		GameServerName = 2,
	}
}
