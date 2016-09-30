using Booma.Instance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public interface IEntityPrefabProvider
	{
		GameObject GetPrefab(EntityType entityType);
	}
}
