using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;

namespace StudentService.Infrastructure.Configs;

public class RoleConfig : IEntityTypeConfiguration<StudentService.Domain.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role> builder)
    {
         builder.ToTable("T_Roles");
    }
}
