using GladNet.Serializer.Protobuf;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using UnityEngine;
using GladNet.Message.Handlers;
using GladNet.Message;
using GladNet.Lidgren.Client.Unity;
using GladNet.Payload;

namespace Booma
{
	/// <summary>
	/// Network client component for Booma clients that abstracts the generic parameters required by
	/// <see cref="UnityClientPeer{TSerializationStrategy, TDeserializationStrategy, TSerializerRegistry}"/> which are
	/// cumbersome to new contribtors.
	/// </summary>
	[Injectee] //marks the class for SceneJect injection
	public abstract class BoomaNetworkClientPeer<TInheritingType> : UnityClientPeer<ProtobufnetSerializerStrategy, ProtobufnetDeserializerStrategy, ProtobufnetRegistry>
		where TInheritingType : BoomaNetworkClientPeer<TInheritingType>
	{
		/// <summary>
		/// Handler service that will deal with dispatching <see cref="ResponseMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private readonly IResponseMessageHandlerService<TInheritingType> ResponseMessageHandler; //we have to have a setter, can't use C#6 readonly prop because FasterFlect when using SceneJect will complain that it can't find a setter.

		/// <summary>
		/// Handler service that will deal with dispatching <see cref="EventMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private readonly IEventMessageHandlerService<TInheritingType> EventMessageHandler; //we have to have a setter, can't use C#6 readonly prop because FasterFlect when using SceneJect will complain that it can't find a setter.

		/// <summary>
		/// Assets that the generic arg <typeparamref name="TInheritingType"/> is the subtype.
		/// </summary>
		protected virtual void OnLevelLoaded()
		{
			//What a hack
			if (GetType() != typeof(TInheritingType))
				throw new InvalidOperationException($"Created invalid closed generic {nameof(BoomaNetworkClientPeer<TInheritingType>)} due to {typeof(TInheritingType)} not being the true subtype of this object.");
		}

		public sealed override void OnReceiveEvent(IEventMessage message, IMessageParameters parameters)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			EventMessageHandler.TryProcessMessage(message, null, this as TInheritingType);
		}

		public sealed override void OnReceiveResponse(IResponseMessage message, IMessageParameters parameters)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			ResponseMessageHandler.TryProcessMessage(message, null, this as TInheritingType);
		}
	}
}
