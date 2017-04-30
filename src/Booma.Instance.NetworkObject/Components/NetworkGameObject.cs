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
		protected IEntityStateContainer<TNetworkStateType> StateContainer;

		protected virtual void Start()
		{
			if(StateContainer == null)
				throw new InvalidOperationException($"Service {nameof(IEntityStateContainer<TNetworkStateType>)} is null and unavailable.");

			OnStart(StateContainer.State);
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
