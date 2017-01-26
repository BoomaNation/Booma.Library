using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Booma.Instance.Common
{
	public class EntityContainer
	{
		/// <summary>
		/// Network GUID associated with the entity,
		/// </summary>
		[NotNull]
		public NetworkEntityGuid NetworkGuid { get; }

		/// <summary>
		/// <see cref="GameObject"/> that represents the entity in the world.
		/// </summary>
		[NotNull]
		public GameObject WorldObject { get; }

		public EntityContainer([NotNull] NetworkEntityGuid networkGuid, [NotNull] GameObject worldObject)
		{
			if (networkGuid == null) throw new ArgumentNullException(nameof(networkGuid));
			if (worldObject == null) throw new ArgumentNullException(nameof(worldObject));

			NetworkGuid = networkGuid;
			WorldObject = worldObject;
		}
	}
}
