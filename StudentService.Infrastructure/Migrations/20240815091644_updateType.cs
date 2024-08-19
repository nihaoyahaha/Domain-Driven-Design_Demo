using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Students",
                type: "varchar(20)",
                nullable: false,
                comment: "学生姓名",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 20,
                oldComment: "学生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Students",
                type: "varchar(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "T_Students",
                type: "varchar(8)",
                nullable: false,
                comment: "学号",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 8,
                oldComment: "学号");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Sections",
                type: "varchar(20)",
                nullable: false,
                comment: "班级名称",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 20,
                oldComment: "班级名称");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Sections",
                type: "varchar(8)",
                nullable: false,
                comment: "班级Id(外键)",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldComment: "班级Id(外键)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Grades",
                type: "varchar(20)",
                nullable: false,
                comment: "年级名称",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 20,
                oldComment: "年级名称");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Grades",
                type: "varchar(8)",
                nullable: false,
                comment: "年级ID",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 8,
                oldComment: "年级ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Students",
                type: "varchar",
                maxLength: 20,
                nullable: false,
                comment: "学生姓名",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldComment: "学生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Students",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "T_Students",
                type: "varchar",
                maxLength: 8,
                nullable: false,
                comment: "学号",
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldComment: "学号");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Sections",
                type: "varchar",
                maxLength: 20,
                nullable: false,
                comment: "班级名称",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldComment: "班级名称");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Sections",
                type: "varchar",
                nullable: false,
                comment: "班级Id(外键)",
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldComment: "班级Id(外键)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Grades",
                type: "varchar",
                maxLength: 20,
                nullable: false,
                comment: "年级名称",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldComment: "年级名称");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "T_Grades",
                type: "varchar",
                maxLength: 8,
                nullable: false,
                comment: "年级ID",
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldComment: "年级ID");
        }
    }
}
