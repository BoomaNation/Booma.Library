using GladNet.Lidgren.Server.Unity;
using GladNet.Serializer.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using GladNet.Engine.Common;
using GladNet.Engine.Server;
using GladNet.Serializer;
using GladNet.Message;
using SceneJect.Common;

namespace Booma
{
	/// <summary>
	/// Base Booma server application Type.
	/// Uses <see cref="ProtoBuf"/>net serialization strategies.
	/// </summary>
	[Injectee]
	public abstract class BoomaServerApplicationBase : UnityServerApplicationBase<ProtobufnetSerializerStrategy, ProtobufnetDeserializerStrategy, ProtobufnetRegistry>
	{
		/// <summary>
		/// Sceneject injectable logging dependency.
		/// </summary>
		[Inject]
		private readonly ILog _Logger;

		/// <inheritdoc />
		public override ILog Logger => _Logger;
	}
}
