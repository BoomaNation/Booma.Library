using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma.Payloads.Surrogates.Unity
{
	/// <summary>
	/// Surrogate for the Unity3D Quaternion type.
	/// </summary>
	[GladNetSerializationContract]
	public class QuaternionSurrogate : Vector3Surrogate
	{
		/// <summary>
		/// The W coordinate of the Quaternion.
		/// </summary>
		[GladNetMember(GladNetDataIndex.Index1)]
		public float W { get; private set; }

		/// <summary>
		/// Creates a new Quaternion surrogate.
		/// </summary>
		/// <param name="x">X value.</param>
		/// <param name="y">Y value.</param>
		/// <param name="z">Z value.</param>
		/// <param name="w">W value.</param>
		public QuaternionSurrogate(float x, float y, float z, float w) 
			: base(x, y, z)
		{
			W = w;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected QuaternionSurrogate()
		{

		}
	}
}
