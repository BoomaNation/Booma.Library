using GladLive.Common;
using GladNet.PhotonServer.Client;
using GladNet.Serializer.Protobuf;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;

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
		/// Assets that the generic arg <typeparamref name="TInheritingType"/> is the subtype.
		/// </summary>
		private void Awake()
		{
			//What a hack
			if (GetType() != typeof(TInheritingType))
				throw new InvalidOperationException($"Created invalid closed generic {nameof(BoomaNetworkClientPeer<TInheritingType>)} due to {typeof(TInheritingType)} not being the true subtype of this object.");
		}

		/// <summary>
		/// Handler service that will deal with dispatching <see cref="ResponseMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private IResponsePayloadHandlerService<TInheritingType> responseHandler { get; }

		/// <summary>
		/// Handler service that will deal with dispatching <see cref="ResponseMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private IEventPayloadHandlerService<TInheritingType> eventHandler { get; }

		public sealed override void OnReceiveEvent(PacketPayload payload)
		{
			if (payload == null)
				throw new ArgumentNullException(nameof(payload), $"Cannot have a null {nameof(PacketPayload)} in on recieve. This should never occur internally. Indicates major fault. Should never reach this point.");

			eventHandler.TryProcessPayload(payload, null, this as TInheritingType);
		}

		public sealed override void OnReceiveResponse(PacketPayload payload)
		{
			if (payload == null)
				throw new ArgumentNullException(nameof(payload), $"Cannot have a null {nameof(PacketPayload)} in on recieve. This should never occur internally. Indicates major fault. Should never reach this point.");

			responseHandler.TryProcessPayload(payload, null, this as TInheritingType);
		}
	}
}
