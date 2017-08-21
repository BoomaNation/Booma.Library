using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladBehaviour.Common;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class InitializationEngine : GladMonoBehaviour
	{
		/// <summary>
		/// The list of <see cref="IInitializable"/> listeners.
		/// </summary>
		[SerializeField]
		private List<IInitializable> InitializationList { get; }

		//TODO: How should we allow configuration?
		[SerializeField]
		private bool Initialize;

		private void FixedUpdate()
		{
			//TODO: What about reiniaitlaize?
			if (Initialize)
			{
				foreach (IInitializable i in InitializationList)
					i.Initialize();

				Initialize = false;
			}
		}
	}
}
