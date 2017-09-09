using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using SceneJect.Common;
using UnityEngine;

namespace Booma
{
	//TODO: Turn this into a generic scene data passing module
	public sealed class UserDetailsLocationRegisterationModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(IServiceRegister register)
		{
			LoginDetailsSceneData details = GameObject.FindObjectOfType<LoginDetailsSceneData>();

			//TODO: Should we throw? Or log? It might be more informative for the container
			//to say it doesn't have it and log the component requring it.
			if (details == null)
				return;

			register.RegisterInstance<LoginDetailsSceneData, IUserAuthenticationDetailsContainer>(details);
		}
	}
}
