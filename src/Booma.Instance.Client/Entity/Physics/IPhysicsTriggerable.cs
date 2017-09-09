using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	public interface IPhysicsTriggerable
	{
		/// <summary>
		/// Triggered when a collider comes into a trigger.
		/// </summary>
		/// <param name="other">The collider that entered the trigger.</param>
		void OnTriggerEnter(Collider other);

		/// <summary>
		/// Triggered when a collider leaves the trigger.
		/// </summary>
		/// <param name="other">The collider that left the trigger.</param>
		void OnTriggerExit(Collider other);
	}
}
