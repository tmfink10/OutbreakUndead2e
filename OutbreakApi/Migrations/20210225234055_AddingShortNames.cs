using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class AddingShortNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 2,
                column: "ShortName",
                value: "Balance");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 3,
                column: "ShortName",
                value: "Bow/Crossbow");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 4,
                column: "ShortName",
                value: "Brawl");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 5,
                column: "ShortName",
                value: "Calm Other");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 6,
                column: "ShortName",
                value: "Climb");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 7,
                column: "ShortName",
                value: "Composure");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 9,
                column: "ShortName",
                value: "Digital Sys.");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 15,
                column: "ShortName",
                value: "Dodge");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 16,
                column: "ShortName",
                value: "Endurance");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 17,
                column: "ShortName",
                value: "Expression");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 20,
                column: "ShortName",
                value: "First Aid");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 21,
                column: "ShortName",
                value: "Grapple");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 22,
                column: "ShortName",
                value: "Hold");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 23,
                column: "ShortName",
                value: "Jump/Leap");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 24,
                column: "ShortName",
                value: "Lift/Pull");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 25,
                column: "ShortName",
                value: "Martial Arts");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 29,
                column: "ShortName",
                value: "Navigation");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 30,
                column: "ShortName",
                value: "Pilot");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 31,
                column: "ShortName",
                value: "Resist Pain");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 32,
                column: "ShortName",
                value: "Ride");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 33,
                column: "ShortName",
                value: "Science");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 34,
                column: "ShortName",
                value: "Search");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 35,
                column: "ShortName",
                value: "Spot/Listen");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 36,
                column: "ShortName",
                value: "Stealth");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 37,
                column: "ShortName",
                value: "Survival");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 38,
                column: "ShortName",
                value: "Swim");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 39,
                column: "ShortName",
                value: "Throw");

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 40,
                column: "ShortName",
                value: "Toughness");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 2,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 3,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 4,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 5,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 6,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 7,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 9,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 15,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 16,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 17,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 20,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 21,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 22,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 23,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 24,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 25,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 29,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 30,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 31,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 32,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 33,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 34,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 35,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 36,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 37,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 38,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 39,
                column: "ShortName",
                value: null);

            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 40,
                column: "ShortName",
                value: null);
        }
    }
}
