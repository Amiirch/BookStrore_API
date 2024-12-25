using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;


namespace BookStoreApi.Data;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
        
        
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
           
        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.Email, u.UserName, u.phoneNumber })
            .IsUnique(true);
        
        modelBuilder.Entity<Book>()
            .HasIndex(u => u.Name).IsUnique(true);
    }
}
