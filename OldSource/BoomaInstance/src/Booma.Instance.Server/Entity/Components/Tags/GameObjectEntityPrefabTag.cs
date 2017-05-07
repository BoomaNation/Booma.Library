using Booma.Entity.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Tag component containing the <see cref="GameObjectPrefab"/>
	/// </summary>
	public class GameObjectEntityPrefabTag : MonoBehaviour, ITagProvider<GameObjectPrefab>
	{
		[SerializeField]
		private GameObjectPrefab prefabId;

		/// <summary>
		/// Indicates the prefab ID associated with the tagged entity.
		/// </summary>
		public GameObjectPrefab Tag { get { return prefabId; } }
	}
}
