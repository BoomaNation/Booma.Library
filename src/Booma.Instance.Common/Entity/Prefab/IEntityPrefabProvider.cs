using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Contract for services that produce prefab references.
	/// </summary>
	public interface IEntityPrefabProvider
	{
		/// <summary>
		/// Provides a prefab <see cref="GameObject"/> instance for the provided
		/// <see cref="PrefabType"/> key.
		/// </summary>
		/// <param name="prefabType"></param>
		/// <returns></returns>
		GameObject GetPrefab(PrefabType prefabType);
	}
}
