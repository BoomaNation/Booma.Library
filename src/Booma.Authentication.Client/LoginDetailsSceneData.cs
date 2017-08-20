using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloLive.Models.Authentication;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client
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

		/// <summary>
		/// Password used for authentication
		/// </summary>
		public string Password { get; private set; }

		private void Awake()
		{
			//We should keep the object alive until someone destroys it.
			DontDestroyOnLoad(this);
		}

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
