using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneJect.Common;
using UnityEngine;

namespace Booma
{
	/// <summary>
	/// Base class for HTTP service registerations.
	/// </summary>
	public abstract class HttpServiceRegisterationModule : NonBehaviourDependency
	{
		[SerializeField]
		[Tooltip("This should be the URL/URI that points to the base path of the service.")]
		private string _BaseUrl;

		/// <summary>
		/// The URL/URI of the registered service.
		/// </summary>
		public string BaseUrl => _BaseUrl;

		private void Awake()
		{
			if(String.IsNullOrEmpty(BaseUrl))
				throw new InvalidOperationException($"The provided {nameof(BaseUrl)} for the {GetType().FullName} registeration component was invalid.");
		}
	}
}
