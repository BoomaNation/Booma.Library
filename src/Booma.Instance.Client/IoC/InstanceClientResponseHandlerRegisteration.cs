using Booma.Client.Network.Common;
using GladNet.Message;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Client
{
	public class InstanceClientResponseHandlerRegisteration : NetworkMessageHandlerServiceRegistration<InstanceClientPeer, IResponseMessage, IResponseMessageHandler<InstanceClientPeer>, ResponseMessageHandlerService<InstanceClientPeer>, IResponseMessageHandlerService<InstanceClientPeer>>
	{
		protected override ResponseMessageHandlerService<InstanceClientPeer> CreateConcreteService(IMessageHandlerStrategy<InstanceClientPeer, IResponseMessage> strat)
		{
			return new ResponseMessageHandlerService<InstanceClientPeer>(strat);
		}
	}
}
