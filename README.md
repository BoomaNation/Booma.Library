# Booma.Library

Booma.Library is a repository containing all the dll libraries for the Booma project. This is a repo that was created by merging approximately 10 other [Booma Nation](www.github.com/BoomaNation) repos. It contains anything from general/common libraries down to specific client/server libraries like the Instance client/server project.

## Setup

To use this project you'll first need a couple of things:

* Visual Studio 2017 (earlier versions will likely be incompatible)
* Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## Current Dependency Graph

To help understand the relationships between the various csprojs it may be helpful for some to view and skim this dependency graph generated from the project itself.

It is not directly embed here because it is VERY LARGE but is only about 700kb. You can view it [here](http://i.imgur.com/gFSK3Uc.png).

## Information

Booma is a big project. It references some internal tools such as: Sceneject, GladBehaviour, GladNet and sometimes GladLive. These libraries/tools/frameworks may be confusing because they were written quickly to target and solve critical problems without regard for general usability sometimes. This section will aim to clarify and explain some of those libraries' and tools' use within Booma and other parts of Booma that may be complex but don't depend on external libraries.

### Packets/Payloads

The Booma project uses a traditional networking message scheme that sends packets with both a header and a payload. These headers containing message type information and an operation code in the header. The three main message types that a user can send are:

1. **Response message** is a message that can only be sent from a server to a listening peer.
2. **Event message** is a message that can only be sent from a server to a listening peer.
3. **Request message** is a message that can only be sent from a client to a listening server.

Events are typically used when the message was unsolicitied by a client. Responses are typically used to respond to requests but the GladNet2 API does not offer a system for continuations or async handling. Therefore the context for a request and response will not be the same. Though this may change in the future. There is **NO** enforcement by GladNet that requires you to follow these semantics. It is just suggested. You also **DO NOT** need to manually create and send these messages. They are handled under the hood.

To understand how the payloads of these messages (packets) are defined you do not need to dig into GladNet2, which is the networking API used, but instead look only at the [DTOs (Data Transfer Object)](https://martinfowler.com/eaaCatalog/dataTransferObject.html) that make up the payloads.

To get an understanding of what payloads there are you should refer to their [enumeration of operation codes](https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.Payloads.Common/Enums/BoomaPayloadMessageType.cs). For this example we will analyze [GetGameServerListResponse](https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.Payloads.ServerSelection/Payloads/GameServerListResponsePayload.cs). It may be confusing as to why the field is initialized to another enumeration value but that can be ignored for now.

**Breakdown:**

The class attribute [GladNetSerializationContract] is required on the class to indicate to GladNet2 that this is a DTO that can and should be made serializable.

The class attribute [BoomaPayload(BoomaPayloadMessageType.GetGameServerListResponse)] is required on the class to tell GladNet2 that it should use the BoomaPayloadMessageType.GetGameServerListResponse value, which can be found in the [enumeration of operation codes](https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.Payloads.Common/Enums/BoomaPayloadMessageType.cs), in the header as the message's opcode. This allows you to send DTOs without even specifying the operation code manually.

The base type PacketPayload must be inherited from to enable network serialization for the type. The reason for this is GladNet2 sends the DTOs polymorphically so all of them must be a PacketPayload.

The field or property attribute [GladNetMember(...)] is required to markup a classes' fields or properties to indicate to the serializer that the contents of this field or property should be serialized. If you want to send a particular piece of data you must mark it with this attribute. Otherwise no data, or null, will be sent in its place. See the example below which marks a ResponseCode property of type GameServerListResponseCode enabled for serialization. The GladNetDataIndex parameter for the attribute should be unique for the current fields/properties in that level of the Type heirarchy. It controls the order of serialization and deserialization used.

```
/// <summary>
/// Indicates the status of the response.
/// </summary>
[GladNetMember(GladNetDataIndex.Index1)]
public GameServerListResponseCode ResponseCode { get; private set; }
```

All serializable payloads should contain a default parameterless ctor like below. It can be either private or public.

```
/// <summary>
/// Creates a new payload for the <see cref="BoomaPayloadMessageType.GetGameServerListResponse"/> packet.
/// </summary>
public GameServerListResponsePayload()
{

}
```

### Network Peers and Message Sending

Network Peers are simple, but complex under the hood, and they offer an interface to the network to send messages. An example of a complete network peer can be seen in the [AuthenticationWebClient](https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.ServerSelection.Client/Clients/AuthenticationWebClient.cs) which registers the accepted payloads, registers an HTTP web middleware and not much else. The simplicity of the network peer/client was by design in GladNet2. The ability to create peers, point them to service endpoints and send messages through them is made incredibly easy with this system. Most importantly these peers all implement the **INetPeer** interface which allows you to send messages through them. Sending network messages through peers is one of the two critial points of networking in the Booma project.

Sending network messages does not require creating a ResponseMessage, EventMessage or RequestMessage. Sending messages involves simply sending a PacketPayload through an interface such as [INetPeer's Extension Methods](https://github.com/HelloKitty/GladNet2/blob/master/src/GladNet.Engine.Common/General/Extensions/Peer/INetPeerExtensions.cs).

For example, to understand how the sending of a payload through a network peer works consult the [LoginRequestGenerator](https://github.com/BoomaNation/Booma.Library/blob/master/src/Booma.ServerSelection.Client/RequestGenerators/GameServerListRequestGenerator.cs) which shows:

```
NetworkPeer.TrySendMessage(OperationType.Request, new GameServerListRequestPayload(), DeliveryMethod.ReliableOrdered, true);
```

This sends a RequestMessage with the operation code specific by the GameServerListRequestPayload. See the Payload section to understand how operation codes are tied to specific payload Types. It sends it with an [ordered reliable](https://github.com/HelloKitty/GladNet2/blob/master/src/GladNet.Common/Network/Parameters/DeliveryMethod.cs) method. Consult the linked GladNet2 documentation to explain what each means. Lastly it's sent encrypted. This is unimplemented as of this writing, that is not all GladNet2 implementations support encryption toggling, but it works partly for the HTTP implementation and unsued implementations such as PhotonServer.

This though was through the least specific interface INetPeer and through extension methods for this interface. More specific interfaces offer a cleaner way to send messages like so:

```
Peer.SendRequest(new GameServerListRequestPayload(), DeliveryMethod.ReliableOrdered, true);
```

## Deprecated Libraries

This section describes projects or libraries which are now deprecated, removed from the main project and/or have been renamed.

* Booma.Common.ServerSelection -> **Booma.ServerSelection.Common**

* Booma.Client.Common.IoCModules -> **Booma.Unity.IoCModules.Common**

* Booma.Client.Logging -> **Booma.Unity.Logging.Common**

* Booma.Client.Network.Common -> **Booma.Unity.Network.Common**

* Booma.Client.Network.Common -> **Booma.Unity.Network.Client** + **Booma.Unity.Network.IoCModules.Client**

* Booma.Server.Network.Unity.Common -> **Booma.Unity.Network.Server** + **Booma.Unity.Network.IoCModules.Server**

* Booma.Client.ServerSelection.Authentication -> **Booma.ServerSelection.Client** + **Booma.ServerSelection.IoCModules.Client**

* Booma.Instance.Client.Handlers -> **Booma.Instance.Handlers.Client**

* Booma.Instance.Server.Handlers -> **Booma.Instance.Handlers.Server**


## Licensing

This project is protected under the GPL licensing agreement. It will be actively enforced.
