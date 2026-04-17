using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spydle_api.Data;
using spydle_api.DTOs;
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

            for (int i = 0; i < suspectsDTO.SuspectCodes.Length; i++)
            {
                if (CharacterService.AreCharactersSharingTraits(suspectsDTO.SuspectCodes[i], CaseController.TodaysCase.SpyCharacterCode))
                {
                    return Ok(new { result = true });
                }
            }
            return Ok(new { result = false });
        }
    }
}
