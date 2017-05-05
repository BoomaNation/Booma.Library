using Booma.Payloads.ServerSelection;
using Common.Logging;
using GladBehaviour.Common;
using GladNet.Common;
using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Component is capable of sending gameserver list requests through the
	/// peer to the currently connected network.
	/// </summary>
	public class GameServerListRequestGenerator : GladMonoBehaviour, IRequestSender
	{
		//TODO: Put this in a base class.
		[SerializeField]
		private INetPeer messageSender;

		[Inject]
		private ILog logger;

		public void Start()
		{
			if (messageSender == null)
				throw new InvalidOperationException($"Field {nameof(messageSender)} was null.");

			if (logger == null)
				throw new InvalidOperationException($"Field {nameof(logger)} was null.");
		}

		public void SendRequest()
		{
			SendRequestWithResult();
		}

		public SendResult SendRequestWithResult()
		{
			if(logger.IsDebugEnabled)
				logger.Debug("Sending list request.");

			return messageSender.TrySendMessage(OperationType.Request, new GameServerListRequestPayload(), DeliveryMethod.ReliableOrdered, true);
		}
	}
}
