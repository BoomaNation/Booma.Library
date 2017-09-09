using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Math;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Booma
{
	[Serializable]
	public class EntityStateUnityEvent : UnityEvent<byte> { }

	//TODO: Cleanup and document
	/// <summary>
	/// Object component that contains and broadcasts the entity's state, of type <typeparamref name="TStateType"/>, to listeners
	/// and tracks the current state of the object.
	/// </summary>
	/// <typeparam name="TStateType"></typeparam>
	public abstract class NetworkEntityStateTag<TStateType> : SerializedMonoBehaviour, IEntityStateContainer<TStateType>,
		IDefaultStateProvider<TStateType>
		where TStateType : struct, IConvertible
	{
		/// <summary>
		/// Event that is invoked when the state changes.
		/// </summary>
		[SerializeField]
		private EntityStateUnityEvent OnStateChanged;

		/// <summary>
		/// The default state for the entity.
		/// </summary>
		[SerializeField]
		private TStateType defaultState;

		/// <summary>
		/// The default state for the entity.
		/// </summary>
		public TStateType DefaultState => defaultState;

		byte IDefaultStateProvider.DefaultState => GenericMath<TStateType, byte>.Convert(defaultState);

		/// <summary>
		/// The current state of the entity.
		/// </summary>
		public TStateType State
		{
			get => GenericMath<byte, TStateType>.Convert(state);
			set => ((IEntityStateContainer)this).State = GenericMath<TStateType, byte>.Convert(value);
		}

		private byte state;
		byte IEntityStateContainer.State
		{
			get => state;

			set
			{
				if (state == value) return;

				state = value;
				OnStateChanged?.Invoke(state);
			}
		}

		private void Awake()
		{
			//This is used to warn users that they may not have properly setup state broadcasts.
			if(OnStateChanged == null || OnStateChanged.GetPersistentEventCount() == 0)
				Debug.LogWarning($"{name} object's {GetType().FullName} does not contain any listeners for its state change.");

			//Set the default state.
			state = GenericMath<TStateType, byte>.Convert(defaultState);
		}
	}
}
