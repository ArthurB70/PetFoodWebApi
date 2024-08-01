using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext (DbContextOptions dbContextOptions) 
    : base(dbContextOptions){
    }
    
    public DbSet<User> User {get; set;}
    public DbSet<Hardware> Hardware {get; set;}
    public DbSet<Pet> Pet {get; set;}
    public DbSet<Allergy> Allergy {get; set;}
    public DbSet<Vaccine> Vaccine {get; set;}
    public DbSet<Medicine> Medicine {get; set;}
    public DbSet<Comment> Comment {get; set;}
    public DbSet<Reminder> Reminder {get; set;}
}
