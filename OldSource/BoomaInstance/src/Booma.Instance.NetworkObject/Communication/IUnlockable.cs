using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.NetworkObject
{
	public interface IUnlockable
	{
		bool isLocked { get; }

		/// <summary>
		/// Unlocks the UnLockable.
		/// (No parameter so that a UnityEvent can fire it.
		/// </summary>
		void Unlock();

		/// <summary>
		/// Attempts to interact with the interactable.
		/// GUID of Entity interacting with it is provided.
		/// </summary>
		/// <param name="entityInteracting">The GUID of the entity trying to interact with this interactable.</param>
		/// <returns>Indicates if the entity was able to interact with the object.</returns>
		void Unlock(NetworkEntityGuid entityInteracting);
	}
}
