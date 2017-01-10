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
	public abstract class NetworkGameObject<TNetworkStateType> : GladMonoBehaviour, IEntityStateListener
		where TNetworkStateType : struct, IConvertible
	{
		[SerializeField]
		private IEntityState<TNetworkStateType> stateContainer;

		/// <summary>
		/// Represents the current state of the door.
		/// </summary>
		public TNetworkStateType State
		{
			get { return stateContainer.State; }
			set { stateContainer.State = value; }
		}

		protected virtual void Start()
		{
			HandleInitialState(this.stateContainer.State);
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
