using Booma.Client.Network.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using UnityEngine.Events;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// The login network client for the Boom project
	/// </summary>
	public class LoginNetworkClient : BoomaNetworkClientPeer<LoginNetworkClient>
	{
		/// <summary>
		/// Called when the peer enters a connected state
		/// </summary>
		[Tooltip("Called when the peer enters a connected state.")]
		[SerializeField]
		private NetworkStatusUnityEvent OnConnectionEstablished;

		/// <summary>
		/// Called when the peer successfully establishes a secure channel.
		/// </summary>
		[Tooltip("Called when the peer successfully establishes a secure channel.")]
		[SerializeField]
		private NetworkStatusUnityEvent OnEncryptionEstablished;

		public override void OnStatusChanged(NetStatus status)
		{
			switch(status)
			{
				case NetStatus.Connected:
					if(OnConnectionEstablished != null)
						OnConnectionEstablished.Invoke(this, status);
					break;
				case NetStatus.EncryptionEstablished:
					if(OnConnectionEstablished != null)
						OnEncryptionEstablished.Invoke(this, status);
					break;
			}
		}
	}
}
