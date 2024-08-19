using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentService.Infrastructure;

public class MyDesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
{
    public StudentDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<StudentDbContext> builder = new();
        string connStr = "Host=localhost;Database=Student;Username=postgres;Persist Security Info=True;Password=postgre123456";
        builder.UseNpgsql(connStr);
        return new StudentDbContext(builder.Options);
    }
}
