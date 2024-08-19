using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure.Configs;

public class GradeConfig : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("T_Grades");
        builder.HasIndex(x => x.Name);
        builder.HasKey(x => x.GradeId);
        builder.Property(x => x.GradeId)
        .HasColumnType("varchar(8)")
        .HasComment("年级ID");

        builder.Property(x => x.Name)
        .IsUnicode()
        .HasColumnType("varchar(20)")
        .HasComment("年级名称");
    }

}
