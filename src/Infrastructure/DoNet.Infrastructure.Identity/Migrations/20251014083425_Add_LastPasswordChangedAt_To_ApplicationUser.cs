using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoNet.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Add_LastPasswordChangedAt_To_ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastPasswordChangedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastPasswordChangedAt",
                table: "Users",
                column: "LastPasswordChangedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_LastPasswordChangedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastPasswordChangedAt",
                table: "Users");
        }
    }
}
