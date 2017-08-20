using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class InitializationEngine
	{
		/// <summary>
		/// The list of <see cref="IInitializable"/> listeners.
		/// </summary>
		[SerializeField]
		private List<IInitializable> InitializationList { get; }

		//TODO: How should we allow configuration?
		[SerializeField]
		private bool initializeOnEnable;

		private void OnEnable()
		{
			//TODO: What about reiniaitlaize?
			if(initializeOnEnable)
				foreach(IInitializable i in InitializationList)
					i.Initialize();
		}
	}
}
