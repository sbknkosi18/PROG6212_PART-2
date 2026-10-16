using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.DTOs;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/results")]
    public class ResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Organiser")]
        [HttpPost]
        public async Task<IActionResult> CaptureResult(ResultDto dto)
        {
            var result = new Result
            {
                EnrollmentId = dto.EnrollmentId,
                Time = dto.Time,
                Position = dto.Position
            };
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return Created("", result);
        }

        [Authorize(Roles = "Participant")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResults(int userId)
        {
            var results = await _context.Results
                .Include(r => r.Enrollment)
                .Where(r => r.Enrollment.UserId == userId)
                .ToListAsync();
            return Ok(results);
        }
    }

}
