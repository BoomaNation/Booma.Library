using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HaloLive.Models;
using Newtonsoft.Json;

namespace Booma
{
	/// <summary>
	/// Response model for the <see cref="CharacterCreationRequest"/>.
	/// </summary>
	public sealed class CharacterCreationResponse : IResponseModel<CharacterCreationResponseCode>, ISucceedable
	{
		/// <summary>
		/// The result code for the response.
		/// </summary>
		[JsonProperty]
		public CharacterCreationResponseCode ResultCode { get; private set; }

		/// <summary>
		/// The section id for the created character.
		/// </summary>
		[JsonProperty]
		public SectionId CharacterSectionId { get; private set; }

		/// <summary>
		/// Indicates if the request was sucessful.
		/// </summary>
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterCreationResponseCode.Success;

		/// <summary>
		/// Creates a sucessful character creation response
		/// with the provided section id.
		/// </summary>
		public CharacterCreationResponse(SectionId characterSectionId)
		{
			if(!Enum.IsDefined(typeof(SectionId), characterSectionId)) throw new ArgumentOutOfRangeException(nameof(characterSectionId), "Value should be defined in the SectionId enum.");

			ResultCode = CharacterCreationResponseCode.Success;
			CharacterSectionId = characterSectionId;
		}

		/// <summary>
		/// Creates a failing character creation response.
		/// </summary>
		/// <param name="resultCode">The failing reason code.</param>
		public CharacterCreationResponse(CharacterCreationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterCreationResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the CharacterCreationResponseCode enum.");
			if(resultCode == CharacterCreationResponseCode.Success) throw new ArgumentException($"Provided {nameof(CharacterCreationResponseCode)} must not be success without generated section id.");

			ResultCode = resultCode;
		}
	}
}
