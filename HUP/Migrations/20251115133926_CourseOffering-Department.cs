using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class CourseOfferingDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_DepartmentId",
                table: "CourseOfferings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_DepartmentId1",
                table: "CourseOfferings",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId",
                table: "CourseOfferings",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId1",
                table: "CourseOfferings",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId1",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_DepartmentId",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_DepartmentId1",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "CourseOfferings");
        }
    }
}
