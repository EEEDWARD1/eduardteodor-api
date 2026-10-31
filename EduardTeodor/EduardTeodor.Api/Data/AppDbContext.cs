using EduardTeodor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardTeodor.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id);

            entity.HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity("post_tags");
        });
        
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(t => t.Name).IsUnique();
        });
    }
}
