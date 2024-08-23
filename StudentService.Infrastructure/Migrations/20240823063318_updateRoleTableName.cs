using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateRoleTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_T_Role_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_T_Role_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Role",
                table: "T_Role");

            migrationBuilder.RenameTable(
                name: "T_Role",
                newName: "T_Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Roles",
                table: "T_Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_T_Roles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "T_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_T_Roles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "T_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_T_Roles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_T_Roles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Roles",
                table: "T_Roles");

            migrationBuilder.RenameTable(
                name: "T_Roles",
                newName: "T_Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Role",
                table: "T_Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_T_Role_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "T_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_T_Role_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "T_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
