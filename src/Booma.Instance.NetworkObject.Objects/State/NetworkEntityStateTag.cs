using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Instance.NetworkObject
{
	[Serializable]
	public class EntityStateUnityEvent : UnityEvent<byte> { }

	//TODO: Cleanup and document
	/// <summary>
	/// Object component that contains and broadcasts the entity's state, of type <typeparamref name="TStateType"/>, to listeners
	/// and tracks the current state of the object.
	/// </summary>
	/// <typeparam name="TStateType"></typeparam>
	public abstract class NetworkEntityStateTag<TStateType> : GladMonoBehaviour, IEntityStateContainer<TStateType>,
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

		byte IDefaultStateProvider.DefaultState => MiscUtil.Operator<TStateType, byte>.Convert(defaultState);

		/// <summary>
		/// The current state of the entity.
		/// </summary>
		public TStateType State
		{
			get { return MiscUtil.Operator<byte, TStateType>.Convert(state); }
			set { ((IEntityStateContainer)this).State = MiscUtil.Operator<TStateType, byte>.Convert(value); }
		}

		private byte state;
		byte IEntityStateContainer.State
		{
			get
			{
				return state;
			}

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
			state = MiscUtil.Operator<TStateType, byte>.Convert(defaultState);
		}
	}
}
