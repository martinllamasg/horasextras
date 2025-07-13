using Microsoft.EntityFrameworkCore.Migrations;

namespace HorasExtrasAppClean.Migrations
{
    public partial class AddWeekNumberToOvertimeRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "OvertimeRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "OvertimeRecord");
        }
    }
}
