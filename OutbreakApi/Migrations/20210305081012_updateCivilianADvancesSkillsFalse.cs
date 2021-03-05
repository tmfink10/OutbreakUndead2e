using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class updateCivilianADvancesSkillsFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 18,
                column: "AdvancesSkills",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 18,
                column: "AdvancesSkills",
                value: true);
        }
    }
}
