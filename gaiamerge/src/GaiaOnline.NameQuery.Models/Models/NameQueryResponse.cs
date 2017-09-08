using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Models;
using Newtonsoft.Json;

namespace GaiaOnline
{
	/// <summary>
	/// DTO model that represents tha result of a name query.
	/// </summary>
	[JsonObject]
	public sealed class NameQueryResponse : IResponseModel<NameQueryResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public NameQueryResponseCode ResultCode { get; private set; }

		/// <summary>
		/// The name of the avatar the query resulted in.
		/// Will be null if <see cref="ResultCode"/> indicates failure.
		/// </summary>
		[JsonProperty]
		public string AvatarName { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == NameQueryResponseCode.Success;

		public NameQueryResponse(string avatarName)
		{
			if(string.IsNullOrWhiteSpace(avatarName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(avatarName));

			ResultCode = NameQueryResponseCode.Success;
			AvatarName = avatarName;
		}

		/// <summary>
		/// Creates a failed response. 
		/// </summary>
		/// <param name="resultCode">Should be the result code other than success. Will throw on success.</param>
		public NameQueryResponse(NameQueryResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(NameQueryResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the NameQueryResponseCode enum.");
			if(resultCode == NameQueryResponseCode.Success) throw new InvalidOperationException($"Cannot create a {nameof(NameQueryResponse)} without a success code without the result.");

			ResultCode = resultCode;
		}

		private NameQueryResponse()
		{
			
		}
	}
}
