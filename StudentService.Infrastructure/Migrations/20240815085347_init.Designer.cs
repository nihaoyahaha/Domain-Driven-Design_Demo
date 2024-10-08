﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentService.Infrastructure;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    [Migration("20240815085347_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StudentService.Domain.Entities.Grade", b =>
                {
                    b.Property<string>("GradeId")
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("varchar")
                        .HasComment("年级名称");

                    b.HasKey("GradeId");

                    b.ToTable("T_Grades", (string)null);
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Section", b =>
                {
                    b.Property<string>("GradeId")
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("varchar")
                        .HasComment("班级名称");

                    b.Property<string>("SectionId")
                        .HasColumnType("text");

                    b.HasKey("GradeId");

                    b.HasIndex("Name");

                    b.ToTable("T_Sections", (string)null);
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasMaxLength(8)
                        .IsUnicode(true)
                        .HasColumnType("varchar")
                        .HasComment("学号");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp")
                        .HasComment("出生日期");

                    b.Property<string>("GradeId")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("varchar")
                        .HasComment("学生姓名");

                    b.Property<string>("SectionId")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.HasKey("StudentId");

                    b.HasIndex("GradeId");

                    b.HasIndex("SectionId");

                    b.HasIndex("Name", "SectionId");

                    b.ToTable("T_Students", (string)null);
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Section", b =>
                {
                    b.HasOne("StudentService.Domain.Entities.Grade", "Grade")
                        .WithMany("Sections")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Student", b =>
                {
                    b.HasOne("StudentService.Domain.Entities.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentService.Domain.Entities.Section", "Section")
                        .WithMany("Students")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Grade", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("StudentService.Domain.Entities.Section", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
