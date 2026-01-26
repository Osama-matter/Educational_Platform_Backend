using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCourseFilesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    BlobStorageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: true),
                    UploadedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFile_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFile_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFile_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFile_CourseId",
                table: "CourseFile",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFile_LessonId",
                table: "CourseFile",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFile_UploadedById",
                table: "CourseFile",
                column: "UploadedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseFile");
        }
    }
}
