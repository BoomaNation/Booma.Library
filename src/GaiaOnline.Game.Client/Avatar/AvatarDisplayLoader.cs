using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SceneJect.Common;
using UnityEngine;
using Unitysync.Async;

namespace GaiaOnline
{
	/// <summary>
	/// Component that can load the display for an avatar.
	/// </summary>
	[Injectee]
	public sealed class AvatarDisplayLoader : MonoBehaviour, IInitializable
	{
		//TODO: Remove this, it's for testing.
		public string UserName;

		/// <summary>
		/// The client for querying Gaia.
		/// </summary>
		[Inject]
		private IGaiaOnlineQueryClient QueryClient { get; }

		/// <summary>
		/// The client for loading images from the Gaia CDN.
		/// </summary>
		[Inject]
		private IGaiaOnlineImageCDNClient ImageClient { get; }

		//These should be the renderers to initialize the Avatar image to.
		[SerializeField]
		[Tooltip("List of renderers to set the loaded Avatar image to.")]
		private Renderer[] AvatarRenderers;

		private void Start()
		{
			if(AvatarRenderers == null)
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be null.");

			if (!AvatarRenderers.Any())
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be empty.");
		}

		/// <inheritdoc />
		public void Initialize()
		{
			Reinitialize();
		}

		/// <inheritdoc />
		public void Reinitialize()
		{
			//It's possible reinitialize was called while we were initializing the renderers
			//So we should stop the routines
			StopAllCoroutines();

			//We can't really recover from this but we can log
			try
			{
				QueryClient.GetAvatarFromUsername(UserName)
					.UnityAsyncContinueWith(this, GetAvatarImage)
					.UnityAsyncContinueWith(this, StartAvatarRendererConfigurationCoroutine);
			}
			catch (Exception e)
			{
				//TODO: Add username/id info
				Debug.LogError($"Encountered error when attempting to load the avatar: {e.Message}");
				throw;
			}
		}

		private void StartAvatarRendererConfigurationCoroutine([NotNull] Texture2D texture)
		{
			if (texture == null) throw new ArgumentNullException(nameof(texture));

			//We use a coroutine because creating a material and setting a texture is slightly costly and it scales
			//O(n) for the renderers we need to set so we can save some frame per frame perf by doing only 1 renderer per frame.
			StartCoroutine(SetAvatarForRenderers(texture));
		}

		private async Task<Texture2D> GetAvatarImage([NotNull] UserAvatarQueryResponse response)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));
			if (String.IsNullOrWhiteSpace(response.AvatarRelativeUrlPath))
				throw new InvalidOperationException("The avatar query request failed.");

			//TODO: Somehow get ref to avatar name for logging.
			if(!response.isRequestSuccessful)
				throw new InvalidOperationException($"Encounted Error Code: {response.ResponseStatusCode} when trying to request Avatar.");

			//Change the URL to get the strip
			string stripUrl = $"{response.AvatarRelativeUrlPath.Split('?').First().TrimEnd(".png".ToCharArray())}_strip.png";

			return (await ImageClient.GetAvatarImage(stripUrl)).Texture.Value;
		}

		private IEnumerator SetAvatarForRenderers([NotNull] Texture2D texture)
		{
			if (texture == null) throw new ArgumentNullException(nameof(texture));

			foreach (Renderer r in AvatarRenderers)
			{
				r.material.mainTexture = texture;
				yield return null;
			}
		}
	}
}
