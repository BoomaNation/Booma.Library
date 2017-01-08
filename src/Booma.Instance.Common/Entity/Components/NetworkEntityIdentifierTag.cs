using Booma.Entity.Identity;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Component for indentifying entities.
	/// </summary>
	[Injectee]
	public class NetworkEntityIdentifierTag : MonoBehaviour, IEntityIdentifiable, IEntityGuidContainer
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		public int EntityId { get { return NetworkGuid.EntityId; } }

		/// <summary>
		/// Indicates the Entity's Type.
		/// </summary>
		public EntityType EntityType { get { return NetworkGuid.EntityType; } }

		/// <summary>
		/// Network GUID.
		/// </summary>
		[Inject]
		public NetworkEntityGuid NetworkGuid { get; private set; }
	}
}
