using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_ProgramPlan_DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "DepartmentId2",
                table: "ProgramPlan");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "ProgramID");

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

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramCourseId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramDepartmentId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramName",
                table: "ProgramPlan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExamDate",
                table: "Exams",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseID",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Enrollments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "CourseSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSchedules_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSchedules_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyID",
                table: "Students",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramDepartmentId_ProgramCourseId",
                table: "Students",
                columns: new[] { "ProgramDepartmentId", "ProgramCourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseID",
                table: "Exams",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId1",
                table: "Enrollments",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSchedules_CourseID",
                table: "CourseSchedules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSchedules_InstructorID",
                table: "CourseSchedules",
                column: "InstructorID");

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
                name: "FK_Students_ProgramPlan_ProgramDepartmentId_ProgramCourseId",
                table: "Students",
                columns: new[] { "ProgramDepartmentId", "ProgramCourseId" },
                principalTable: "ProgramPlan",
                principalColumns: new[] { "DepartmentId", "CourseId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Courses_CourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses");

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
                name: "FK_Students_ProgramPlan_ProgramDepartmentId_ProgramCourseId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CourseSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramDepartmentId_ProgramCourseId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CourseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId1",
                table: "Enrollments");

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
                name: "FacultyID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramCourseId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramDepartmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramName",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ProgramID",
                table: "Students",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId2",
                table: "ProgramPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExamDate",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPlan_DepartmentId2",
                table: "ProgramPlan",
                column: "DepartmentId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramPlan_Departments_DepartmentId2",
                table: "ProgramPlan",
                column: "DepartmentId2",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
