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
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Base class for any message broadcasting component.
	/// </summary>
	[Injectee]
	public abstract class NetworkMessageBroadcaster : SerializedMonoBehaviour
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

		/// <summary>
		/// Indicates the set broadcasting mode for the broadcaster.
		/// </summary>
		[OdinSerialize]
		public NetworkMessageBroadcaster.Mode BroadcastingMode { get; private set; }

		[Inject]
		private IGameObjectComponentAttachmentFactory ComponentFactory { get; }

		protected IMessageBroadcastingService MessageBroadcaster { get; private set; } //can't be readonly prop because MonoBehaviour

		protected virtual void Start()
		{
			if(ComponentFactory == null)
				throw new InvalidOperationException($"The {nameof(ComponentFactory)} is null.");

			switch (BroadcastingMode)
			{
				case Mode.None:
					throw new NotImplementedException($"None? Why? This isn't implemented just yet. Sorry from Glader.");
				case Mode.All:
					MessageBroadcaster = ComponentFactory.AddTo<AllPeerMessageBroadcastingStrategy>(this.gameObject);
					break;
				case Mode.AllExcludingOwner:
					MessageBroadcaster = ComponentFactory.CreateBuilder()
						.With(Service<INetworkOwnable>.As(GetComponent(typeof(INetworkOwnable)) as INetworkOwnable))
						.AddTo<IgnoreOwnerPeerMessageBroadcastingStrategy>(this.gameObject);
					break;
				default:
					throw new NotImplementedException($"Encoundered {nameof(NetworkMessageBroadcaster.Mode)} setting {BroadcastingMode} that was invalid.");
			}
		}
	}
}
