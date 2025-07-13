using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasExtrasAppClean.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSentToOvertimeRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutorizacionToken",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "FechaAutorizacion",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "IsAprobado",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "JefeDirectoEmail",
                table: "OvertimeRecords");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "OvertimeRecords",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Days",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "OvertimeRecords",
                newName: "SentAt");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Days",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutorizacionToken",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAutorizacion",
                table: "OvertimeRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAprobado",
                table: "OvertimeRecords",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JefeDirectoEmail",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
