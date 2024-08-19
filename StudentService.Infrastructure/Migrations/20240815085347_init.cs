using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Grades",
                columns: table => new
                {
                    GradeId = table.Column<string>(type: "varchar", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "年级名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Grades", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "T_Sections",
                columns: table => new
                {
                    GradeId = table.Column<string>(type: "varchar", nullable: false),
                    SectionId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "班级名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Sections", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_T_Sections_T_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "T_Grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "varchar", maxLength: 8, nullable: false, comment: "学号"),
                    Name = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "学生姓名"),
                    Birthday = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "出生日期"),
                    SectionId = table.Column<string>(type: "varchar", nullable: false),
                    GradeId = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_T_Students_T_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "T_Grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Students_T_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "T_Sections",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Sections_Name",
                table: "T_Sections",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_T_Students_GradeId",
                table: "T_Students",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Students_Name_SectionId",
                table: "T_Students",
                columns: new[] { "Name", "SectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_T_Students_SectionId",
                table: "T_Students",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Students");

            migrationBuilder.DropTable(
                name: "T_Sections");

            migrationBuilder.DropTable(
                name: "T_Grades");
        }
    }
}
