using Booma.Payloads.ServerSelection;
using Common.Logging;
using GladBehaviour.Common;
using GladNet.Common;
using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Unity.Network;
using SceneJect.Common;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Component is capable of sending gameserver list requests through the
	/// peer to the currently connected network.
	/// </summary>
	[Injectee]
	public class GameServerListRequestGenerator : RequestGenerator
	{
		public override void SendRequest()
		{
			SendRequestWithResult();
		}

		public override SendResult SendRequestWithResult()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug("Sending list request.");

			return NetworkPeer.TrySendMessage(OperationType.Request, new GameServerListRequestPayload(), DeliveryMethod.ReliableOrdered, true);
		}
	}
}
