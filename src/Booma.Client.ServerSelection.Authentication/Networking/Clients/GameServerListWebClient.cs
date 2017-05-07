using Booma.Client.Network.Common;
using Booma.Payloads.ServerSelection;
using GladNet.ASP.Client.RestSharp;
using GladNet.ASP.Client.RestSharp.Middleware.Authentication;
using GladNet.Message;
using GladNet.Payload;
using GladNet.Serializer;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client.ServerSelection
{
	public class GameServerListWebClient : BoomaNetworkWebPeer<GameServerListWebClient>
	{
		[Inject]
		private readonly ISerializerRegistry registry;

		public override void Start()
		{
			base.Start();

			RegisterMessages();
		}
		
		private void RegisterMessages()
		{
			registry.RegisterServerSelectionPayloads();
		}
	}
}
