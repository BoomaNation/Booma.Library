using GladLive.Common;
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
using Easyception;
using GladNet.Lidgren.Client.Unity;
using GladNet.Payload;

namespace Booma.Client.Network.Common
{
	/// <summary>
	/// Network client component for Booma clients that abstracts the generic parameters required by
	/// <see cref="UnityClientPeer{TSerializationStrategy, TDeserializationStrategy, TSerializerRegistry}"/> which are
	/// cumbersome to new contribtors.
	/// </summary>
	[InjecteeAttribute] //marks the class for SceneJect injection
	public abstract class BoomaNetworkClientPeer<TInheritingType> : UnityClientPeer<ProtobufnetSerializerStrategy, ProtobufnetDeserializerStrategy, ProtobufnetRegistry>
		where TInheritingType : BoomaNetworkClientPeer<TInheritingType>
	{
		/// <summary>
		/// Handler service that will deal with dispatching <see cref="ResponseMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private IResponseMessageHandlerService<TInheritingType> responseHandler { get; set; } //we have to have a setter, can't use C#6 readonly prop because FasterFlect when using SceneJect will complain that it can't find a setter.

		/// <summary>
		/// Handler service that will deal with dispatching <see cref="EventMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private IEventMessageHandlerService<TInheritingType> eventHandler { get; set; } //we have to have a setter, can't use C#6 readonly prop because FasterFlect when using SceneJect will complain that it can't find a setter.

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
			//TODO: Phase out easyexception
			Throw<ArgumentNullException>.If.IsNull(message)?.Now(nameof(message), $"Cannot have a null {nameof(IEventMessage)} in on recieve. This should never occur internally. Indicates major fault. Should never reach this point.");
			eventHandler.TryProcessMessage(message, null, this as TInheritingType);
		}

		public sealed override void OnReceiveResponse(IResponseMessage message, IMessageParameters parameters)
		{
			//TODO: Phase out easyexception
			Throw<ArgumentNullException>.If.IsNull(message)?.Now(nameof(message), $"Cannot have a null {nameof(IResponseMessage)} in on recieve. This should never occur internally. Indicates major fault. Should never reach this point.");
			responseHandler.TryProcessMessage(message, null, this as TInheritingType);
		}
	}
}
