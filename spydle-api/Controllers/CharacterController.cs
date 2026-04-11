using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spydle_api.Data;
using spydle_api.DTOs;
using spydle_api.Mappers;
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
        public async Task<IActionResult> GetEyeColors()
        {
            var eyeColors = _context.CharacterEyeColors.Select(c => c.ToEyeColorDTO()).ToListAsync();
            return Ok(eyeColors);
        }

        [HttpGet("SkinColors")]
        public async Task<IActionResult> GetSkinColors()
        {
            var skinColors = _context.CharacterSkinColors.Select(c => c.ToSkinColorDTO()).ToListAsync();
            return Ok(skinColors);
        }

        [HttpPost("CheckSuspects")]
        public async Task<IActionResult> CheckSuspectsForTraits([FromBody] SuspectsDTO suspectsDTO)
        {
            if(CaseController.TodaysCase == null)
            {
                return BadRequest(new { error = "Today's case hasn't been generated yet." });
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
