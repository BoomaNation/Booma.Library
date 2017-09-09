using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Generic.Math;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Booma
{
	public abstract class NetworkGameObject<TNetworkStateType> : SerializedMonoBehaviour, IEntityStateListener
		where TNetworkStateType : struct, IConvertible
	{
		//TODO: Find a way to make this serializable and generic
		//We can't make this generic or it won't work
		[NotNull]
		[OdinSerialize]
		[SerializeField]
		protected IEntityStateContainer<TNetworkStateType> StateContainer;

		protected virtual void Start()
		{
			if(StateContainer == null)
				throw new InvalidOperationException($"Service {nameof(IEntityStateContainer<TNetworkStateType>)} is null and unavailable.");

			OnStart(StateContainer.State);
		}

		/// <inheritdoc />
		public void OnEntityStateChanged(byte value) => OnEntityStateChanged(GenericMath<byte, TNetworkStateType>.Convert(value));

		protected abstract void OnStart(TNetworkStateType initialState);

		/// <summary>
		/// Called internally when the state of the <see cref="NetworkGameObject{TNetworkStateType}"/> changes.
		/// </summary>
		/// <param name="newState">The new state.</param>
		protected abstract void OnEntityStateChanged(TNetworkStateType newState);
	}
}
