using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure.Configs;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
       builder.ToTable("T_Students");
       builder.HasKey(x => x.StudentId);

       builder.HasIndex(x=> new { x.Name });

       builder.Property(x=>x.StudentId)
       .IsUnicode()
       .HasComment("学号");

       builder.Property(x=>x.Name)
       .HasComment("学生姓名")
       .IsUnicode()
       .HasColumnType("varchar(20)")
       .IsRequired();

       builder.Property(x=>x.Birthday)
       .HasComment("出生日期")
       .HasColumnType("timestamp");

        builder.Property(x => x.Gender)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasConversion<string>()
            .HasComment("性别");

    }

}
