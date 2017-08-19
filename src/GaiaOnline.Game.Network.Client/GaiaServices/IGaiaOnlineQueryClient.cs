using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;

namespace Assets.Scripts
{
	///profiles?mode=lookup&avatar_username=
	public interface IGaiaOnlineQueryClient
	{
		//Should end up as something like this ?mode=lookup&avatar_username={username}
		//Example: http://gaiaonline.com/profiles?mode=lookup&avatar_username=lm_a_kitty_cat
		[Get("/profiles?mode=lookup")]
		Task<UserAvatarQueryResponse> GetAvatarFromUsername([AliasAs("avatar_username"), QueryStringParameter] string userName);
	}
}
