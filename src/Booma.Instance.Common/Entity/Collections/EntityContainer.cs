using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public class EntityContainer
	{
		/// <summary>
		/// Network GUID associated with the entity,
		/// </summary>
		public NetworkEntityGuid NetworkGuid { get; }

		/// <summary>
		/// <see cref="GameObject"/> that represents the entity in the world.
		/// </summary>
		public GameObject WorldObject { get; }

		public EntityContainer(NetworkEntityGuid guid, GameObject go)
		{
			//TODO: Null checks

			NetworkGuid = guid;
			WorldObject = go;
		}
	}
}
