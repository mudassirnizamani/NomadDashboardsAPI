using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NomadDashboardsAPI.Models;

namespace NomadDashboardsAPI.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<User>(a => a.Sender)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserID);
        }

        public DbSet<Message> Messages { get; set; }
    }
}