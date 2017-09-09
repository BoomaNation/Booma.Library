using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Generic dictionary with <see cref="NetworkEntityGuid"/> key types.
	/// </summary>
	/// <typeparam name="TKey">Key type.</typeparam>
	/// <typeparam name="TValue">Value type.</typeparam>
	public class EntityGuidDictionary<TKey, TValue> : Dictionary<TKey, TValue>
		where TKey : NetworkEntityGuid
	{
		public EntityGuidDictionary()
			: base(NetworkGuidEqualityComparer<TKey>.Instance)
		{

		}

		public EntityGuidDictionary(int capacity)
			: base(capacity, NetworkGuidEqualityComparer<TKey>.Instance)
		{

		}

		public EntityGuidDictionary(IDictionary<TKey, TValue> dictionary)
			: base(dictionary, NetworkGuidEqualityComparer<TKey>.Instance)
		{

		}
	}
}
