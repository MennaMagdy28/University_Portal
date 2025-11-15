using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class CourseEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId1",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingId1",
                table: "Enrollments",
                newName: "CourseId1");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingId",
                table: "Enrollments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseOfferingId1",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId1");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseOfferingId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId1",
                table: "Enrollments",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId1",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "CourseId1",
                table: "Enrollments",
                newName: "CourseOfferingId1");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollments",
                newName: "CourseOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId1",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseOfferingId1");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId",
                table: "Enrollments",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_CourseOfferings_CourseOfferingId1",
                table: "Enrollments",
                column: "CourseOfferingId1",
                principalTable: "CourseOfferings",
                principalColumn: "Id");
        }
    }
}
