using Booma.Payloads.Surrogates.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEngine
{
	public static class Vector3ToSurrogateExtensions
	{
		/// <summary>
		/// Converts a provided <see cref="Vector3"/> to the network surrogate.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static Vector3Surrogate ToSurrogate(this Vector3 vector)
		{
			//Just map the vector3 to the surrogate
			return new Vector3Surrogate(vector.x, vector.y, vector.z);
		}

		/// <summary>
		/// Converts a provided <see cref="Vector3"/> to the network surrogate.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static Vector3 ToVector3(this Vector3Surrogate vector)
		{
			//Just map the vector3 to the surrogate
			return new Vector3(vector.X, vector.Y, vector.Z);
		}
	}
}
