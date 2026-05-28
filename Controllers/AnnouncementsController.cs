using CampusAnnouncementApp.Application.Factories;
using CampusAnnouncementApp.Application.Services;
using CampusAnnouncementApp.Domain.Enums;
using CampusAnnouncementApp.Infrastructure.Observers;
using CampusAnnouncementApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusAnnouncementApp.Controllers;

[ApiController]
[Route("api/announcements")]
public class AnnouncementsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AnnouncementsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcements = await _context.Announcements.ToListAsync();
        return Ok(announcements);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
    {
        var announcement = AnnouncementFactory.Create(request.Type, request.Title, request.Content);

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        var publisher = new AnnouncementPublisher();
        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            if (user.UserType == UserType.Ogrenci)
                publisher.Subscribe(new StudentObserver(user));
            else
                publisher.Subscribe(new TeacherObserver(user));
        }

        publisher.Notify(announcement);

        return CreatedAtAction(nameof(GetAll), new { id = announcement.Id }, announcement);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement is null) return NotFound();

        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

public record CreateAnnouncementRequest(string Title, string Content, AnnouncementType Type);
