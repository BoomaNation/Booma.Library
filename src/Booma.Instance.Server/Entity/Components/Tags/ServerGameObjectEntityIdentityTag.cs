using Booma;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	[Injectee]
	public class ServerGameObjectEntityIdentityTag : MonoBehaviour, IEntityGuidContainer, ITagProvider<NetworkEntityGuid>
	{
		[Inject]
		private readonly INetworkGuidFactory guidFactory;

		private Lazy<NetworkEntityGuid> networkGuid = null;
		public NetworkEntityGuid NetworkGuid { get { return networkGuid.Value; } }

		public NetworkEntityGuid Tag { get { return networkGuid.Value; } }

		private void Awake()
		{
			//Don't touch guid factory in awake. No injected property is available in awake reliably
			networkGuid = new Lazy<NetworkEntityGuid>(BuildGuid, true);
		}

		public NetworkEntityGuid BuildGuid()
		{
			return guidFactory.Create(EntityType.GameObject);
		}
	}
}
