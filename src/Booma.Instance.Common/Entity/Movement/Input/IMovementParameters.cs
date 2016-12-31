using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Provides movement parameters.
	/// Allows for abstraction of input source (Network/Local/Rewind)
	/// </summary>
	public interface IMovementParameters
	{
		/// <summary>
		/// Desired movement speed.
		/// </summary>
		float MovementSpeed { get; }
	}
}
