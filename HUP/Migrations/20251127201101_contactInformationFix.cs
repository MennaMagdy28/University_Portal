using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class contactInformationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "ContactInfo_Phone");

            migrationBuilder.RenameColumn(
                name: "FullEnglishName",
                table: "Users",
                newName: "PersonalInfo_FullEnglishName");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalInfo_BirthPlace",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInfo_FullEnglishName",
                table: "Users",
                newName: "FullEnglishName");

            migrationBuilder.RenameColumn(
                name: "ContactInfo_Phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalInfo_BirthPlace",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
