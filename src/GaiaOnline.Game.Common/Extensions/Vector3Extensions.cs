using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GaiaOnline
{
	/// <summary>
	/// Extension methods for the <see cref="Vector3"/> Type.
	/// </summary>
	public static class VectorExtensions
	{
		/// <summary>
		/// Converts a <see cref="Vector3"/> into a <see cref="Vector2"/> without the z component.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>The <see cref="Vector2"/> representation of the provided <see cref="Vector3"/> with x and y mapping.</returns>
		public static Vector2 ToVector2(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.y);
		}

		/// <summary>
		/// Converts a <see cref="Vector2"/> into a <see cref="Vector3"/> with the z component as 0.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>The <see cref="Vector3"/> representation of the provided <see cref="Vector2"/> with x and y mapping and z as 0.</returns>
		public static Vector3 ToVector3(this Vector2 vector)
		{
			return new Vector3(vector.x, vector.y);
		}

		/// <summary>
		/// Converts a <see cref="Vector2"/> into a <see cref="Vector3"/> with the z component as the y component
		/// and the x component as the x component. y will be 0.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>The <see cref="Vector3"/> representation of the provided <see cref="Vector2"/> with x and y mapping to x and z respectively. Y being 0.</returns>
		public static Vector3 ToVector3XZ(this Vector2 vector)
		{
			return new Vector3(vector.x, 0.0f, vector.y);
		}
	}
}
