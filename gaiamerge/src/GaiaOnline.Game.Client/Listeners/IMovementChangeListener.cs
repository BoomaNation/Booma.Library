using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaiaOnline
{
	/// <summary>
	/// Listener interface that listens for movement.
	/// </summary>
	public interface IMovementChangeListener
	{
		/// <summary>
		/// Indicates if something is moving.
		/// The listener will directly set this property when the movement state changes.
		/// </summary>
		bool isMoving { get; set; }
	}
}
