using Booma.Client.Network.Common;
using GladLive.Common;
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
	/// Scene component that allows for the registeration of a <see cref="EventPayloadHandlerService{TSessionType}"/> for <see cref="LoginNetworkClient"/> peer types.
	/// </summary>
	public class LoginNetworkEventHandlerServiceRegisteration :
		NetworkMessageHandlerServiceRegistration<LoginNetworkClient, IEventPayloadHandler<LoginNetworkClient>, EventPayloadHandlerService<LoginNetworkClient>, IEventPayloadHandlerService<LoginNetworkClient>>
	{
		/// <summary>
		/// As specified by the base class documentation it generates a valid non-null <see cref="RequestPayloadHandlerService{TSessionType}"/> instance.
		/// </summary>
		/// <param name="strat">Strategy the base class decided to use.</param>
		/// <returns>Non-null instance of <see cref="RequestPayloadHandlerService{TSessionType}"/> or throws instead.</returns>
		protected override EventPayloadHandlerService<LoginNetworkClient> CreateConcreteService(IPayloadHandlerStrategy<LoginNetworkClient> strat)
		{
			//just create and return the handler as expected with the provided strat
			return new EventPayloadHandlerService<LoginNetworkClient>(strat);
		}
	}
}
