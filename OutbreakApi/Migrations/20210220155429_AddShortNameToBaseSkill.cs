using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class AddShortNameToBaseSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "BaseSkills",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShortName",
                value: "Adv. Medicine");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 8,
                column: "ShortName",
                value: "Craft/Cons./Eng.");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 10,
                column: "ShortName",
                value: "Barter/Bribe");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 11,
                column: "ShortName",
                value: "Command");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 12,
                column: "ShortName",
                value: "Det. Motives");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 13,
                column: "ShortName",
                value: "Intimidate");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 14,
                column: "ShortName",
                value: "Persuade");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 18,
                column: "ShortName",
                value: "Long Gun");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 19,
                column: "ShortName",
                value: "Pistol");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 26,
                column: "ShortName",
                value: "Bludgeoning");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 27,
                column: "ShortName",
                value: "Piercing");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 28,
                column: "ShortName",
                value: "Slashing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "BaseSkills");
        }
    }
}
