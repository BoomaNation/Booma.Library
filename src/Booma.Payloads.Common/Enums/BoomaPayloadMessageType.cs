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
		/// <summary>
		/// An event that contains information about an entity that should be spawned.
		/// </summary>
		EntitySpawnEvent = 4,

		/// <summary>
		/// An event that contains information about an entity that should be despawned.
		/// </summary>
		EntityDespawnEvent = 5,

		/// <summary>
		/// Request that tries to claim a session that a client wants, and the the server may or may not grant.
		/// </summary>
		ClaimSessionRequest = 6,

		/// <summary>
		/// Response sent by server as a response to <see cref="ClaimSessionRequest"/>.
		/// </summary>
		ClaimSessionResponse = 7,

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
