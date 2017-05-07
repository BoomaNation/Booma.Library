using GladBehaviour.Common;
using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GladNet.Common;
using GladNet.Message;
using GladNet.Payload;
using SceneJect.Common;
using Booma.Instance.Common;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Base class for any message broadcasting component.
	/// </summary>
	[Injectee]
	public abstract class NetworkMessageBroadcaster : GladMonoBehaviour
	{
		/// <summary>
		/// Enumeration of all broadcasting modes.
		/// </summary>
		[Serializable]
		public enum Mode
		{
			/// <summary>
			/// Indicates that no broadcasting should take place.
			/// </summary>
			None = 0,

			/// <summary>
			/// Indicates that broadcasting should be to all peers.
			/// </summary>
			All = 1,

			/// <summary>
			/// Indicates that broadcasting should happen to all but owner.
			/// </summary>
			AllExcludingOwner = 2
		}

		[SerializeField]
		private NetworkMessageBroadcaster.Mode broadcastingMode;

		/// <summary>
		/// Indicates the set broadcasting mode for the broadcaster.
		/// </summary>
		public NetworkMessageBroadcaster.Mode BroadcastingMode { get { return broadcastingMode; } }

		[Inject]
		private readonly IGameObjectComponentAttachmentFactory componentFactory;

		protected IMessageBroadcastingService messageBroadcaster { get; private set; } //can't be readonly prop because MonoBehaviour

		protected virtual void Start()
		{
			switch (broadcastingMode)
			{
				case Mode.None:
					throw new NotImplementedException($"None? Why? This isn't implemented just yet. Sorry from Glader.");
				case Mode.All:
					messageBroadcaster = componentFactory.AddTo<AllPeerMessageBroadcastingStrategy>(this.gameObject);
					break;
				case Mode.AllExcludingOwner:
					messageBroadcaster = componentFactory.CreateBuilder()
						.With(Service<INetworkOwnable>.As(GetComponent(typeof(INetworkOwnable)) as INetworkOwnable))
						.AddTo<IgnoreOwnerPeerMessageBroadcastingStrategy>(this.gameObject);
					break;
				default:
					throw new NotImplementedException($"Encoundered {nameof(NetworkMessageBroadcaster.Mode)} setting {broadcastingMode} that was invalid.");
			}
		}
	}
}
