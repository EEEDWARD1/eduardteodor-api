using EduardTeodor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardTeodor.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.ToTable(b => b.HasCheckConstraint(
                "CK_Post_Type",
                "\"type\" IN ('project', 'thought')"
                ));

            entity.OwnsMany(p => p.Tags, b => b.ToJson());
        });
    }
}
