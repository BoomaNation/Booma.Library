using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using GladBehaviour.Common;
using GladNet.Common;
using GladNet.Engine.Common;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Unity.Network
{
	[Injectee]
	public abstract class RequestGenerator : GladMonoBehaviour
	{
		//TODO: How should we chooce if we should inject this or set it with GladMonoBehaviour?
		[SerializeField]
		[Tooltip("The network peer to use for sending the request.")]
		private INetPeer _NetworkPeer;

		/// <summary>
		/// The <see cref="INetPeer"/> service to use for sending the request.
		/// </summary>
		protected INetPeer NetworkPeer => _NetworkPeer;

		/// <summary>
		/// Injected logging service.
		/// </summary>
		[Inject]
		public readonly ILog Logger;

		/// <summary>
		/// Sends the request this <see cref="RequestGenerator"/> was created to send.
		/// </summary>
		public abstract void SendRequest();

		/// <summary>
		/// Sends the request this <see cref="RequestGenerator"/> was created to send.
		/// Also returns the <see cref="SendResult"/>
		/// </summary>
		public abstract SendResult SendRequestWithResult();
	}
}
