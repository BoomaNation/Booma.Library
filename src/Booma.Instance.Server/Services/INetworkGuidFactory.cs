using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface INetworkGuidFactory
	{
		/// <summary>
		/// Issues a new network GUID.
		/// </summary>
		/// <param name="entityType">Type of the entity.</param>
		/// <returns>A new non-null <see cref="NetworkEntityGuid"/> with the provided entity type.</returns>
		NetworkEntityGuid Create(EntityType entityType);
	}
}
