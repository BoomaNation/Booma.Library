using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// Surrogate for the Unity3D Vector3 type.
	/// </summary>
	[GladNetSerializationContract]
	[GladNetSerializationInclude(1, typeof(QuaternionSurrogate), true)]
	public class Vector3Surrogate
	{
		/// <summary>
		/// X coordinate of the Vector.
		/// </summary>
		[GladNetMember(2)]
		public float X { get; private set; }

		/// <summary>
		/// Y coordinate of the Vector.
		/// </summary>
		[GladNetMember(3)]
		public float Y { get; private set; }

		/// <summary>
		/// Z coordinate of the Vector.
		/// </summary>
		[GladNetMember(4)]
		public float Z { get; private set; }

		/// <summary>
		/// Creates a new Vector3 surrogate.
		/// </summary>
		/// <param name="x">X value.</param>
		/// <param name="y">Y value.</param>
		/// <param name="z">Z value.</param>
		public Vector3Surrogate(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected Vector3Surrogate()
		{

		}
	}
}
