using GladLive.Security.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Scene component that allows for the registeration of a <see cref="ICryptoService"/>.
	/// </summary>
	public class RSAServiceRegisteration : NonBehaviourDependency
	{
		[SerializeField]
		private string rsaKeyFileName;

		public override void Register(IServiceRegister register)
		{
			RSACryptoServiceProvider provider = null;

			//Depending on if we're in the editor or if we're in a standalone
			//build we'll need to load this from a different location.
			if (Application.isEditor)
			{
				//if we're in the editor we should load the test RSA file
				//which is located in the default directory
				string rsaFile = File.ReadAllText($"Assets/{rsaKeyFileName}.rsa");

				provider = new RSACryptoServiceProvider();

				provider.FromXmlString(rsaFile);
			}
			else
			{
				//TODO: Determine where we load it.
			}

			register.Register(new RSACryptoProviderAdapter(provider), getFlags(), typeof(ICryptoService));
		}
	}
}
