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
			register.Register<ProtobufnetSerializerStrategy>(ComputeFlags(), typeof(ISerializerStrategy));
			register.Register<ProtobufnetDeserializerStrategy>(ComputeFlags(), typeof(IDeserializerStrategy));
			register.Register<ProtobufnetRegistry>(ComputeFlags(), typeof(ISerializerRegistry));
		}
	}
}
