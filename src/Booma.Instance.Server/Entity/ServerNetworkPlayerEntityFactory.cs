using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Instance.Common;
using JetBrains.Annotations;
using SceneJect.Common;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Server implementation of the <see cref="NetworkPlayerEntityFactory"/> that extends
	/// the construction of the network player entities with server specific logic for networking.
	/// </summary>
	[Injectee]
	public sealed class ServerNetworkPlayerEntityFactory : NetworkPlayerEntityFactory
	{
		/// <summary>
		/// The translation service that translates network IDs to entity guids.
		/// </summary>
		[Inject]
		private IConnectionToGuidRegistry ConnectionRegistry { get; }

		/// <summary>
		/// Player entity collection.
		/// </summary>
		[Inject]
		private NetworkEntityCollection EntityCollection { get; }

		/// <summary>
		/// The peer collection.
		/// </summary>
		[Inject]
		private IPeerCollection PeerCollection { get; }

		/// <inheritdoc />
		protected override IEntitySpawnResults OnEntityConstructionCompleted([NotNull] IPlayerSpawnContext context, [NotNull] IEntitySpawnResults result)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(result == null) throw new ArgumentNullException(nameof(result));

			//Check if the spawn was a success. If not we don't want to add information about it to the network services.
			if(result.Result != SpawnResult.Success)
				return result;

			//TODO: Verify that none of this is in the services/collections all ready. MAJOR ISSUES if we are getting multiple of the same entity.
			EntityCollection.Add(context.EntityGuid, new EntityContainer(context.EntityGuid, result.EntityGameObject));
			PeerCollection.Add(context.OwnerPeer);
			ConnectionRegistry.Register(context.OwnerPeer.PeerDetails.ConnectionID, context.EntityGuid);

			return result;
		}
	}
}
