using Booma.Payloads.Surrogates.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Booma.Payloads.Surrogates.Unity
{
	public static class QuaternionToSurrogateExtensions
	{
		/// <summary>
		/// Converts a provided <see cref="Quaternion"/> to the network surrogate.
		/// </summary>
		/// <param name="quat"></param>
		/// <returns></returns>
		public static QuaternionSurrogate ToSurrogate(this Quaternion quat)
		{
			//Just map the Quaternion to the surrogate
			return new QuaternionSurrogate(quat.x, quat.y, quat.z, quat.w);
		}
	}
}

namespace UnityEngine
{
	public static class SurrogateToQuaternionExtensions
	{
		/// <summary>
		/// Converts a provided <see cref="Quaternion"/> to the network surrogate.
		/// </summary>
		/// <param name="quat"></param>
		/// <returns></returns>
		public static Quaternion ToQuaternion(this QuaternionSurrogate quat)
		{
			//Just map the Quaternion to the surrogate
			return new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
		}
	}
}
