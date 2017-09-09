using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using GladNet.Common;
using GladNet.Engine.Common;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma
{
	[Injectee]
	public abstract class RequestGenerator : SerializedMonoBehaviour, IRequestSender
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

		private void Start()
		{
			if (NetworkPeer == null)
				throw new InvalidOperationException($"Field {nameof(_NetworkPeer)} was null.");

			if (Logger == null)
				throw new InvalidOperationException($"Field {nameof(Logger)} was null.");

			OnStart();
		}

		protected virtual void OnStart()
		{
			//do nothing; let overriders implement if they want.
		}

		/// <inheritdoc />
		public abstract void SendRequest();

		/// <inheritdoc />
		public abstract SendResult SendRequestWithResult();
	}
}
