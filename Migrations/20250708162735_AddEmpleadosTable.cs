using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasExtrasAppClean.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpleadosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "OvertimeRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JefeDirectoEmail",
                table: "OvertimeRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "OvertimeRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JefeDirectoEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");

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
                name: "IsSent",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "JefeDirectoEmail",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "OvertimeRecords");
        }
    }
}
