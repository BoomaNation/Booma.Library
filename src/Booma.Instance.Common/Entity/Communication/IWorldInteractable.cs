using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IWorldInteractable
	{
		/// <summary>
		/// Attempts to interact with the interactable.
		/// GUID of Entity interacting with it is provided.
		/// </summary>
		/// <param name="entityInteracting">The GUID of the entity trying to interact with this interactable.</param>
		/// <returns>Indicates if the entity was able to interact with the object.</returns>
		bool TryInteract(NetworkEntityGuid entityInteracting);

		/// <summary>
		/// Attempts to interact with the interactable.
		/// </summary>
		/// <returns></returns>
		bool TryInteract();
	}
}
