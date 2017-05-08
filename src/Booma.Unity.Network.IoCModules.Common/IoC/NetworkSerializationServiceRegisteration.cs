using GladNet.Serializer;
using GladNet.Serializer.Protobuf;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Unity.Network
{
	public class NetworkSerializationServiceRegisteration : NonBehaviourDependency
	{
		public override void Register(IServiceRegister register)
		{
			//Register the protobufnet serializers
			register.Register<ProtobufnetSerializerStrategy>(this.getFlags(), typeof(ISerializerStrategy));
			register.Register<ProtobufnetDeserializerStrategy>(this.getFlags(), typeof(IDeserializerStrategy));
			register.Register<ProtobufnetRegistry>(this.getFlags(), typeof(ISerializerRegistry));
		}
	}
}
