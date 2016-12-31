using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	//Based on my halo prototype's code: https://github.com/HaloUnity/Halo.Unity.Common/blob/master/src/Halo.Unity.Common/Movement/IMovementInput.cs
	/// <summary>
	/// Provides movement input services.
	/// Allows for abstraction of input source (Network/Local/Rewind)
	/// </summary>
	public interface IMovementInput
	{
		/// <summary>
		/// Direction of movement.
		/// </summary>
		Vector3 Direction { get; }
	}
}
