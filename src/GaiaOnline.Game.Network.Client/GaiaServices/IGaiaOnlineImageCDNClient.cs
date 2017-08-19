using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;
using UnityEngine;

namespace Assets.Scripts.Clients
{
	//Just a get request to the Avatar url
	//Exmaple: http://a2.cdn.gaiaonline.com/dress-up/avatar/ava/9f/8b/28c7853cf78b9f.png
	public interface IGaiaOnlineImageCDNClient
	{
		[Get(@"/dress-up/avatar/{url}")] //last slash is important, do not remove
		Task<Texture2DWrapper> GetAvatarImage([AliasAs("url")] string uniqueAvatarUrl);
	}
}
