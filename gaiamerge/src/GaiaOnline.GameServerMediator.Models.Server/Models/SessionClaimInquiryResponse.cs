using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HaloLive.Models;
using Newtonsoft.Json;

namespace GaiaOnline
{
	[JsonObject]
	public sealed class SessionClaimInquiryResponse : IResponseModel<SessionClaimInquiryResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public SessionClaimInquiryResponseCode ResultCode { get; private set; }
		
		//Could be invalid if the response fails.

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == SessionClaimInquiryResponseCode.Success;

		/// <summary>
		/// Represents the user id associated with the session.
		/// This value will be invalid if the <see cref="ResultCode"/> isn't success.
		/// </summary>
		[JsonProperty]
		public int UserId { get; private set; }

		/// <summary>
		/// Creates a failed <see cref="SessionClaimInquiryResponse"/>.
		/// </summary>
		/// <param name="resultCode">A valid response code that isn't <see cref="SessionClaimInquiryResponseCode"/>.Success</param>
		public SessionClaimInquiryResponse(SessionClaimInquiryResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(SessionClaimInquiryResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the SessionClaimInquiryResponseCode enum.");
			if(resultCode == SessionClaimInquiryResponseCode.Success) throw new ArgumentException($"Provided {nameof(SessionClaimInquiryResponseCode)} was invalid. It cannot be success if there is no provided {nameof(UserId)}");

			ResultCode = resultCode;
		}

		protected SessionClaimInquiryResponse()
		{
			
		}
	}
}
