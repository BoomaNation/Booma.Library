using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Contract for a movement motor for Entities.
	/// </summary>
	public interface IEntityMotor
	{
		/// <summary>
		/// Indicates if the motor is moving.
		/// </summary>
		/// <returns>True if the motor is moving.</returns>
		bool isMoving { get; }

		IMovementInput Input { get; }

		IMovementParameters Parameters { get; }

		/// <summary>
		/// Moves the entity.
		/// </summary>
		void Move(float timeDelta, Vector3 currentPosition);

		/// <summary>
		/// Moves the entity.
		/// </summary>
		void Move(float timeDelta);
	}
}
