using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using GladNet.Message.Handlers;

namespace Booma
{
	public sealed class HandlerStagingComponent : MonoBehaviour
	{
#if !DEPLOY
		[Button("Generate")]
		public void InitializationButton()
		{
			//Foreach known type
			foreach(Type t in AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(a => a.GetTypes()))
			{
				if(typeof(MonoBehaviour).IsAssignableFrom(t) && !t.IsAbstract)
				{
					//Check if it's a request or event handler
					if(typeof(IRequestMessageHandler<InstanceClientSession>).IsAssignableFrom(t))
					{
						//If the component isn't on here we should attach it.
						if(GetComponent(t) == null)
						{
							gameObject.AddComponent(t);
						}
					}
				}
			}
		}
#endif
	}
}
