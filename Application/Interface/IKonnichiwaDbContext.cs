using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interface;
public interface IKonnichiwaDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> post { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Likes> Like { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
}
