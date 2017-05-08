using GladNet.Engine.Common;
using GladNet.Message;
using GladNet.Message.Handlers;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Client.Network.Common
{
	/// <summary>
	/// Abstract generic network message handler IoC/DI <see cref="SceneJect"/> registeration module.
	/// Registers all network message handlers for <typeparamref name="TPeerType"/> peers.
	/// Handlers are indicated by generic type arg 22 <typeparamref name="TNetworkHandlerType"/>
	/// </summary>
	/// <typeparam name="TPeerType"></typeparam>
	public abstract class NetworkMessageHandlerServiceRegistration<TPeerType, TNetworkMessageType, TNetworkHandlerType, THandlerServiceTypeConcrete, THandlerServiceTypeServiceInterface> : NonBehaviourDependency
		where TPeerType : INetPeer
		where TNetworkMessageType : INetworkMessage
		where TNetworkHandlerType : IMessageHandler<TPeerType, TNetworkMessageType>
		where THandlerServiceTypeConcrete : class, TNetworkHandlerType, THandlerServiceTypeServiceInterface //there is no interface for handler services
		where THandlerServiceTypeServiceInterface : IMessageHandler<TPeerType, TNetworkMessageType>
	{
		public override void Register(IServiceRegister register)
		{
			//Chain handling semantics are preferable on the client.
			//No reason multiple handlers should be consuming the same packets, complications things
			//Put it infront of the handling process if you want that functionality
			ChainMessageHandlerStrategy<TPeerType, TNetworkMessageType> chainHandler = new ChainMessageHandlerStrategy<TPeerType, TNetworkMessageType>(FindHandlersInScene());

			//generics are semi-limited in .Net when trying to construct an instance of the type so we put the requirement for inheritors to create the instance.
			register.Register<THandlerServiceTypeConcrete>(CreateConcreteService(chainHandler), getFlags(), typeof(THandlerServiceTypeServiceInterface));
		}

		/// <summary>
		/// Produces a concrete non-null instance of <typeparamref name="THandlerServiceTypeConcrete"/> for service registeration with the provided
		/// <see cref="IPayloadHandlerStrategy{TSessionType}"/> strategy.
		/// </summary>
		/// <param name="strat"></param>
		/// <returns></returns>
		protected abstract THandlerServiceTypeConcrete CreateConcreteService(IMessageHandlerStrategy<TPeerType, TNetworkMessageType> strat);

		/// <summary>
		/// Locates references to all <see cref="MonoBehaviour"/>s in the current scene that implement
		/// <see cref="IPayloadHandler{TPeerType}"/>
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IMessageHandler<TPeerType, TNetworkMessageType>> FindHandlersInScene()
		{
			//Used for debugging for count of handlers in the scene
			//The type co/contra variance in Unity is iffy
#if DEBUG || DEBUG_BUILD || DEBUGBUILD
			IEnumerable<IMessageHandler<TPeerType, TNetworkMessageType>> monoBehaviours =
#else
				return
#endif
			Resources.FindObjectsOfTypeAll<MonoBehaviour>()
				.Where(mb => mb.GetType().GetInterfaces().Where(t => typeof(TNetworkHandlerType).IsAssignableFrom(t)).Count() != 0 ) //this checks to see if the handler we found is assignable to a potentially less derived interface type. (it's co? or maybe contra? variant)
				.Cast<IMessageHandler<TPeerType, TNetworkMessageType>>();

#if DEBUG || DEBUG_BUILD || DEBUGBUILD
			Debug.Log($"{monoBehaviours.Count()}");

			return monoBehaviours.Cast<IMessageHandler<TPeerType, TNetworkMessageType>>();
#endif
		}
	}
}
