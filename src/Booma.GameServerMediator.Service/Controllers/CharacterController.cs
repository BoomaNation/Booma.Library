using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HaloLive.Hosting;
using HaloLive.Hosting.Authorization;
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

		/// <summary>
		/// Queries for the name of a character based on the provided <see cref="id"/>.
		/// </summary>
		/// <param name="id">The character id of the character.</param>
		/// <param name="characterRepository">Character repository service.</param>
		/// <returns>A <see cref="NameQueryResponse"/> or 422 code if invalid.</returns>
		[HttpGet("name/{id}")]
		public async Task<IActionResult> NameQuery(int id, [FromServices] IReadonlyCharacterRepository characterRepository)
		{
			if(characterRepository == null) throw new ArgumentNullException(nameof(characterRepository));

			if(id < 0)
				return StatusCode(422); //Unprocessable Entity

			string name = await characterRepository.GetCharacterName(id);

			//We should assume it's unknown. There could be other issues but the user does not need to know this.
			if(string.IsNullOrWhiteSpace(name))
				return Json(new NameQueryResponse(NameQueryResponseCode.UnknownUserId));

			return Json(new NameQueryResponse(name));
		}

		/// <summary>
		/// Loads and returns the character list for the authorized user.
		/// If the user isn't authorized then we both don't know who they're interested in nor
		/// would we provided the result if we did.
		/// </summary>
		/// <returns>Returns the character list if authorized.</returns>
		[Authorize] //We must authorize to protect user's privacies.
		[HttpGet("list")]
		public async Task<JsonResult> GetCharacterList([FromServices] IReadonlyCharacterRepository characterRepository)
		{
			if(characterRepository == null) throw new ArgumentNullException(nameof(characterRepository));

			//The account id should be in the JWT/claim sent. It's required to load the characters from the database
			//that are associated with the account.
			int accountId = HaloLiveUserManager.GetUserIdInt(User);

			//We don't load additional uneeded information. The Ids are enough for the client to load their names, profiles and appearance if required.
			int[] characterIds = (await characterRepository.LoadAssociatedCharacterIds(accountId)).ToArray();
			characterIds = characterIds ?? Enumerable.Empty<int>().ToArray();

			//We don't need to do anything fancy. The ID of the characters is TRULY enough for the client to then request and piece together all the other missing content
			return Json(new CharacterListResponse(characterIds));
		}

		[Authorize] //We must authorize to prevent users from creating characters for other accounts and to identify WHO is creating a character
		[HttpPost("create")]
		public async Task<JsonResult> CreateCharacter([FromBody] CharacterCreationRequest request, [FromServices] ICharacterRepository characterRepository)
		{
			if(characterRepository == null) throw new ArgumentNullException(nameof(characterRepository));

			//TODO: Return error
			if(!ModelState.IsValid)
				return Json(new CharacterCreationResponse(CharacterCreationResponseCode.GeneralServerError));

			//The account id should be in the JWT/claim sent. It's required to load the characters from the database
			//that are associated with the account.
			int accountId = HaloLiveUserManager.GetUserIdInt(User);

			//TODO: We should check banned names list
			//We must check the name first because there could be a duplicate
			bool nameIsTaken = await characterRepository.DoesNameExist(request.CharacterName);

			if(nameIsTaken)
				return Json(new CharacterCreationResponse(CharacterCreationResponseCode.NameInvalid));

			//TODO: Enforce a maximum account of characters per account
			CharacterCreationResult result = await characterRepository.TryCreateNewCharacter(accountId, Request.HttpContext.Connection.RemoteIpAddress.ToString(), 
				new CharacterCreationInformation(request.CharacterName, request.CharacterClass));

			//This means that the character was created and was assigned a section id
			if(result.IsSuccessful)
				return Json(new CharacterCreationResponse(result.CharacterSectionId));

			//We don't know what went wrong here, something did. This could be due to a race condition. It should so rarely occur that it is most likely just a major server
			//issue instead.
			return Json(new CharacterCreationResponse(CharacterCreationResponseCode.GeneralServerError));
		}
		
		//This method doesn't require authorize because the name availability isn't secret and doesn't depend on identity.
		/// <summary>
		/// Controller action that takes a <see cref="CharacterNameValidationRequest"/> and validates
		/// the contained username to determine if it's valid
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet("namecheck")]
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
