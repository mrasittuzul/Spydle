using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spydle_api.Data;
using spydle_api.DTOs;
using spydle_api.Entities;
using spydle_api.Interfaces;
using spydle_api.Mappers;
using spydle_api.Models;
using spydle_api.Services;

namespace spydle_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public CharacterController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("EyeColors")]
        [Produces<EyeColorDTO[]>]
        [Authorize]
        public async Task<IActionResult> GetEyeColors()
        {
            var eyeColors = await _context.CharacterEyeColors.Select(c => c.ToEyeColorDTO()).ToArrayAsync();
            return Ok(eyeColors);
        }

        [HttpGet("SkinColors")]
        [Produces<SkinColorDTO[]>]
        [Authorize]
        public async Task<IActionResult> GetSkinColors()
        {
            var skinColors = await _context.CharacterSkinColors.Select(c => c.ToSkinColorDTO()).ToArrayAsync();
            return Ok(skinColors);
        }

        [HttpPost("CheckSuspects")]
        [Authorize]
        public async Task<IActionResult> CheckSuspectsForTraits([FromBody] SuspectsDTO suspectsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorContainer(ModelState.ToErrorCollection()));
            }
            if(CaseController.TodaysCase == null)
            {
                return BadRequest(new ErrorContainer("Today's case hasn't been generated yet."));
            }

            User user = await _tokenService.GetUserFromHTTPContext(HttpContext);
            int interrogationCount = user.Interrogations.Where(i => i.CaseDate == CaseController.TodaysCase.Date).Count();
            if (interrogationCount == CaseController.TodaysCase.InterrogationCount)
            {
                return BadRequest(new ErrorContainer("You don't have any interrogations left."));
            }
            if ((interrogationCount < CaseController.TodaysCase.InterrogationCount - 1 && suspectsDTO.SuspectCodes.Length > CaseController.TodaysCase.RegularInterrogationSuspectCount)
                || (interrogationCount == CaseController.TodaysCase.InterrogationCount - 1 && suspectsDTO.SuspectCodes.Length > CaseController.TodaysCase.FinalInterrogationSuspectCount))
            {
                return BadRequest(new ErrorContainer("Interrogation violates allowed suspect count."));
            }

            bool isMatchingTraitFound = false;
            for (int i = 0; i < suspectsDTO.SuspectCodes.Length; i++)
            {
                if (CharacterService.AreCharactersSharingTraits(suspectsDTO.SuspectCodes[i], CaseController.TodaysCase.SpyCharacterCode))
                {
                    isMatchingTraitFound = true;
                    break;
                }
            }

            Interrogation interrogation = new Interrogation
            {
                CaseDate = CaseController.TodaysCase.Date,
                FoundMatchingTrait = isMatchingTraitFound,
                Suspects = suspectsDTO.SuspectCodes,
                UserId = user.Id
            };
            await _context.Interrogations.AddAsync(interrogation);
            await _context.SaveChangesAsync();

            return Ok(new InterrogationResultDTO { IsMatchFound = isMatchingTraitFound, InterrogationNumber = interrogationCount + 1 });
        }

        [HttpGet("GetSpy")]
        [Authorize]
        public async Task<IActionResult> GetSpy()
        {
            User user = await _tokenService.GetUserFromHTTPContext(HttpContext);
            int interrogationCount = user.Interrogations.Where(i => i.CaseDate == CaseController.TodaysCase.Date).Count();
            if (interrogationCount < CaseController.TodaysCase.InterrogationCount)
            {
                return BadRequest(new ErrorContainer("You haven't performed enough interrogations to get the identity of the spy."));
            }
            return Ok(new SpyDTO { SpyCharacterCode = CaseController.TodaysCase.SpyCharacterCode });
        }
    }
}
