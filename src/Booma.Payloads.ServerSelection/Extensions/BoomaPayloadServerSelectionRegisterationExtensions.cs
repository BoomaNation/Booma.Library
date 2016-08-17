using Booma.Payloads.ServerSelection;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;


public static class BoomaPayloadServerSelectionRegisterationExtensions
{
	/// <summary>
	/// Register the server selection list payloads.
	/// </summary>
	/// <typeparam name="TSerializerRegistry"></typeparam>
	/// <param name="registry"></param>
	/// <returns></returns>
	public static TSerializerRegistry RegisterServerSelectionPayloads<TSerializerRegistry>(this TSerializerRegistry registry)
		where TSerializerRegistry : ISerializerRegistry
	{
		//Register the gameserver list payloads.
		registry.Register(typeof(GameServerListRequestPayload));
		registry.Register(typeof(GameServerListResponsePayload));

		//return for fluent chaining.
		return registry;
	}
}

