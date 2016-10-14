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
		GetGameServerListRequest = GladNetIncludeIndex.Index19,
		
		/// <summary>
		/// A response containing the list of available gameservers/ships
		/// </summary>
		GetGameServerListResponse = GladNetIncludeIndex.Index20,

		/// <summary>
		/// An event that contains information about an entity that should be spawned.
		/// </summary>
		EntitySpawnEvent = GladNetIncludeIndex.Index21,

		/// <summary>
		/// An event that contains information about an entity that should be despawned.
		/// </summary>
		EntityDespawnEvent = GladNetIncludeIndex.Index22,

		/// <summary>
		/// Request sent by players who want to spawn in an instance.
		/// </summary>
		PlayerSpawnRequest = GladNetIncludeIndex.Index23,

		/// <summary>
		/// Response sent by server as a response to <see cref="BoomaPayloadMessageType.PlayerSpawnRequest"/>.
		/// </summary>
		PlayerSpawnResponse = GladNetIncludeIndex.Index24,

		/// <summary>
		/// Request sent by the client's to request movement.
		/// </summary>
		PlayerMoveRequest = GladNetIncludeIndex.Index25,

		/// <summary>
		/// Event sent by the server about position updates of an entity.
		/// </summary>
		EntityPositionUpdateEvent = GladNetIncludeIndex.Index26,
	}
}
