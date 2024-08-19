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

       builder.HasIndex(x=> new {x.Name,x.SectionId});

       builder.Property(x=>x.StudentId)
       .IsUnicode()
       .HasComment("学号")
       .HasColumnType("varchar(8)");

       builder.Property(x=>x.Name)
       .HasComment("学生姓名")
       .IsUnicode()
       .HasColumnType("varchar(20)")
       .IsRequired();

       builder.Property(x=>x.Birthday)
       .HasComment("出生日期")
       .HasColumnType("timestamp");

       builder.Property(x=>x.SectionId)
       .HasMaxLength(8)
       .HasComment("班级ID(外键)");

       builder.HasOne(x=>x.Section)
       .WithMany(x => x.Students)
       .HasForeignKey(x=>x.SectionId);

       builder.HasOne(x=>x.Grade).WithMany();
    }

}
