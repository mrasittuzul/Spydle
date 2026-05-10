using Microsoft.AspNetCore.Authorization;
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
    public class CaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public static Case? TodaysCase { get; private set; }

        public CaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Today")]
        [Authorize]
        public async Task<IActionResult> GetTodaysCase()
        {
            await PrepareTodaysCase();
            return Ok(TodaysCase.ToCaseDTO());
        }

        private async Task<Case> PrepareTodaysCase()
        {
            if (TodaysCase == null || TodaysCase.Date != DateTime.UtcNow.Date)
            {
                TodaysCase = await _context.Cases.FindAsync(DateTime.UtcNow.Date);
                if (TodaysCase == null)
                {
                    int rngSeed = int.Parse(DateTime.UtcNow.Date.ToString("yyyyMMdd"));
                    Random random = new Random(rngSeed);
                    List<Character> characters = await _context.Characters.Where((c) => c.IsActive).ToListAsync();
                    TodaysCase = new Case
                    {
                        Date = DateTime.UtcNow.Date,
                        InterrogationCount = 5,
                        RngSeed = rngSeed,
                        SpyCharacterCode = characters[random.Next(0, characters.Count)].Code,
                        IncludedCharacters = characters.Select(ch =>  ch.Code).ToArray()
                    };
                    await _context.Cases.AddAsync(TodaysCase);
                    await _context.SaveChangesAsync();
                }
            }
            return TodaysCase;
        }
    }
}
