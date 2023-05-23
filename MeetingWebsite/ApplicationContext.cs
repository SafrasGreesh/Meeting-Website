using MeetingWebsite.Entity;
using Microsoft.EntityFrameworkCore;

namespace MeetingWebsite.Services
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Matches> Matches { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Entity.Thread> Thread { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MeetingWebsite;Username=admin;Password=12345");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(m => m.Thread)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.ThreadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            //base.OnModelCreating(builder);
        }
    }
}


