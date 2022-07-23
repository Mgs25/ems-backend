using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ems_backend.Migrations
{
    public partial class otp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpires",
                table: "Users",
                newName: "OtpExpires");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Users",
                newName: "Otp");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactAddress",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "OtpExpires",
                table: "Users",
                newName: "ResetTokenExpires");

            migrationBuilder.RenameColumn(
                name: "Otp",
                table: "Users",
                newName: "PasswordResetToken");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
