using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentService.Infrastructure;

public class MyDesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
{
    public StudentDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<StudentDbContext> builder = new();
		//string connStr = "Host=localhost;Database=Student;Username=postgres;Persist Security Info=True;Password=postgre123456";
		string connStr = "Host=nihaoyahaha.top;Port=5433;Database=Student;Username=postgres;Password=gyg;Persist Security Info=True";
		builder.UseNpgsql(connStr);
        return new StudentDbContext(builder.Options);
    }
}
