using Booma.Instance.Common;
using GladNet.Message;
using GladNet.Message.Handlers;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public class InstanceClientSessionRequestHandlerRegisteration : NonBehaviourDependency
	{
		public override void Register(IServiceRegister register)
		{
			//Chain handling semantics are preferable on the client.
			//No reason multiple handlers should be consuming the same packets, complications things
			//Put it infront of the handling process if you want that functionality
			ChainMessageHandlerStrategy<InstanceClientSession, IRequestMessage> chainHandler = new ChainMessageHandlerStrategy<InstanceClientSession, IRequestMessage>(FindHandlersInScene());

			//generics are semi-limited in .Net when trying to construct an instance of the type so we put the requirement for inheritors to create the instance.
			register.Register<RequestMessageHandlerService<InstanceClientSession>>(new RequestMessageHandlerService<InstanceClientSession>(chainHandler), getFlags(), typeof(IRequestMessageHandlerService<InstanceClientSession>));
		}

		/// <summary>
		/// Locates references to all <see cref="MonoBehaviour"/>s in the current scene that implement
		/// <see cref="IPayloadHandler{TPeerType}"/>
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IMessageHandler<InstanceClientSession, IRequestMessage>> FindHandlersInScene()
		{
			//Used for debugging for count of handlers in the scene
			//The type co/contra variance in Unity is iffy
#if DEBUG || DEBUG_BUILD || DEBUGBUILD
			IEnumerable<IMessageHandler<InstanceClientSession, IRequestMessage>> monoBehaviours =
#else
				return
#endif
			Resources.FindObjectsOfTypeAll<MonoBehaviour>()
				.Where(mb => mb.GetType().GetInterfaces().Where(t => typeof(IRequestMessageHandler<InstanceClientSession>).IsAssignableFrom(t)).Count() != 0) //this checks to see if the handler we found is assignable to a potentially less derived interface type. (it's co? or maybe contra? variant)
				.Cast<IMessageHandler<InstanceClientSession, IRequestMessage>>();

#if DEBUG || DEBUG_BUILD || DEBUGBUILD
			Debug.Log($"{monoBehaviours.Count()}");

			return monoBehaviours.Cast<IMessageHandler<InstanceClientSession, IRequestMessage>>();
#endif
		}
	}
}
