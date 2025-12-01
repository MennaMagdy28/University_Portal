using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUP.Migrations
{
    /// <inheritdoc />
    public partial class fixUserPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "ContactInfo_Phone",
            //     table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInfo_FullEnglishName",
                table: "Users",
                newName: "FullEnglishName");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
