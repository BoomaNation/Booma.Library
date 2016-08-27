using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface IEntityCollection<TEntityType> : IDictionary<int, TEntityType>
	{
		/// <summary>
		/// Indicates the type of entity this collection manages.
		/// </summary>
		EntityType CollectionType { get; }
	}
}
