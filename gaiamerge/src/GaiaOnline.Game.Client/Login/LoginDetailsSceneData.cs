using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloLive.Models.Authentication;
using UnityEngine;
using UnityEngine.Events;

namespace GaiaOnline
{
	/// <summary>
	/// Encapsulates the required login-data and lives in a Unity3D scene.
	/// </summary>
	public class LoginDetailsSceneData : MonoBehaviour, IUserAuthenticationDetailsContainer
	{
		/// <summary>
		/// String required for a login/authentication
		/// (Ex. Username, Email, One-off token)
		/// </summary>
		public string UserName { get; private set; }

		//TODO: Don't use this until we actually have a legit system
		/// <summary>
		/// Password used for authentication
		/// </summary>
		public string Password { get; private set; } = "test";

		//These setters are here for hooking to UnityEvents
		/// <summary>
		/// Sets the <see cref="UserName"/> value.
		/// Used to hook to <see cref="UnityEvent"/>
		/// </summary>
		/// <param name="value">Login string value.</param>
		public void SetLoginString(string value)
		{
			UserName = value;
		}

		/// <summary>
		/// Sets the <see cref="Password"/> value.
		/// Used to hook to <see cref="UnityEvent"/>
		/// </summary>
		/// <param name="value">Password value.</param>
		public void SetPasswordString(string value)
		{
			Password = value;
		}
	}
}
