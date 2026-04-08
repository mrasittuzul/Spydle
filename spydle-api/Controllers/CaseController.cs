using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spydle_api.Models;

namespace spydle_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private Case? _todaysCase;

        public CaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodaysCase()
        {
            if (_todaysCase == null)
            {
                var foundCase = await _context.Cases.FindAsync(DateTime.UtcNow.Date);
                if (foundCase == null)
                {
                    _todaysCase = new Case { Date = DateTime.UtcNow.Date, RngSeed = DateTime.UtcNow.Date.GetHashCode().ToString() };
                    await _context.Cases.AddAsync(_todaysCase);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok(_todaysCase);
        }
    }
}
