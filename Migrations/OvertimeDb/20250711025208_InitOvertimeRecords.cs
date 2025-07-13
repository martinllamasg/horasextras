using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasExtrasAppClean.Migrations.OvertimeDb
{
    /// <inheritdoc />
    public partial class InitOvertimeRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "OvertimeRecords");

            migrationBuilder.RenameColumn(
                name: "ArchivoAdjuntoNombre",
                table: "OvertimeRecords",
                newName: "NombreEmpleado");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DetalleActividades",
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

            migrationBuilder.AddColumn<string>(
                name: "DiasSeleccionadosJson",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HorasPorDiaJson",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hours",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Semana",
                table: "OvertimeRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasSeleccionadosJson",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "HorasPorDiaJson",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "Semana",
                table: "OvertimeRecords");

            migrationBuilder.RenameColumn(
                name: "NombreEmpleado",
                table: "OvertimeRecords",
                newName: "ArchivoAdjuntoNombre");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetalleActividades",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "OvertimeRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
