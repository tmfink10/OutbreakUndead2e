using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class UpdateBaseSkillCrafting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 8,
                column: "ShortName",
                value: "Craft/Con./Eng.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseSkills",
                keyColumn: "Id",
                keyValue: 8,
                column: "ShortName",
                value: "Craft/Cons./Eng.");
        }
    }
}
