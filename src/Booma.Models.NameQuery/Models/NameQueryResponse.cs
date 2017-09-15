using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Models;
using Newtonsoft.Json;

namespace Booma
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
		/// The name of the object.
		/// Will be null if <see cref="ResultCode"/> indicates failure.
		/// </summary>
		[JsonProperty]
		public string Name { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == NameQueryResponseCode.Success;

		/// <summary>
		/// Creates a successful response with <see cref="ResultCode"/> set to success
		/// with the provided <see cref="Name"/>.
		/// </summary>
		/// <param name="name">The name.</param>
		public NameQueryResponse(string name)
		{
			if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

			ResultCode = NameQueryResponseCode.Success;
			Name = name;
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
