using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	public interface IConnectionToGuidLookupService
	{
		/// <summary>
		/// Indicates if the lookup service contains the provided guid.
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		bool Contains(NetworkEntityGuid guid);

		/// <summary>
		/// Indicates if the lookup service contains the provided connection id.
		/// </summary>
		/// <param name="connectionId"></param>
		/// <returns></returns>
		bool Contains(int connectionId);

		/// <summary>
		/// Produces a <see cref="NetworkEntityGuid"/> for the provided connection ID.
		/// </summary>
		/// <param name="connectionId"></param>
		/// <returns></returns>
		NetworkEntityGuid Lookup(int connectionId);

		/// <summary>
		/// Produces the connection ID associated with the <see cref="NetworkEntityGuid"/>.
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		int Lookup(NetworkEntityGuid guid);
	}
}
