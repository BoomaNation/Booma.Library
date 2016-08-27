using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public class PlayerEntityCollection : Dictionary<int, GameObject>, IEntityCollection<GameObject>
	{
		/// <summary>
		/// Indicates the type of entity this collection manages.
		/// </summary>
		public EntityType CollectionType { get; } = EntityType.Player;
	}
}
