using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public class CharacterControllerMotor : GladMonoBehaviour, IEntityMotor
	{
		//TODO: Implement
		public bool isMoving { get; }

		public IMovementInput Input { get { return motorInput; } }

		public IMovementParameters Parameters { get { return movementParameters; } }

		[SerializeField]
		private CharacterController entityController;

		/// <summary>
		/// Motor input.
		/// </summary>
		[SerializeField]
		private IMovementInput motorInput;

		/// <summary>
		/// Parameters for movement.
		/// </summary>
		[SerializeField]
		private IMovementParameters movementParameters;

		//The timeDelta parameter allows the movement to be stepped forward through time.
		public void Move(float timeDelta, Vector3 currentPosition)
		{
			//Set the position to the provided position
			//This could be used to force an entity back to a previous position for rewind.
			this.transform.position = currentPosition;

			Move(timeDelta);
		}

		//The timeDelta parameter allows the movement to be stepped forward through time.
		public void Move(float timeDelta)
		{
			//Move the controller forward through time using the movement speed and the direction provided in the inputs
			entityController.Move(timeDelta * movementParameters.MovementSpeed * motorInput.Direction);
		}
	}
}
