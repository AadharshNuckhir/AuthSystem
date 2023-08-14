using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthSystem.Data;

public class AutSystemContext : IdentityDbContext<AuthSystemUser>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<AuthorGenre> AuthorGenres { get; set; }
    public DbSet<AgentGenre> AgentGenres { get; set; }
    public DbSet<PublisherGenre> PublisherGenres { get; set; }

    public AutSystemContext(DbContextOptions<AutSystemContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.PublishedBooks)
            .HasForeignKey(b => b.PublisherId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AuthorGenre>()
            .HasKey(ag => new { ag.AuthorId, ag.GenreId });

        modelBuilder.Entity<AuthorGenre>()
            .HasOne(ag => ag.Author)
            .WithMany(a => a.WritingGenres)
            .HasForeignKey(ag => ag.AuthorId);

        modelBuilder.Entity<AuthorGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany(g => g.InterestedAuthors)
            .HasForeignKey(ag => ag.GenreId);

        modelBuilder.Entity<AgentGenre>()
            .HasKey(ag => new { ag.AgentId, ag.GenreId });

        modelBuilder.Entity<AgentGenre>()
            .HasOne(ag => ag.Agent)
            .WithMany(a => a.InterestedGenres)
            .HasForeignKey(ag => ag.AgentId);

        modelBuilder.Entity<AgentGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany(g => g.InterestedAgents)
            .HasForeignKey(ag => ag.GenreId);

        modelBuilder.Entity<PublisherGenre>()
            .HasKey(pg => new { pg.PublisherId, pg.GenreId });

        modelBuilder.Entity<PublisherGenre>()
            .HasOne(pg => pg.Publisher)
            .WithMany(p => p.InterestedGenres)
            .HasForeignKey(pg => pg.PublisherId);

        modelBuilder.Entity<PublisherGenre>()
            .HasOne(pg => pg.Genre)
            .WithMany(g => g.InterestedPublishers)
            .HasForeignKey(pg => pg.GenreId);
    }
}
