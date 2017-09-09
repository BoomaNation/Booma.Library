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
#if !DEPLOY
		[Button("Init")]
		private void InitializationButton()
		{
			//Initialization would be to try to initialize the StateContainer if it's null
			if(StateContainer == null)
				StateContainer = GetComponent(typeof(IEntityStateContainer<TNetworkStateType>))
					as IEntityStateContainer<TNetworkStateType>;
		}
#endif

		//TODO: Find a way to make this serializable and generic
		//We can't make this generic or it won't work
		[NotNull]
		[Required("Network objects require a reference to a component that contains their corresponding state.", InfoMessageType.Error)]
		[OdinSerialize]
		protected IEntityStateContainer<TNetworkStateType> StateContainer;

		protected virtual void Start()
		{
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
