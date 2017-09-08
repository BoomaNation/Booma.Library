using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaiaOnline
{
	/// <summary>
	/// Contract for components that are initializable.
	/// </summary>
	public interface IInitializable
	{
		/// <summary>
		/// Called for the first time initialization is requested.
		/// This could be the same as <see cref="Reinitialize"/> but it may
		/// do things that it might only do the first time.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Called when the object needs to be reinitialized.
		/// (Ex. Avatar was in a pooling system and now has been assigned to handle a new user)
		/// </summary>
		void Reinitialize();
	}
}
