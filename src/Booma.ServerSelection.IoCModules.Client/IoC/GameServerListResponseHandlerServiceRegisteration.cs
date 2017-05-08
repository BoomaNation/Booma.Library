using GladNet.Message;
using GladNet.Message.Handlers;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Booma.Unity.Network;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Scene component that allows for the registeration of a <see cref="ResponseMessageHandlerService{TSessionType}"/> for <see cref="GameServerListWebClient"/> peer types.
	/// </summary>
	public class GameServerListResponseHandlerServiceRegisteration :
		NetworkMessageHandlerServiceRegistration<GameServerListWebClient, IResponseMessage, IResponseMessageHandler<GameServerListWebClient>, ResponseMessageHandlerService<GameServerListWebClient>, IResponseMessageHandlerService<GameServerListWebClient>>
	{
		/// <summary>
		/// As specified by the base class documentation it generates a valid non-null <see cref="ResponseMessageHandlerService{TSessionType}"/> instance.
		/// </summary>
		/// <param name="strat">Strategy the base class decided to use.</param>
		/// <returns>Non-null instance of <see cref="ResponseMessageHandlerService{TSessionType}"/> or throws instead.</returns>
		protected override ResponseMessageHandlerService<GameServerListWebClient> CreateConcreteService(IMessageHandlerStrategy<GameServerListWebClient, IResponseMessage> strat)
		{
			//just create and return the handler as expected with the provided strat
			return new ResponseMessageHandlerService<GameServerListWebClient>(strat);
		}
	}
}
