using GladNet.Message;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Booma.Unity.Network;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Scene component that allows for the registeration of a <see cref="ResponseMessageHandlerService{TSessionType}"/> for <see cref="AuthenticationWebClient"/> peer types.
	/// </summary>
	public class LoginNetworkResponseHandlerServiceRegisteration :
		NetworkMessageHandlerServiceRegistration<AuthenticationWebClient, IResponseMessage, IResponseMessageHandler<AuthenticationWebClient>, ResponseMessageHandlerService<AuthenticationWebClient>, IResponseMessageHandlerService<AuthenticationWebClient>>
	{
		/// <summary>
		/// As specified by the base class documentation it generates a valid non-null <see cref="ResponseMessageHandlerService{TSessionType}"/> instance.
		/// </summary>
		/// <param name="strat">Strategy the base class decided to use.</param>
		/// <returns>Non-null instance of <see cref="ResponseMessageHandlerService{TSessionType}"/> or throws instead.</returns>
		protected override ResponseMessageHandlerService<AuthenticationWebClient> CreateConcreteService(IMessageHandlerStrategy<AuthenticationWebClient, IResponseMessage> strat)
		{
			//just create and return the handler as expected with the provided strat
			return new ResponseMessageHandlerService<AuthenticationWebClient>(strat);
		}
	}
}
