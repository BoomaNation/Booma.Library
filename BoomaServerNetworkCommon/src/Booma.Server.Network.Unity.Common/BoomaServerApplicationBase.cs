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
using Booma.Client.Logging;
using GladNet.Message;

namespace Booma.Server.Network.Unity.Common
{
	/// <summary>
	/// Base Booma server application Type.
	/// Uses <see cref="ProtoBuf"/>net serialization strategies.
	/// </summary>
	public abstract class BoomaServerApplicationBase : UnityServerApplicationBase<ProtobufnetSerializerStrategy, ProtobufnetDeserializerStrategy, ProtobufnetRegistry>
	{
		/// <summary>
		/// Basic Unity3D logging service.
		/// </summary>
		public override ILog Logger { get; } = new UnityLoggingService();
	}
}
