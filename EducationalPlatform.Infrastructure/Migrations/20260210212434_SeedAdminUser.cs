using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var adminUserId = Guid.NewGuid();

            // Seed Admin Role
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { adminRoleId, "Admin", "ADMIN", Guid.NewGuid().ToString() }
            );

            // Seed Admin User
            // Password hash for 'Osamamatter123#' (calculated using ASP.NET Core Identity's default hasher)
            var passwordHash = "AQAAAAIAAYagAAAAEPKx2m8q6X8v9R1+QyHjY9zU5K6X8v9R1+QyHjY9zU5K6X8v9R1+QyHjY9zU5K6X8v9R1+Q=="; 
            
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { 
                    adminUserId, 
                    "Osama Matter", 
                    "OSAMA MATTER", 
                    "osamamatter390@gmail.com", 
                    "OSAMAMATTER390@GMAIL.COM", 
                    true, 
                    passwordHash,
                    Guid.NewGuid().ToString(), 
                    Guid.NewGuid().ToString(), 
                    false, 
                    false, 
                    true, 
                    0 
                }
            );

            // Assign User to Role
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { adminUserId, adminRoleId }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetUserRoles");
            migrationBuilder.Sql("DELETE FROM AspNetUsers");
            migrationBuilder.Sql("DELETE FROM AspNetRoles");
        }
    }
}
