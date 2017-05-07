using Booma.Entity.Identity;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.NetworkObject
{
	/// <summary>
	/// Component for indentifying entities.
	/// </summary>
	[Injectee]
	public class NetworkEntityIdentifierTag : MonoBehaviour, IEntityIdentifiable, IEntityGuidContainer, ITagProvider<NetworkEntityGuid>
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		public int EntityId => NetworkGuid.EntityId;

		/// <summary>
		/// Indicates the Entity's Type.
		/// </summary>
		public EntityType EntityType => NetworkGuid.EntityType;

		/// <summary>
		/// Network GUID.
		/// </summary>
		[Inject]
		public NetworkEntityGuid NetworkGuid { get; private set; }

		public NetworkEntityGuid Tag => NetworkGuid;
	}
}
