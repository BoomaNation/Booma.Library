using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GaiaOnline
{
	/// <summary>
	/// Acts as a 2D motor.
	/// </summary>
	public sealed class Direction2DMotor : MonoBehaviour, IDirectionChangeListener
	{
		/// <summary>
		/// The movement speed the motor shall move with.
		/// </summary>
		[SerializeField]
		[Tooltip("The movement speed the motor should move with.")]
		private float MovementSpeed;

		/// <summary>
		/// The cached direction.
		/// </summary>
		private Vector2 MovementDirection { get; set; }

		public void OnDirectionChanged(Vector2 direction)
		{
			//TODO: Should we verify this is always different in debug builds?
			MovementDirection = direction;
		}

		//In the fixed update we just move the transform of the gameobject this is attached to based on the provided direction and speed.
		private void FixedUpdate()
		{
			//Use XZ to keep it on the xz plane.
			//TODO: If you ever want an actual game then don't directly move the transform. Use a controller or navmesh agent.
			transform.position += MovementDirection.ToVector3XZ() * Time.fixedDeltaTime * MovementSpeed;
		}

	}
}

