using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IConnectionToGuidRegistry
	{
		/// <summary>
		/// Registers a new <see cref="NetworkEntityGuid"/> object under the provided
		/// <see cref="connectionId"/> key.
		/// </summary>
		/// <param name="connectionId">The connection id.</param>
		/// <param name="guid">The network guid.</param>
		void Register(int connectionId, NetworkEntityGuid guid);
	}
}
