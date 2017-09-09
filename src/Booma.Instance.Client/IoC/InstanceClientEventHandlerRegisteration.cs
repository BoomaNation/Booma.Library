using GladNet.Message;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;

namespace Booma
{
	public class InstanceClientEventHandlerRegisteration : NetworkMessageHandlerServiceRegistration<InstanceClientPeer, IEventMessage, IEventMessageHandler<InstanceClientPeer>, EventMessageHandlerService<InstanceClientPeer>, IEventMessageHandlerService<InstanceClientPeer>>
	{
		protected override EventMessageHandlerService<InstanceClientPeer> CreateConcreteService(IMessageHandlerStrategy<InstanceClientPeer, IEventMessage> strat)
		{
			return new EventMessageHandlerService<InstanceClientPeer>(strat);
		}
	}
}
