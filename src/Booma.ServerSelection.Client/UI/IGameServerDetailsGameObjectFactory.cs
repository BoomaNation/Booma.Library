using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.ServerSelection.Common;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	public interface IGameServerDetailsGameObjectFactory
	{
		/// <summary>
		/// Creates a new UI button for the server details.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="region"></param>
		/// <returns></returns>
		GameObject Create(string name, ServerRegion region);
	}
}
