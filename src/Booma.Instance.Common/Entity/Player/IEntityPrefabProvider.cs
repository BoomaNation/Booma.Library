using Booma.Instance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public interface IEntityPrefabProvider
	{
		GameObject GetPrefab(EntityType entityType);
	}
}
