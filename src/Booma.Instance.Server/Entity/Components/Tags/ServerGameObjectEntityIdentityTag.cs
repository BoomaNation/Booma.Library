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
		//TODO: Should we inject a guid ourselves?
		[Inject]
		private INetworkGuidFactory GuidFactory { get; }

		/// <inheritdoc />
		public NetworkEntityGuid NetworkGuid => Tag;

		/// <inheritdoc />
		public NetworkEntityGuid Tag { get; private set; }

		private void Start()
		{
			//Just create a new guid for the tag.
			Tag = GuidFactory.Create(EntityType.GameObject);
		}
	}
}
