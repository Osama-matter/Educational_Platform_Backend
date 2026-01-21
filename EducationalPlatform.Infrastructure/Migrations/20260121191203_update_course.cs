using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_URl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_URl",
                table: "Courses");
        }
    }
}
