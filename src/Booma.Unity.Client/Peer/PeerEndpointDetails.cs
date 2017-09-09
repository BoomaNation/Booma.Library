using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	/// <summary>
	/// Encapsulates the details required for a peer to connect to a remote host.
	/// Serializable to the Unity3D inspector.
	/// </summary>
	[Serializable]
	public class PeerEndpointDetails
	{
		//TODO: Add pragmas
		/// <summary>
		/// The endpoint to connect to with this peer.
		/// </summary>
		[Tooltip("Either the remote-host's IP or domain name.")]
		[SerializeField]
		private string endpointName;
		public string EndpointName => endpointName;

		/// <summary>
		/// The endpoint port to connect through with this peer.
		/// </summary>
		[SerializeField]
		private int endpointPort;
		public int EndpointPort => endpointPort;

		/// <summary>
		/// Application name of the remote service.
		/// </summary>
		[SerializeField]
		private string applicationName;
		public string ApplicationName => applicationName;

		/// <summary>
		/// Computes the full server address for the remote host.
		/// </summary>
		/// <returns>Returns the full name of the server</returns>
		public string ComputeServerAddress()
		{
			return $"{EndpointName}:{EndpointPort}";
		}
	}
}
