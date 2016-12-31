using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	public class NetworkedEntityMovementDataProvider : MonoBehaviour, IMovementInput, IMovementParameters
	{
		public Vector3 Direction { get; }

		[SerializeField]
		private float DefaultMovementSpeed;

		public float MovementSpeed { get; private set; }

		void Start()
		{
			MovementSpeed = DefaultMovementSpeed;
		}

		public void SetData(Vector3 direction, float movementSpeed)
		{
			MovementSpeed = movementSpeed;
		}
	}
}
