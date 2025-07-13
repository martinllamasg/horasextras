using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasExtrasAppClean.Migrations.OvertimeDb
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "HoursPerDayJson",
                table: "OvertimeRecords");

            migrationBuilder.DropColumn(
                name: "Semana",
                table: "OvertimeRecords");

            migrationBuilder.RenameColumn(
                name: "IsSent",
                table: "OvertimeRecords",
                newName: "Rechazado");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "OvertimeRecords",
                newName: "RazonRechazo");

            migrationBuilder.AddColumn<bool>(
                name: "Enviado",
                table: "OvertimeRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enviado",
                table: "OvertimeRecords");

            migrationBuilder.RenameColumn(
                name: "Rechazado",
                table: "OvertimeRecords",
                newName: "IsSent");

            migrationBuilder.RenameColumn(
                name: "RazonRechazo",
                table: "OvertimeRecords",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Days",
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

            migrationBuilder.AddColumn<string>(
                name: "HoursPerDayJson",
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
    }
}
