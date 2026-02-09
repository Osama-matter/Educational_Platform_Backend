using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteCountToForumEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostVotes_forumPosts_ForumPostId",
                table: "ForumPostVotes");

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "ForumThreads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ForumPostId",
                table: "ForumPostVotes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ForumThreadId",
                table: "ForumPostVotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostVotes_ForumThreadId",
                table: "ForumPostVotes",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostVotes_ForumThreads_ForumThreadId",
                table: "ForumPostVotes",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostVotes_forumPosts_ForumPostId",
                table: "ForumPostVotes",
                column: "ForumPostId",
                principalTable: "forumPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostVotes_ForumThreads_ForumThreadId",
                table: "ForumPostVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostVotes_forumPosts_ForumPostId",
                table: "ForumPostVotes");

            migrationBuilder.DropIndex(
                name: "IX_ForumPostVotes_ForumThreadId",
                table: "ForumPostVotes");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "ForumThreadId",
                table: "ForumPostVotes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForumPostId",
                table: "ForumPostVotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostVotes_forumPosts_ForumPostId",
                table: "ForumPostVotes",
                column: "ForumPostId",
                principalTable: "forumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
