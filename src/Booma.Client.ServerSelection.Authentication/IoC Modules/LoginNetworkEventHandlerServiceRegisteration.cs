using Booma.Client.Network.Common;
using GladLive.Common;
using GladNet.Message;
using GladNet.Message.Handlers;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Scene component that allows for the registeration of a <see cref="EventMessageHandlerService{TSessionType}"/> for <see cref="LoginNetworkClient"/> peer types.
	/// </summary>
	public class LoginNetworkEventHandlerServiceRegisteration :
		NetworkMessageHandlerServiceRegistration<LoginNetworkClient, IEventMessage, IEventMessageHandler<LoginNetworkClient>, EventMessageHandlerService<LoginNetworkClient>, IEventMessageHandlerService<LoginNetworkClient>>
	{
		/// <summary>
		/// As specified by the base class documentation it generates a valid non-null <see cref="EventMessageHandlerService{TSessionType}"/> instance.
		/// </summary>
		/// <param name="strat">Strategy the base class decided to use.</param>
		/// <returns>Non-null instance of <see cref="EventMessageHandlerService{TSessionType}"/> or throws instead.</returns>
		protected override EventMessageHandlerService<LoginNetworkClient> CreateConcreteService(IMessageHandlerStrategy<LoginNetworkClient, IEventMessage> strat)
		{
			//just create and return the handler as expected with the provided strat
			return new EventMessageHandlerService<LoginNetworkClient>(strat);
		}
	}
}
