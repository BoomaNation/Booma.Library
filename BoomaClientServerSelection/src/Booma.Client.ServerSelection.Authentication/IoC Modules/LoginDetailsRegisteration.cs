using Booma.Client.ServerSelection.Authentication;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Scene component that allows for the registeration of a <see cref="LoginDetailsSceneData"/> 
	/// for <see cref="LoginNetworkClient"/> peer types.
	/// </summary>
	public class LoginDetailsRegisteration : NonBehaviourDependency
	{
		//We should try to locate this object in the scene
		public override void Register(IServiceRegister register)
		{
			//It should be in the scene hopefully
			LoginDetailsSceneData loginDetails = MonoBehaviour.FindObjectOfType<LoginDetailsSceneData>();

			if (loginDetails == null)
				throw new Exception($"{nameof(loginDetails)} of type {nameof(LoginDetailsSceneData)} could not be found in the scene.");

			register.Register(loginDetails, getFlags(), typeof(ILoginDetails));
		}
	}
}
