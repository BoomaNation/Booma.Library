using Booma.Client.Network.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Booma.Payloads.ServerSelection;
using UnityEngine;
using SceneJect.Common;
using UnityEngine.Events;

namespace Booma.Client.ServerSelection.Authentication
{
	[Injectee]
	public class GameServerListResponseHandler : ResponsePayloadHandlerComponent<GameServerListWebClient, GameServerListResponsePayload>
	{
		[Serializable]
		private sealed class GameServerListResponseEvent : UnityEvent<GameServerListResponseCode> { }

		/// <summary>
		/// Factory that creates game server detail views.
		/// </summary>
		[Inject]
		private readonly IGameServerDetailsGameObjectFactory detailsFactory;

		/// <summary>
		/// Unity event invoked when the ship list load fails.
		/// </summary>
		[SerializeField]
		[Tooltip("Listeners invokved when the shiplist fails.")]
		private GameServerListResponseEvent OnShipListResponseError;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, GameServerListResponsePayload payload, IMessageParameters parameters, GameServerListWebClient peer)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Handling response game server list {payload.GameServerDetails.Count()}");

			if (payload.ResponseCode != GameServerListResponseCode.Success)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to load ship list.");

				OnShipListResponseError?.Invoke(payload.ResponseCode);
				return;
			}

			foreach(SimpleGameServerDetailsModel details in payload.GameServerDetails)
				detailsFactory.Create(details.Name, details.Region);
		}
	}
}
