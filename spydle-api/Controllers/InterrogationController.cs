using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spydle_api.Data;
using spydle_api.DTOs;
using spydle_api.Interfaces;
using spydle_api.Models;
using spydle_api.Mappers;
using spydle_api.Entities;

namespace spydle_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterrogationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public InterrogationController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("GetInterrogationsForTodaysCase")]
        [Authorize]
        public async Task<IActionResult> GetInterrogationsForTodaysCase()
        {
            User user = await _tokenService.GetUserFromHTTPContext(HttpContext);
            List<Interrogation> interrogations = user.Interrogations.Where(i => i.CaseDate == CaseController.TodaysCase.Date).ToList();

            InterrogationsForTodaysCaseDTO interrogationsForToday = new InterrogationsForTodaysCaseDTO() { Interrogations = new InterrogationDTO[interrogations.Count()] };
            for (int i = 0; i < interrogations.Count(); i++)
            {
                interrogationsForToday.Interrogations[i] = interrogations[i].ToDTO();
            }
            return Ok(interrogationsForToday);
        }
    }
}