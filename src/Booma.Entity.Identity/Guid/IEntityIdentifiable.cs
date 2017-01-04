using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Identity
{
	/// <summary>
	/// Contract for types that can indentify a Entity.
	/// </summary>
	public interface IEntityIdentifiable
	{
		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		int EntityId { get; }
	}
}
