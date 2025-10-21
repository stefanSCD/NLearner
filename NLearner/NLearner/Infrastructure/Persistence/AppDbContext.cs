using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NLearner.Domain.Entities;

namespace NLearner.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
