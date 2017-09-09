using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Contract for types that contain a <see cref="NetworkEntityGuid"/>
	/// </summary>
	public interface IEntityGuidContainer
	{
		/// <summary>
		/// The Network GUID contained inside the container.
		/// </summary>
		NetworkEntityGuid NetworkGuid { get; }
	}
}
