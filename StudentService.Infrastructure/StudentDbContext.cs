using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure;

public class StudentDbContext:IdentityDbContext<User,StudentService.Domain.Role,Guid>
{
    public DbSet<Student> students{ get;private set; }
    public DbSet<Section> sections{ get;private set; }
	public DbSet<Grade> grades { get; private set; }

	public StudentDbContext(DbContextOptions options) : base(options)
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
