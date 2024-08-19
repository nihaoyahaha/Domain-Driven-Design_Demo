using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentService.Domain.Entities;

namespace StudentService.Infrastructure.Configs;

public class SectionConfig : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("T_Sections");
        builder.HasKey(x => x.SectionId);
        builder.Property(x => x.SectionId)
        .HasMaxLength(8)
        .HasColumnType("varchar");

        builder.HasIndex(x=>new {x.Name});
        builder.Property(x=>x.Name)
        .IsUnicode()
        .HasComment("班级名称")
        .HasColumnType("varchar(20)")
        .IsRequired();

        builder.Property(x=>x.GradeId)
        .HasColumnType("varchar(8)")
        .HasComment("班级Id(外键)");

        builder.HasOne(x => x.Grade)
        .WithMany(x=>x.Sections)
        .HasForeignKey(x=>x.GradeId);
    }

}
