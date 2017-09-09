using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IEntityStateListener
	{
		/// <summary>
		/// Listener method for state changes.
		/// </summary>
		/// <param name="value">The new state value.</param>
		void OnEntityStateChanged(byte value);
	}
}
