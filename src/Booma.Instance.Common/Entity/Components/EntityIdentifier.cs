using Booma.Instance.Data;
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
	public class EntityIdentifier : MonoBehaviour, IEntityIdentifiable
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		public int EntityId { get; private set; }

		/// <summary>
		/// Indicates the Entity's Type.
		/// </summary>
		public EntityType EntityType { get; private set; }

		/// <summary>
		/// Indicates if the component has been initialized.
		/// </summary>
		private bool isInitialized = false;

		public void Initialize(int id, EntityType type)
		{
			if (isInitialized)
				throw new InvalidOperationException($"Cannot initialize the {nameof(EntityIdentifier)} multiple times.");

			EntityId = id;
			EntityType = type;

			isInitialized = true;
		}
	}
}
