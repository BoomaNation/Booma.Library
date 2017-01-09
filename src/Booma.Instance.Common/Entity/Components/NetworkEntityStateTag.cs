using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.Common
{
	public class NetworkEntityStateTag : GladMonoBehaviour, IEntityState
	{
		[Serializable]
		public class EntityStateUnityEvent : UnityEvent<byte> { }

		[SerializeField]
		private EntityStateUnityEvent OnStateChanged;

		[SerializeField]
		private IDefaultStateProvider defaultStateProvider;

		private byte state;
		public byte State
		{
			get
			{
				return state;
			}

			set
			{
				if(state != value)
				{
					state = value;
					OnStateChanged?.Invoke(state);
				}
			}
		}

		private void Start()
		{
			state = defaultStateProvider.DefaultState;
		}
	}
}
