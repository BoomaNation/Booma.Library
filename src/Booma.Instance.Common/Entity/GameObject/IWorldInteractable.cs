using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface IWorldInteractable
	{
		/// <summary>
		/// Attempts to interact with an interactable.
		/// </summary>
		/// <param name="entityInteracting">The GUID of the entity trying to interact with this interactable.</param>
		/// <returns>Indicates if the entity was able to interact with the object.</returns>
		bool TryInteract(NetworkEntityGuid entityInteracting);
	}
}
