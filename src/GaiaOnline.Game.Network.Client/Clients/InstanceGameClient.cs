using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Instance.Client;
using Booma.Payloads.Instance;
using GladNet.Common;
using GladNet.Lidgren.Client.Unity;
using SceneJect.Common;
using UnityEngine;
using Unitysync.Async;

namespace GaiaOnline
{
	[Injectee]
	public sealed class InstanceGameClient : InstanceClientPeer
	{
		[Inject]
		private IGameServerMediatorService MediatorService { get; }

		/// <summary>
		/// Overriden connection info that will be set after we request entry
		/// via the session endpoint.
		/// </summary>
		private ConnectionInfo connectionEndpointDetails;

		/// <inheritdoc />
		protected override ConnectionInfo ConnectionEndpointDetails => connectionEndpointDetails;

		//TODO: Eventually GladNet will have async support. Right now we don't so we need to cache the session guid
		private Guid SessionGuid { get; set; }

		//TODO: This is kinda of a hack, to override the gameserver location.
		public void Start()
		{
			//TODO: We should hide this behind a service/interface because we may not load from Prefs in the future
			string token = PlayerPrefs.GetString(PlayerPreferences.UserToken.ToString());

			if(token == null)
				throw new InvalidOperationException("User token was not present in player prefs.");

			Debug.Log("Sending reques to start session on gameserver.");

			MediatorService.TryEnterGameServer(token)
				.UnityAsyncContinueWith(this, OnEnterGameServerResponse);
		}

		private void OnEnterGameServerResponse(ServerEntryResponse result)
		{
			Debug.Log("Recieved response for session start request on gameserver..");

			if(!result.isSuccessful)
				throw new InvalidOperationException($"Failed to connect with Peer: {GetType().Name} with Failurecode: {result.ResultCode}");

			//Otherwise we should connect now to the provided server after overriding the endpoint
			connectionEndpointDetails = new ConnectionInfo(base.ConnectionEndpointDetails.ApplicationIdentifier, result.Endpoint.EndpointPort, result.Endpoint.EndpointAddress);

			Debug.Log($"Initialized GameServer connection details for Ip: {ConnectionEndpointDetails.ServerIp} Port: {connectionEndpointDetails.RemotePort}");

			SessionGuid = result.SessionClaimGuid;

			ConnectToServer();
		}

		/// <inheritdoc />
		public override void OnStatusChanged(NetStatus status)
		{
			//We only need to override handling for Connected. Send the session claim request
			if(status == NetStatus.Connected)
			{
				SendRequest(new ClaimSessionRequestPayload(SessionGuid), DeliveryMethod.ReliableOrdered);
				return;
			}
				

			base.OnStatusChanged(status);
		}
	}
}
