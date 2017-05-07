using Booma.Entity.Prefab;
using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Client
{
	/// <summary>
	/// GameObject prefab spawn context.
	/// </summary>
	public interface IGameObjectPrefabSpawnContext : ISpawnContext
	{
		/// <summary>
		/// Indicates the prefab ID of the spawn context.
		/// </summary>
		GameObjectPrefab PrefabId { get; }
	}
}
