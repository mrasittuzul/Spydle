using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spydle_api.Data;
using spydle_api.DTOs;
using spydle_api.Entities;
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

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
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
            if(CaseController.TodaysCase == null)
            {
                return BadRequest(new ErrorContainer("Today's case hasn't been generated yet."));
            }

            string userId = HttpContext.User.Claims.First(c => c.Type == "userId").Value;
            var user = await _context.Users.Include(u => u.Interrogations).FirstOrDefaultAsync(u => u.Id == userId);
            int interrogationCount = user.Interrogations.Where(i => i.CaseDate == CaseController.TodaysCase.Date).Count();
            if (interrogationCount == 6)
            {
                return BadRequest(new ErrorContainer("You don't have any interrogations left."));
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
    }
}
