using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gradeaddindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_T_Grades_Name",
                table: "T_Grades",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_Grades_Name",
                table: "T_Grades");
        }
    }
}
