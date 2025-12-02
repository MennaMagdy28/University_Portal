using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class GhostFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId1",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId1",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_CourseOfferings_CourseOfferingId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CourseOfferingId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseId1",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_DepartmentId1",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CourseWorkGrade",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PercentGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "HeadOfDepartment",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "CourseOfferings");

            migrationBuilder.AddColumn<Guid>(
                name: "HeadOfDepartmentId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "ClassGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "MidtermGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "finalGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "HeadOfDepartmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HeadOfDepartmentId",
                table: "Users",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseOfferingId",
                table: "Enrollments",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadOfDepartmentId",
                table: "Departments",
                column: "HeadOfDepartmentId",
                unique: true,
                filter: "[HeadOfDepartmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_HeadOfDepartmentId",
                table: "Departments",
                column: "HeadOfDepartmentId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId",
                table: "Enrollments",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_HeadOfDepartmentId",
                table: "Users",
                column: "HeadOfDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_HeadOfDepartmentId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_HeadOfDepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HeadOfDepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseOfferingId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadOfDepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HeadOfDepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "MidtermGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "finalGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "HeadOfDepartmentId",
                table: "Departments");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingId1",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CourseWorkGrade",
                table: "ProgramPlan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadOfDepartment",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseOfferingId1",
                table: "Schedules",
                column: "CourseOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId1",
                table: "Enrollments",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_DepartmentId1",
                table: "CourseOfferings",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Departments_DepartmentId1",
                table: "CourseOfferings",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId1",
                table: "Enrollments",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_CourseOfferings_CourseOfferingId1",
                table: "Schedules",
                column: "CourseOfferingId1",
                principalTable: "CourseOfferings",
                principalColumn: "Id");
        }
    }
}
