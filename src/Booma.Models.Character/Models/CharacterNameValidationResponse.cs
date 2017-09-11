using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Models;
using Newtonsoft.Json;

namespace Booma
{
	public sealed class CharacterNameValidationResponse : IResponseModel<CharacterNameValidationResponseCode>, ISucceedable
	{
		/// <summary>
		/// Instance of the model that represents success.
		/// </summary>
		public static CharacterNameValidationResponse Success { get; } = new CharacterNameValidationResponse() { ResultCode = CharacterNameValidationResponseCode.Success };

		/// <summary>
		/// Indicates if the character name is valid.
		/// </summary>
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterNameValidationResponseCode.Success;

		/// <summary>
		/// Indicates the result of the name validation reuqest.
		/// This will likely indicate why the validation failed.
		/// </summary>
		[JsonProperty]
		public CharacterNameValidationResponseCode ResultCode { get; private set; }

		/// <summary>
		/// New response model with the specified <see cref="ResultCode"/>
		/// </summary>
		/// <param name="resultCode">Result</param>
		public CharacterNameValidationResponse(CharacterNameValidationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterNameValidationResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the CharacterNameValidationResponseCode enum.");

			ResultCode = resultCode;
		}

		private CharacterNameValidationResponse()
		{
			
		}
	}
}
