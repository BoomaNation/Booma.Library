using GladBehaviour.Common;
using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.Common
{
	public abstract class NetworkGameObject<TNetworkStateType> : GladMonoBehaviour, IEntityState<TNetworkStateType>, IDefaultStateProvider, IDefaultStateProvider<TNetworkStateType>,
		IEntityStateListener
		where TNetworkStateType : struct, IConvertible
	{
		//[SerializeField]
		//private UnityEvent OnEntityDespawn;

		[SerializeField]
		private IEntityState stateContainer;

		/// <summary>
		/// Indicates the default state.
		/// </summary>
		byte IDefaultStateProvider.DefaultState { get { return Operator<TNetworkStateType, byte>.Convert(DefaultState); } }

		[SerializeField]
		private TNetworkStateType defaultNetworkState;
		/// <summary>
		/// Indicates the default <see cref="TNetworkStateType"/> value.
		/// </summary>
		public TNetworkStateType DefaultState { get { return defaultNetworkState; } }

		/// <summary>
		/// Represents the current state of the door.
		/// </summary>
		public TNetworkStateType State
		{
			get { return Operator<byte, TNetworkStateType>.Convert(stateContainer.State); }
			set { stateContainer.State = Operator<TNetworkStateType, byte>.Convert(value); }
		}

		protected virtual void Start()
		{
			HandleInitialState(DefaultState);
		}

		public void OnEntityStateChanged(byte value)
		{
			OnEntityStateChanged(Operator<byte, TNetworkStateType>.Convert(value));
		}

		protected abstract void HandleInitialState(TNetworkStateType state);

		/// <summary>
		/// Called internally when the state of the <see cref="NetworkGameObject{TNetworkStateType}"/> changes.
		/// </summary>
		/// <param name="newState">The new state.</param>
		protected abstract void OnEntityStateChanged(TNetworkStateType newState);
	}
}
