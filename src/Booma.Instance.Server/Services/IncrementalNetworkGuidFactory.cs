using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using System.Threading;

namespace Booma
{
	/// <summary>
	/// Simple incremental network GUID factory.
	/// </summary>
	public class IncrementalNetworkGuidFactory : INetworkGuidFactory
	{
		/// <summary>
		/// Incremental count of the ID.
		/// </summary>
		private int currentId = 1;

		public NetworkEntityGuid Create(EntityType entityType)
		{
			return NetworkEntityGuidBuilder.New().WithType(entityType)
				.WithId(Interlocked.Increment(ref currentId))
				.Build();
		}
	}
}
