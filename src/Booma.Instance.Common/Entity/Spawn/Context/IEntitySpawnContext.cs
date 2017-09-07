using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface IEntitySpawnContext : ISpawnContext
	{
		/// <summary>
		/// The network GUID associated with the entity.
		/// </summary>
		NetworkEntityGuid EntityGuid { get; }
	}
}
