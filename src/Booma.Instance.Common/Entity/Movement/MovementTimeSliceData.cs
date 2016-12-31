using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Structure that contains the data of a timeslice such as: Timestamp, position and direction.
	/// </summary>
	public class MovementTimeSliceData
	{
		/// <summary>
		/// Timestamp for the movement data.
		/// </summary>
		public float TimeStamp { get; private set; }

		/// <summary>
		/// Represents the computed position at this timeslice.
		/// </summary>
		public Vector3 Position { get; private set; }

		/// <summary>
		/// Represents the direction at this timeslide.
		/// </summary>
		public Vector3 Direction { get; private set; }

		public MovementTimeSliceData(float timeStamp, Vector3 position, Vector3 direction)
		{
			TimeStamp = timeStamp;
			Position = position;
			Direction = direction;
		}
	}
}
