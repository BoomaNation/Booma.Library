using Booma.Instance.Common;
using Booma.Instance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public class ServerPlayerEntityCollection : Dictionary<int, GameObject>, IPlayerEntityCollection
	{
		public EntityType CollectionType { get; } = EntityType.Player;
	}
}
