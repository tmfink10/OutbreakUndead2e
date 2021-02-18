using Microsoft.EntityFrameworkCore.Migrations;

namespace OutbreakApi.Migrations
{
    public partial class PlayerSkillSpeciality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerCharacterId1",
                table: "PlayerSkills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "PlayerSkills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.\r\n\r\nSkill Support: {First Aid%}\r\n\r\nAdvancement Rate: +1 per Tier to {First Aid%}\r\n\r\nTraining Value(s):\r\n -First Aid: +1 per Tier\r\n -Medical Supplies: +1 per Tier\r\n -“Medical Supplies” with one form of Injury specificity \r\n\r\nMastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both.", "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.<br/><br/>Skill Support: {First Aid%}<br/><br/>Advancement Rate: +1 per Tier to {First Aid%}<br/><br/>Training Value(s):<br/> -First Aid: +1 per Tier<br/> -Medical Supplies: +1 per Tier<br/> -“Medical Supplies” with one form of Injury specificity <br/><br/>Mastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both." });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkills_PlayerCharacterId1",
                table: "PlayerSkills",
                column: "PlayerCharacterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSkills_PlayerCharacters_PlayerCharacterId1",
                table: "PlayerSkills",
                column: "PlayerCharacterId1",
                principalTable: "PlayerCharacters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSkills_PlayerCharacters_PlayerCharacterId1",
                table: "PlayerSkills");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSkills_PlayerCharacterId1",
                table: "PlayerSkills");

            migrationBuilder.DropColumn(
                name: "PlayerCharacterId1",
                table: "PlayerSkills");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "PlayerSkills");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.\r\n\r\nSkill Support: {First Aid%}\r\n\r\nAdvancement Rate: +1 per Tier to {First Aid%}\r\n\r\nTraining Value(s):\r\n -First Aid: +1 per Tier\r\n -Medical Supplies: 1+ per Tier\r\n -“Medical Supplies” with one form of Injury specificity \r\n\r\nMastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both.", "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.<br/><br/>Skill Support: {First Aid%}<br/><br/>Advancement Rate: +1 per Tier to {First Aid%}<br/><br/>Training Value(s):<br/> -First Aid: +1 per Tier<br/> -Medical Supplies: 1+ per Tier<br/> -“Medical Supplies” with one form of Injury specificity <br/><br/>Mastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both." });
        }
    }
}
