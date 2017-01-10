using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.Common
{
	[Serializable]
	public class EntityStateUnityEvent : UnityEvent<byte> { }

	//TODO: Cleanup and document
	public abstract class NetworkEntityStateTag<TStateType> : GladMonoBehaviour, IEntityState<TStateType>,
		IDefaultStateProvider<TStateType>
		where TStateType : struct, IConvertible
	{
		
		[SerializeField]
		private EntityStateUnityEvent OnStateChanged;

		[SerializeField]
		private TStateType defaultState;

		public TStateType DefaultState { get { return defaultState; } }

		byte IDefaultStateProvider.DefaultState { get { return MiscUtil.Operator<TStateType, byte>.Convert(defaultState); } }

		public TStateType State
		{
			get { return MiscUtil.Operator<byte, TStateType>.Convert(state); }
			set { ((IEntityState)this).State = MiscUtil.Operator<TStateType, byte>.Convert(value); }
		}

		private byte state;
		byte IEntityState.State
		{
			get
			{
				return state;
			}

			set
			{
				if (state != value)
				{
					state = value;
					OnStateChanged?.Invoke(state);
				}
			}
		}

		void Awake()
		{
			//Set the default state.
			state = MiscUtil.Operator<TStateType, byte>.Convert(defaultState);
		}
	}
}
