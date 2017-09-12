using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booma
{
	/// <summary>
	/// Controller for character actions/requests.
	/// </summary>
	[Route("api/[controller]")]
	public class CharacterController : AuthorizationReadyController
	{
		protected CharacterController([FromServices] IClaimsPrincipalReader haloLiveUserManager) 
			: base(haloLiveUserManager)
		{

		}
		
		//This method doesn't require authorize because the name availability isn't secret and doesn't depend on identity.
		/// <summary>
		/// Controller action that takes a <see cref="CharacterNameValidationRequest"/> and validates
		/// the contained username to determine if it's valid
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("check")]
		public async Task<JsonResult> CheckCharacterNameAvailability([FromBody] CharacterNameValidationRequest request, [FromServices] IReadonlyCharacterRepository characterRepository)
		{
			if(!ModelState.IsValid)
				return GenerateValidationResponse(CharacterNameValidationResponseCode.GeneralServerError);

			if(String.IsNullOrWhiteSpace(request.CharacterName))
				return GenerateValidationResponse(CharacterNameValidationResponseCode.NameContainsInvalidCharacters);

			if(!CharacterNameValidator.isNameValidLength(request.CharacterName))
				return GenerateValidationResponse(CharacterNameValidationResponseCode.NameIsInvalidLength);

			if(!CharacterNameValidator.isNameValidCharacters(request.CharacterName))
				return GenerateValidationResponse(CharacterNameValidationResponseCode.NameContainsInvalidCharacters);

			bool isNameTaken;

			//We wrap this in a try because something could be broken db/network wise. We just want to tell the user that something broke instead of
			//breaking their client with malformed responses too
			try
			{
				//At this point the name looks valid but we still don't know if we have another character with that name.
				isNameTaken = await characterRepository.DoesNameExist(request.CharacterName);
			}
			catch(Exception e)
			{
				//TODO: Logging
				return GenerateValidationResponse(CharacterNameValidationResponseCode.GeneralServerError);
			}	

			return !isNameTaken ? GenerateValidationResponse(CharacterNameValidationResponseCode.Success) 
				: GenerateValidationResponse(CharacterNameValidationResponseCode.NameAlreadyTaken);
		}

		private JsonResult GenerateValidationResponse(CharacterNameValidationResponseCode code)
		{
			if(!Enum.IsDefined(typeof(CharacterNameValidationResponseCode), code)) throw new ArgumentOutOfRangeException(nameof(code), "Value should be defined in the CharacterNameValidationResponseCode enum.");

			return Json(new CharacterNameValidationResponse(code));
		}
	}
}
