using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfflineTicketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tickets",
                type: "TEXT",
                nullable: true);
        }
    }
}
