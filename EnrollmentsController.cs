using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.DTOs;
using RaceDayAPI.Models;
using System.Security.Claims;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly Data.ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Participant")]
        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollDto dto)
        {
            var enrollment = new Enrollment
            {
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                EventId = dto.EventId,
                CategoryId = dto.CategoryId,
                Status = "Confirmed"
            };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Created("", enrollment);
        }

        [Authorize(Roles = "Organiser")]
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEnrollments(int eventId)
        {
            var enrollments = await _context.Enrollments.Where(e => e.EventId == eventId).ToListAsync();
            return Ok(enrollments);
        }
    }

}
