using System.Security.Cryptography.X509Certificates;
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;
public class KonnichiwaDbContext : DbContext, IKonnichiwaDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> post { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Likes> Like { get; set; }

    public KonnichiwaDbContext(DbContextOptions<KonnichiwaDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);
        base.OnModelCreating(modelBuilder);
    }

    public int SaveChanges()
    {
        return base.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
