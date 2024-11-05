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

        builder.HasIndex(x=>new {x.Name});
        builder.Property(x=>x.Name)
        .IsUnicode()
        .HasComment("班级名称")
        .HasColumnType("varchar(20)")
        .IsRequired();

        builder.HasMany(x => x.Students)
        .WithOne(x=>x.Section)
        .HasForeignKey(x=>x.SectionId)
        .IsRequired();
    }

}
