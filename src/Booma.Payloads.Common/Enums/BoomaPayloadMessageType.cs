using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Common
{
	/// <summary>
	/// Enumeration of valid Booma message types.
	/// </summary>
	public enum BoomaPayloadMessageType : int
	{
		//We have to make sure we don't overlap with GladLive
		/// <summary>
		/// A request to get the list of available gameservers/ships
		/// </summary>
		GetGameServerListRequest = 2,
		
		/// <summary>
		/// A response containing the list of available gameservers/ships
		/// </summary>
		GetGameServerListResponse = 3,

		/// <summary>
		/// An event that contains information about an entity that should be spawned.
		/// </summary>
		EntitySpawnEvent = 4,

		/// <summary>
		/// An event that contains information about an entity that should be despawned.
		/// </summary>
		EntityDespawnEvent = 5,

		/// <summary>
		/// Request sent by players who want to spawn in an instance.
		/// </summary>
		PlayerSpawnRequest = 6,

		/// <summary>
		/// Response sent by server as a response to <see cref="BoomaPayloadMessageType.PlayerSpawnRequest"/>.
		/// </summary>
		PlayerSpawnResponse = 7,

		/// <summary>
		/// Request sent by the client's to request movement.
		/// </summary>
		PlayerMoveRequest = 8,

		/// <summary>
		/// Event sent by the server about position updates of an entity.
		/// </summary>
		EntityPositionUpdateEvent = 9,

		/// <summary>
		/// Event sent by server when an entity's state has changed.
		/// </summary>
		EntityStateChangedEvent = 10,

		/// <summary>
		/// Request sent by client when the client attempts to interact with an entity.
		/// </summary>
		EntityInteractionRequest = 11
	}
}
