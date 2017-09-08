using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GaiaOnline
{
	/// <summary>
	/// Listener interface for objects who may want to be subscribed to
	/// direction changes.
	/// </summary>
	public interface IDirectionChangeListener
	{
		//TODO: Should we have a Vector3 version?
		void OnDirectionChanged(Vector2 direction);
	}
}
