using GladBehaviour.Common;
using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.NetworkObject
{
	public abstract class NetworkGameObject<TNetworkStateType> : GladMonoBehaviour, IEntityStateListener
		where TNetworkStateType : struct, IConvertible
	{
		[NotNull]
		[SerializeField]
		private IEntityStateContainer<TNetworkStateType> stateContainer;

		/// <summary>
		/// Represents the current state of the door.
		/// </summary>
		protected TNetworkStateType State => stateContainer.State;

		/// <summary>
		/// Set the current <see cref="NetworkGameObject{TNetworkStateType}"/>'s <typeparamref name="TNetworkStateType"/>.
		/// </summary>
		/// <param name="state"></param>
		protected void SetState(TNetworkStateType state)
		{
			stateContainer.State = state;
		}

		protected virtual void Start()
		{
			if(stateContainer == null)
				throw new InvalidOperationException($"Service {nameof(IEntityStateContainer<TNetworkStateType>)} is null and unavailable.");

			OnStart(stateContainer.State);
		}

		/// <inheritdoc />
		void IEntityStateListener.OnEntityStateChanged(byte value) => OnEntityStateChanged(Operator<byte, TNetworkStateType>.Convert(value));

		protected abstract void OnStart(TNetworkStateType initialState);

		/// <summary>
		/// Called internally when the state of the <see cref="NetworkGameObject{TNetworkStateType}"/> changes.
		/// </summary>
		/// <param name="newState">The new state.</param>
		protected abstract void OnEntityStateChanged(TNetworkStateType newState);
	}
}
