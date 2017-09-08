using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GaiaOnline
{
	public sealed class GaiaCharacterInputDispatcher : MonoBehaviour
	{
		/// <summary>
		/// A unity serialization hack to allow us to serialize to the inspector custom UnityEvent types.
		/// </summary>
		[Serializable]
		private class OnDirectionChangedEvent : UnityEvent<Vector2> { }

		/// <summary>
		/// Event dispatched when movement starts.
		/// </summary>
		[SerializeField]
		private UnityEvent OnStartMoving;

		/// <summary>
		/// Event dispatched when movement stops.
		/// </summary>
		[SerializeField]
		private UnityEvent OnStopMoving;

		/// <summary>
		/// Event dispatched when the direction changes.
		/// </summary>
		[SerializeField]
		private OnDirectionChangedEvent OnDirectionChanged;

		//TODO: Look into IL generator/rewritter/weaver to dispatch change events. IPropertyNotify or whatever is too ugly to use
		//locally cached movement state
		private bool isMoving;

		//locally cached direction
		private Vector2 direction;

		//Must do in update, not fixed update
		void Update()
		{
			//TODO: Extract this into an input controller. We should only dispatch here
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			//If our movement state isn't matching the input then negate
			if (isMoving != input.magnitude > 0.0f)
			{
				if (isMoving)
					OnStopMoving?.Invoke();
				else
					OnStartMoving?.Invoke();

				isMoving = !isMoving;
			}

			//Now we need to set the direction if it's dirrection
			if (direction != input)
			{
				OnDirectionChanged?.Invoke(input);

				direction = input;
			}
		}
	}
}
