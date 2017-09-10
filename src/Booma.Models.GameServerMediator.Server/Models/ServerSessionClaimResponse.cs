using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HaloLive.Models;
using Newtonsoft.Json;

namespace Booma
{
	[JsonObject]
	public sealed class ServerSessionClaimResponse : IResponseModel<ServerSessionClaimResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public ServerSessionClaimResponseCode ResultCode { get; private set; }
		
		//Could be invalid if the response fails.

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ServerSessionClaimResponseCode.Success;

		/// <summary>
		/// Represents the user id associated with the session.
		/// This value will be invalid if the <see cref="ResultCode"/> isn't success.
		/// </summary>
		[JsonProperty]
		public int UserId { get; private set; }

		/// <summary>
		/// Creates a failed <see cref="ServerSessionClaimResponse"/>.
		/// </summary>
		/// <param name="resultCode">A valid response code that isn't <see cref="SessionClaimInquiryResponseCode"/>.Success</param>
		public ServerSessionClaimResponse(ServerSessionClaimResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ServerSessionClaimResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the SessionClaimInquiryResponseCode enum.");
			if(resultCode == ServerSessionClaimResponseCode.Success) throw new ArgumentException($"Provided {nameof(ServerSessionClaimResponseCode)} was invalid. It cannot be success if there is no provided {nameof(UserId)}");

			ResultCode = resultCode;
		}

		protected ServerSessionClaimResponse()
		{
			
		}
	}
}
