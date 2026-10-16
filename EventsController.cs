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
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Organiser")]
    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventDto dto)
    {
            var ev = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            Location = dto.Location,
            Distance = dto.Distance,
            EventType = dto.EventType,
            OrganiserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
        };
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvent), new { id = ev.EventId }, ev);
    }

    [Authorize(Roles = "Organiser")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, EventDto dto)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null) return NotFound();

        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.Date = dto.Date;
        ev.Location = dto.Location;
        ev.Distance = dto.Distance;
        ev.EventType = dto.EventType;

        await _context.SaveChangesAsync();
        return Ok(ev);
    }

    [Authorize(Roles = "Organiser")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null) return NotFound();

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        return Ok(events);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null) return NotFound();
        return Ok(ev);
    }
}

}
