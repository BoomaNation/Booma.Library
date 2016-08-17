using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Common
{
	// <summary>
	/// Simplified <see cref="Attribute"/> of the <see cref="GladNetSerializationIncludeAttribute"/> which reduces the
	/// complexity for marking payloads with meta-data.
	/// </summary>
	public class GladLivePayloadAttribute : GladNetSerializationIncludeAttribute
	{
		/// <summary>
		/// Marks a class with the GladLive payload meta-data. Providing a message type
		/// to register it internally with <see cref="GladNet"/>.
		/// </summary>
		/// <param name="messageType">The <see cref="BoomaPayloadMessageType "/> to mark the payload for.</param>
		public GladLivePayloadAttribute(BoomaPayloadMessageType messageType)
			: base((GladNetIncludeIndex)messageType, typeof(PacketPayload), false)
		{

		}
	}
}
