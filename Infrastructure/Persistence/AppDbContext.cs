using CampusAnnouncementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusAnnouncementApp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Announcement> Announcements { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
