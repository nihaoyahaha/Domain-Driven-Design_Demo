using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Students_T_Sections_SectionId",
                table: "T_Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Sections",
                table: "T_Sections");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "T_Students",
                type: "varchar",
                maxLength: 8,
                nullable: false,
                comment: "班级ID(外键)",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "T_Sections",
                type: "varchar",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Sections",
                type: "varchar",
                nullable: false,
                comment: "班级Id(外键)",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Grades",
                type: "varchar",
                maxLength: 8,
                nullable: false,
                comment: "年级ID",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Sections",
                table: "T_Sections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Sections_GradeId",
                table: "T_Sections",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Students_T_Sections_SectionId",
                table: "T_Students",
                column: "SectionId",
                principalTable: "T_Sections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Students_T_Sections_SectionId",
                table: "T_Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Sections",
                table: "T_Sections");

            migrationBuilder.DropIndex(
                name: "IX_T_Sections_GradeId",
                table: "T_Sections");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "T_Students",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 8,
                oldComment: "班级ID(外键)");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Sections",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldComment: "班级Id(外键)");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "T_Sections",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Grades",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 8,
                oldComment: "年级ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Sections",
                table: "T_Sections",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Students_T_Sections_SectionId",
                table: "T_Students",
                column: "SectionId",
                principalTable: "T_Sections",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
