using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Contract for all services that offer event <see cref="PacketPayload"/> broadcasting services.
	/// </summary>
	public interface IMessageBroadcastingService : IEventPayloadSender
	{

	}
}
