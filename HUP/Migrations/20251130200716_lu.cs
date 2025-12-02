using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class lu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Courses_CourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSchedules_Courses_CourseID",
                table: "CourseSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId1",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CourseID",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CourseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_CourseSchedules_CourseID",
                table: "CourseSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseWorkGrade",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "PercentGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "ContactInfo_Phone");

            migrationBuilder.RenameColumn(
                name: "FullEnglishName",
                table: "Users",
                newName: "PersonalInfo_FullEnglishName");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Students",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StudentId1",
                table: "Enrollments",
                newName: "StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId1",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentUserId");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalInfo_BirthPlace",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordExpiryDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "ClassGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MidtermGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "finalGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingId",
                table: "CourseSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SemesterId",
                table: "Enrollments",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSchedules_CourseOfferingId",
                table: "CourseSchedules",
                column: "CourseOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSchedules_CourseOfferings_CourseOfferingId",
                table: "CourseSchedules",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Semesters_SemesterId",
                table: "Enrollments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentUserId",
                table: "Enrollments",
                column: "StudentUserId",
                principalTable: "Students",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSchedules_CourseOfferings_CourseOfferingId",
                table: "CourseSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Semesters_SemesterId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentUserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_SemesterId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_CourseSchedules_CourseOfferingId",
                table: "CourseSchedules");

            migrationBuilder.DropColumn(
                name: "PasswordExpiryDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "MidtermGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "finalGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId",
                table: "CourseSchedules");

            migrationBuilder.RenameColumn(
                name: "PersonalInfo_FullEnglishName",
                table: "Users",
                newName: "FullEnglishName");

            migrationBuilder.RenameColumn(
                name: "ContactInfo_Phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "FacultyID");

            migrationBuilder.RenameColumn(
                name: "StudentUserId",
                table: "Enrollments",
                newName: "StudentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentUserId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId1");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalInfo_BirthPlace",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "CourseWorkGrade",
                table: "ProgramPlan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentID",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyID",
                table: "Students",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseID",
                table: "Exams",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSchedules_CourseID",
                table: "CourseSchedules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Courses_CourseId",
                table: "Courses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSchedules_Courses_CourseID",
                table: "CourseSchedules",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId1",
                table: "Enrollments",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CourseID",
                table: "Exams",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
