using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class AddTrainingValuesToPlayerAbility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerAbilityId",
                table: "PlayerTrainingValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTrainingValues_PlayerAbilityId",
                table: "PlayerTrainingValues",
                column: "PlayerAbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTrainingValues_PlayerAbilities_PlayerAbilityId",
                table: "PlayerTrainingValues",
                column: "PlayerAbilityId",
                principalTable: "PlayerAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTrainingValues_PlayerAbilities_PlayerAbilityId",
                table: "PlayerTrainingValues");

            migrationBuilder.DropIndex(
                name: "IX_PlayerTrainingValues_PlayerAbilityId",
                table: "PlayerTrainingValues");

            migrationBuilder.DropColumn(
                name: "PlayerAbilityId",
                table: "PlayerTrainingValues");
        }
    }
}
