using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IEntitySpawnContext : ISpawnContext
	{
		/// <summary>
		/// The network GUID associated with the entity.
		/// </summary>
		NetworkEntityGuid EntityGuid { get; }
	}
}
