using Microsoft.EntityFrameworkCore;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure;

public class StudentDbContext:DbContext
{
    public DbSet<Student> students{ get;private set; }
    public DbSet<Section> sections{ get;private set; }
    public DbSet<Grade> grades{ get;private set; }

    public StudentDbContext(DbContextOptions options):base(options)   
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
