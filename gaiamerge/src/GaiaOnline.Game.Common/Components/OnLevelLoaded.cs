using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GaiaOnline
{
	public sealed class OnLevelLoaded : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent OnLevelLoadedEvent;

		private bool isFired { get; set; } = false;

		public void Awake() => FireEvent();

		public void OnLevelWasLoaded(int id) => FireEvent();

		public void FireEvent()
		{
			if(isFired)
				return;

			OnLevelLoadedEvent?.Invoke();
			isFired = true;
		}
	}
}
