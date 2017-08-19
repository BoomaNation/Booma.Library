using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assets.Scripts
{
	//TODO: They use different encoding for XML than the default XmlSerializer
	//profiles?mode=lookup&avatar_username=
	//Example: http://gaiaonline.com/profiles?mode=lookup&avatar_username=lm_a_kitty_cat
	//<response status="200" avatarPath="ava/9f/8b/28c7853cf78b9f.png?t=1503043840_6.00_10" userId="16223135" />
	[Serializable]
	[XmlType("response")]
	public sealed class UserAvatarQueryResponse
	{
		//TODO: Extract to interface
		[XmlAttribute("status")]
		public int ResponseStatusCode { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		//TODO: Implement status code checking
		[XmlIgnore]
		public bool isRequestSuccessful => throw new NotImplementedException();

		[XmlAttribute("avatarPath")]
		public string AvatarRelativeUrlPath { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		//XML has issues with empty strings deserializing to ints, maybe only Mono? So we'll keep this as a string
		//This will only ever be "" when an error occurs anyway
		//TODO: Any use for this at the momemt?
		[XmlAttribute("userId")]
		public string UserId { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		public UserAvatarQueryResponse(int responseStatusCode, string avatarRelativeUrlPath, string userId)
		{
			if (string.IsNullOrWhiteSpace(avatarRelativeUrlPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(avatarRelativeUrlPath));

			ResponseStatusCode = responseStatusCode;
			AvatarRelativeUrlPath = avatarRelativeUrlPath;
			UserId = userId;
		}

		//TODO: Can we make this protected? Or will XML cry?
		public UserAvatarQueryResponse()
		{

		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{nameof(ResponseStatusCode)}: {ResponseStatusCode} {nameof(AvatarRelativeUrlPath)}: {AvatarRelativeUrlPath} {nameof(UserId)}: {UserId}";
		}
	}
}
