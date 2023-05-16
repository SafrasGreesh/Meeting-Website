using MeetingWebsite.Entity;
using Microsoft.EntityFrameworkCore;

namespace MeetingWebsite.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Dislikes> Dislikes { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Events> Events { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MeetingWebsite;Username=postgres;Password=987321");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}


