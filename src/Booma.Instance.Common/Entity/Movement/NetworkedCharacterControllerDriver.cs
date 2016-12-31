using GladBehaviour.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	[RequireComponent(typeof(NetworkedEntityMovementDataProvider))]
	public class NetworkedCharacterControllerDriver : GladMonoBehaviour
	{
		[SerializeField]
		private IEntityMotor entityMotor;

		[SerializeField]
		private float timeStep;

		private List<MovementTimeSliceData> movementBuffer;

		private float currentDeltaFromLastUpdate = 0.0f;

		private float lastKnownNetworkTime;

		/// <summary>
		/// If this is an entity on the client it doesn't have authority (or shouldn't).
		/// Not even the player should have authority over itself.
		/// </summary>
		[SerializeField]
		private bool hasAuthority;

		void Start()
		{
			if (hasAuthority)
				lastKnownNetworkTime = Time.time;
		}

		private void FixedUpdate()
		{
			//Don't update until we know the network time
			if (lastKnownNetworkTime == 0.0f)
				return;

			//Check and see if we should move to a new slice
			if (currentDeltaFromLastUpdate > timeStep)
			{
				HandleNewSliceEntry(currentDeltaFromLastUpdate + lastKnownNetworkTime, this.transform.position);
				lastKnownNetworkTime += currentDeltaFromLastUpdate; //move the known time step forward; we're predicting at this point
				currentDeltaFromLastUpdate = 0.0f;
			}
			else
				currentDeltaFromLastUpdate += Time.fixedDeltaTime;

			entityMotor.Move(Time.fixedDeltaTime);
		}

		public void MovementInstruction(float timeStamp, Vector3 direction, Vector3 position)
		{
			lastKnownNetworkTime = timeStamp;

			//If we have authority then we're done
			if (hasAuthority)
				return;

			int index = -1;

			//We should check prediction too. Find the closest slice
			//Look backwards since it'll be closest
			for (int i = movementBuffer.Count; i >= 0; i--)
			{
				//If we find a timestamp greater than the one that was sent
				if (movementBuffer[i].TimeStamp >= timeStamp)
					index = i;
				else
					break;
			}

			//If an index was found it should be index
			if(index != -1)
			{
				//First we should find the last time in predicted section
				float predictedDelta = movementBuffer.Last().TimeStamp - timeStamp + currentDeltaFromLastUpdate;

				//This index should be the current data being provided.
				//Right now it's predicted data and we don't even need to check it.
				//We should just override it and then resimulate forward.
				if (index + 1 != movementBuffer.Count)
					movementBuffer.RemoveRange(index + 1, movementBuffer.Count - 1 - index + 1);

				//Now we should set the index to the movement slice provided
				movementBuffer[index] = new MovementTimeSliceData(timeStamp, position, direction);

				//Now resimulate from the position the server provided
				entityMotor.Move(predictedDelta, position);
			}
		}

		private void HandleNewSliceEntry(float timeStamp, Vector3 position)
		{
			movementBuffer.Add(new MovementTimeSliceData(timeStamp, position, entityMotor.Input.Direction));
		}
	}
}
