using Booma.Payloads.Surrogates.Unity;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Instance
{
	/// <summary>
	/// An event that contains information about a player entity that should be spawned.
	/// </summary>
	[GladNetSerializationContract]
	public class PlayerSpawnEventPayload : EntitySpawnEventPayload
	{
		/// <summary>
		/// The player's name.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public string PlayerName { get; private set; }

		/// <summary>
		/// Creates a new payload for the <see cref="BoomaPayloadMessageType.EntitySpawnEvent"/> packet.
		/// Specifically designed to tramsit information requires for players.
		/// </summary>
		/// <param name="entityId">The entity's ID.</param>
		/// <param name="position">Position of the entity.</param>
		/// <param name="rotation">Rotation of the entity.</param>
		/// <param name="playerName">The player's name.</param>
		public PlayerSpawnEventPayload(int entityId, Vector3Surrogate position, QuaternionSurrogate rotation, string playerName) 
			: base(entityId, position, rotation)
		{
			if (String.IsNullOrEmpty(playerName))
				throw new ArgumentException($"Provied playername must not be empty or null", nameof(playerName));

			PlayerName = playerName;
		}

		protected PlayerSpawnEventPayload()
			: base()
		{

		}
	}
}
