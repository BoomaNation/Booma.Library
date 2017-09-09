using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Interface for objects that can produce multipliers.
	/// </summary>
	public interface IMultiplierProvider
	{
		/// <summary>
		/// Float multiplier.
		/// </summary>
		float Multiplier { get; }
	}
}
