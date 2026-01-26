using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcoursefileentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFile_AspNetUsers_UploadedById",
                table: "CourseFile");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFile_Courses_CourseId",
                table: "CourseFile");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFile_Lessons_LessonId",
                table: "CourseFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFile",
                table: "CourseFile");

            migrationBuilder.RenameTable(
                name: "CourseFile",
                newName: "CourseFiles");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFile_UploadedById",
                table: "CourseFiles",
                newName: "IX_CourseFiles_UploadedById");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFile_LessonId",
                table: "CourseFiles",
                newName: "IX_CourseFiles_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFile_CourseId",
                table: "CourseFiles",
                newName: "IX_CourseFiles_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFiles",
                table: "CourseFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_AspNetUsers_UploadedById",
                table: "CourseFiles",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_Courses_CourseId",
                table: "CourseFiles",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_Lessons_LessonId",
                table: "CourseFiles",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_AspNetUsers_UploadedById",
                table: "CourseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_Courses_CourseId",
                table: "CourseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_Lessons_LessonId",
                table: "CourseFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFiles",
                table: "CourseFiles");

            migrationBuilder.RenameTable(
                name: "CourseFiles",
                newName: "CourseFile");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFiles_UploadedById",
                table: "CourseFile",
                newName: "IX_CourseFile_UploadedById");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFiles_LessonId",
                table: "CourseFile",
                newName: "IX_CourseFile_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFiles_CourseId",
                table: "CourseFile",
                newName: "IX_CourseFile_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFile",
                table: "CourseFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFile_AspNetUsers_UploadedById",
                table: "CourseFile",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFile_Courses_CourseId",
                table: "CourseFile",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFile_Lessons_LessonId",
                table: "CourseFile",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
