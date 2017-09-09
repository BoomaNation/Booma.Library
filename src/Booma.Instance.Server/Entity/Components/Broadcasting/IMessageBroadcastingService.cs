using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Payload;

namespace Booma
{
	/// <summary>
	/// Contract for all services that offer event <see cref="PacketPayload"/> broadcasting services.
	/// </summary>
	public interface IMessageBroadcastingService : IEventPayloadSender
	{

	}
}
