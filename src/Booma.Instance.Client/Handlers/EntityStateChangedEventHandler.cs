using Booma.Client.Network.Common;
using Booma.Payloads.Instance;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Common.Logging;
using Booma.Instance.Common;

namespace Booma.Instance.Client.Handlers
{
	[Injectee]
	public class EntityStateChangedEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, EntityStateChangedEvent>
	{
		[Inject]
		private readonly ILog logger;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		protected override void OnIncomingHandlableMessage(IEventMessage message, EntityStateChangedEvent payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			logger.Debug($"Recieved state change from Entity Id: {payload.EntityGuid.EntityId} with new state Value: {payload.State}.");

			if(entityCollection.ContainsKey(payload.EntityGuid))
			{
				var entityChunk = entityCollection[payload.EntityGuid];

				IEntityState state = entityChunk.WorldObject.GetComponent<IEntityState>();

				if (state == null)
					throw new InvalidOperationException($"Failed to find {nameof(IEntityState)} component on GameObject: {entityChunk.WorldObject.name} with Entity Id: {payload.EntityGuid.EntityId}.");

				//Just straight set the state from the payload.
				state.State = payload.State;
			}
			else
				logger.Warn($"Recieved state change from UNKNOWN Entity Id: {payload.EntityGuid.EntityId} with new state Value: {payload.State}.");
		}
	}
}
