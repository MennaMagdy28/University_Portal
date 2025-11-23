using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class ReduceJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_CourseOfferings_CousreOfferingId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Users_DeanID",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DepartmentID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Users_UserID",
                table: "Instructors");

            migrationBuilder.DropTable(
                name: "UserContacts");

            migrationBuilder.DropTable(
                name: "UserPersonalInfos");

            migrationBuilder.RenameColumn(
                name: "NationalID",
                table: "Users",
                newName: "NationalId");

            migrationBuilder.RenameColumn(
                name: "CGPA",
                table: "Students",
                newName: "Cgpa");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Instructors",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Instructors",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors",
                newName: "IX_Instructors_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DepartmentID",
                table: "Instructors",
                newName: "IX_Instructors_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "DeanID",
                table: "Faculties",
                newName: "DeanId");

            migrationBuilder.RenameColumn(
                name: "FacultyName",
                table: "Faculties",
                newName: "DisplayName");

            migrationBuilder.RenameIndex(
                name: "IX_Faculties_DeanID",
                table: "Faculties",
                newName: "IX_Faculties_DeanId");

            migrationBuilder.RenameColumn(
                name: "CousreOfferingId",
                table: "Exams",
                newName: "CourseOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_CousreOfferingId",
                table: "Exams",
                newName: "IX_Exams_CourseOfferingId");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Departments",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_FacultyID",
                table: "Departments",
                newName: "IX_Departments_FacultyId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_AltEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullEnglishName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonalInfo_BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_BirthPlace",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonalInfo_Gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_Nationality",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo_Religion",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "CourseWorkGrade",
                table: "ProgramPlan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalGrade",
                table: "ProgramPlan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "AcademicTitle",
                table: "Instructors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_CourseOfferings_CourseOfferingId",
                table: "Exams",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Users_DeanId",
                table: "Faculties",
                column: "DeanId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DepartmentId",
                table: "Instructors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Users_UserId",
                table: "Instructors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_CourseOfferings_CourseOfferingId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Users_DeanId",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DepartmentId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Users_UserId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_AltEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullEnglishName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_BirthPlace",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Nationality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInfo_Religion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseWorkGrade",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "FinalGrade",
                table: "ProgramPlan");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Faculties");

            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Users",
                newName: "NationalID");

            migrationBuilder.RenameColumn(
                name: "Cgpa",
                table: "Students",
                newName: "CGPA");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Instructors",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Instructors",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors",
                newName: "IX_Instructors_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                newName: "IX_Instructors_DepartmentID");

            migrationBuilder.RenameColumn(
                name: "DeanId",
                table: "Faculties",
                newName: "DeanID");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Faculties",
                newName: "FacultyName");

            migrationBuilder.RenameIndex(
                name: "IX_Faculties_DeanId",
                table: "Faculties",
                newName: "IX_Faculties_DeanID");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingId",
                table: "Exams",
                newName: "CousreOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_CourseOfferingId",
                table: "Exams",
                newName: "IX_Exams_CousreOfferingId");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Departments",
                newName: "FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                newName: "IX_Departments_FacultyID");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicTitle",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "UserContacts",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContacts", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserContacts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPersonalInfos",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPersonalInfos", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserPersonalInfos_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyID",
                table: "Departments",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_CourseOfferings_CousreOfferingId",
                table: "Exams",
                column: "CousreOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Users_DeanID",
                table: "Faculties",
                column: "DeanID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DepartmentID",
                table: "Instructors",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Users_UserID",
                table: "Instructors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
