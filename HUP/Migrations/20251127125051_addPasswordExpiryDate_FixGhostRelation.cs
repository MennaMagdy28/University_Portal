using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class addPasswordExpiryDate_FixGhostRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId1",
                table: "ProgramPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.DropIndex(
                name: "IX_ProgramPlan_DepartmentId1",
                table: "ProgramPlan");

            migrationBuilder.DropIndex(
                name: "IX_ProgramPlan_DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordExpiryDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordExpiryDate",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "ProgramPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId2",
                table: "ProgramPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPlan_DepartmentId1",
                table: "ProgramPlan",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPlan_DepartmentId2",
                table: "ProgramPlan",
                column: "DepartmentId2");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId1",
                table: "ProgramPlan",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId2",
                table: "ProgramPlan",
                column: "DepartmentId2",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
