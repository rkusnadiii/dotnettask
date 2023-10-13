using examplemvc.Models;
using examplemvc.Models.Request;
using Microsoft.EntityFrameworkCore;


public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
