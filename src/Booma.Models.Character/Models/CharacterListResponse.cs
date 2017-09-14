using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloLive.Models;
using Newtonsoft.Json;

namespace Booma
{
	/// <summary>
	/// Response model for a character list request.
	/// </summary>
	[JsonObject]
	public sealed class CharacterListResponse : IResponseModel<CharacterListResponseCode>, ISucceedable
	{
		/// <summary>
		/// Indicates the result of the <see cref="CharacterListResponse"/>.
		/// </summary>
		[JsonProperty]
		public CharacterListResponseCode ResultCode { get; private set; }

		/// <summary>
		/// The ID's of the characters in the list.
		/// </summary>
		[JsonIgnore]
		public IEnumerable<int> CharacterIds => characterIds;

		/// <summary>
		/// Indicates if the result was sucessful.
		/// </summary>
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterListResponseCode.Success;

		/// <summary>
		/// Serialized collection of character ids.
		/// </summary>
		[JsonProperty]
		private int[] characterIds { get; set; }

		/// <summary>
		/// A new failed response.
		/// (Do not provided Success)
		/// </summary>
		/// <param name="resultCode">Result code (not success)</param>
		public CharacterListResponse(CharacterListResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterListResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the CharacterListResponseEnum enum.");
			if(resultCode == CharacterListResponseCode.Success) throw new ArgumentException($"Cannot provide {nameof(CharacterListResponseCode)} of {resultCode} without providing the characters.");

			characterIds = Enumerable
				.Empty<int>()
				.ToArray();

			ResultCode = resultCode;
		}

		/// <summary>
		/// A successful character list response.
		/// </summary>
		/// <param name="characters">The character's ids.</param>
		public CharacterListResponse(IEnumerable<int> characters)
		{
			if(characters == null) throw new ArgumentNullException(nameof(characters));

			//Even if the character collection is empty, meaning no characters are associated with this account, then the result is success.
			ResultCode = CharacterListResponseCode.Success;
			characterIds = characters.ToArray();
		}

		private CharacterListResponse()
		{
			
		}
	}
}
