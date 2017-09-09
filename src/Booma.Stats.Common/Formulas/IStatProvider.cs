using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	//It's like a value that comes with a unit. The unit being the TStatType and the StatType.
	/// <summary>
	/// Interface for objects that can produce values that have units.
	/// These units are indicated by both the generic parameter and the <see cref="StatType"/> property.
	/// </summary>
	public interface IStatProvider<TStatType>
		where TStatType : struct, IConvertible
	{
			
		/// <summary>
		/// Indicates the unit of the <see cref="Value"/> associated with this stat provider.
		/// </summary>
		TStatType StatType { get; }

		/// <summary>
		/// Integer value associated with the stat provider.
		/// </summary>
		int Value { get; }
	}
}
