using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class BaseAbilityConfig : IEntityTypeConfiguration<BaseAbility>
    {

        public void Configure(EntityTypeBuilder<BaseAbility> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(2500);
            builder.Property(a => a.HtmlDescription)
                .IsRequired()
                .HasMaxLength(2500);
            builder.Property(a => a.AdvancesSkills)
                .IsRequired();
            builder.Property(a => a.Type)
                .HasMaxLength(50);
            builder.Property(a => a.IsProfessional)
                .IsRequired();
            builder.Property(a => a.UsesBaseAttributesCoded)
                .HasMaxLength(10);
            builder.Property(a => a.ShortName)
                .IsRequired()
                .HasMaxLength(22);

            builder.Ignore(a => a.UsesBaseAttributes);
            builder.Ignore(a => a.SupportsBaseSkills);
            builder.Ignore(a => a.ModifiesBaseTrainingValues);

            //attributes
            builder.HasMany<BaseAttribute>(a => a.UsesBaseAttributes);

            //skills
            builder.HasMany<BaseSkill>(a => a.SupportsBaseSkills);

            //training values
            builder.HasMany<BaseTrainingValue>(a => a.ModifiesBaseTrainingValues);

            //seed data
            builder.HasData(new BaseAbility
            {
                Id = 1,
                Name = "Acumen",
                ShortName = "Acumen",
                Description = "This character has a high degree of situational awareness and can come up with solutions quickly. \r\n\r\nSpecial Feature(s): \r\nEach Tier adds + 1 Competence Point(s) each time a GM uses a Location Hazard against the character or the party he or she is in.\r\n\r\nMastery: A character always gets + 2 Competence Point(s) when determining their starting pool.",
                HtmlDescription = "This character has a high degree of situational awareness and can come up with solutions quickly. <br/><br/>Special Feature(s): <br/>Each Tier adds + 1 Competence Point(s) each time a GM uses a Location Hazard against the character or the party he or she is in.<br/><br/>Mastery: A character always gets + 2 Competence Point(s) when determining their starting pool.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 2,
                Name = "Advanced Cardiac Life Support (ACLS)",
                ShortName = "ACLS",
                Description = "With medical training and a cool head as part of the job, this training allows first-responders to keep calm and keep their patients alive. \r\n \r\nSkill Support: {Composure%, First Aid%}\r\n \r\nAdvancement Rate: +1 per Tier to {Composure%, First Aid%}\r\n \r\nTraining Value(s): +1 per Level\r\n -First Aid Kits \r\n -Medical Gear\r\n \r\nMastery: First Aid Kits can be used to remove Damage Dice assigned to a character or Injury that is “Heal (+1)”.",
                HtmlDescription = "With medical training and a cool head as part of the job, this training allows first-responders to keep calm and keep their patients alive. <br/> <br/>Skill Support: {Composure%, First Aid%}<br/> <br/>Advancement Rate: +1 per Tier to {Composure%, First Aid%}<br/> <br/>Training Value(s): +1 per Level<br/> -First Aid Kits <br/> -Medical Gear<br/> <br/>Mastery: First Aid Kits can be used to remove Damage Dice assigned to a character or Injury that is “Heal (+1)”.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "First Aid Kit,Medical Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 3,
                Name = "Agility",
                ShortName = "Agility",
                Description = "This character is nimble and gifted with quick reaction time. \r\n\r\nSkill Support: {Balance%, Dodge%}\r\n\r\nAdvancement Rate: +1 per Tier to {Balance%, Dodge%}\r\n\r\nMastery: Add +1 Defense when targeted with Ranged Attacks.",
                HtmlDescription = "This character is nimble and gifted with quick reaction time. <br/><br/>Skill Support: {Balance%, Dodge%}<br/><br/>Advancement Rate: +1 per Tier to {Balance%, Dodge%}<br/><br/>Mastery: Add +1 Defense when targeted with Ranged Attacks.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 4,
                Name = "Animal Handling <Species>",
                ShortName = "Animal Handling",
                Description = "This character has experience in training and commanding animals of a chosen species.\r\n\r\nSpecial Feature(s): \r\nEach Tier adds +1 Morale to any “Animal Fellowship – Service Animal” Formations. Each Tier also allows the instruction of one Trick or Command to the kind of animal described in the entry.\r\n\r\nSkill Support: {Ride%}\r\n\r\nAdvancement Rate: +1 per Tier to {Ride%}\r\n\r\nMastery: A character can issue Commands to the animal without requiring a Check, but Formation Morale will still be reduced by the type of command issued.",
                HtmlDescription = "This character has experience in training and commanding animals of a chosen species.<br/><br/>Special Feature(s): <br/>Each Tier adds +1 Morale to any “Animal Fellowship – Service Animal” Formations. Each Tier also allows the instruction of one Trick or Command to the kind of animal described in the entry.<br/><br/>Skill Support: {Ride%}<br/><br/>Advancement Rate: +1 per Tier to {Ride%}<br/><br/>Mastery: A character can issue Commands to the animal without requiring a Check, but Formation Morale will still be reduced by the type of command issued.",
                UsesBaseAttributesCoded = "E,W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 5,
                Name = "Animal Fellowship <Named Animal>",
                ShortName = "Animal Fellowship",
                Description = "An animal and the character have established a strong bond and connection over time.\r\n\r\nSpecial Feature(s): \r\nThis Ability only applies to one animal and multiple Tiers in this Ability apply to the same animal. If the character has multiple animals with whom they would like an Animal Fellowship with, they must take a separate instance of the Ability with its own Tier rating.\r\n\r\nEach Tier grants +1 Morale to any Formation with the named animal companion. Characters lose 1 Morale per Tier if this animal comes to any serious harm or dies.\r\n\r\nMastery: A Formation with the named animal cannot be broken and any Command that requires Morale to be reduced to use, can be used for free.",
                HtmlDescription = "An animal and the character have established a strong bond and connection over time.<br/><br/>Special Feature(s): <br/>This Ability only applies to one animal and multiple Tiers in this Ability apply to the same animal. If the character has multiple animals with whom they would like an Animal Fellowship with, they must take a separate instance of the Ability with its own Tier rating.<br/><br/>Each Tier grants +1 Morale to any Formation with the named animal companion. Characters lose 1 Morale per Tier if this animal comes to any serious harm or dies.<br/><br/>Mastery: A Formation with the named animal cannot be broken and any Command that requires Morale to be reduced to use, can be used for free.",
                UsesBaseAttributesCoded = "E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 6,
                Name = "Artistic",
                ShortName = "Artistic",
                Description = "This character is capable of deep and meaningful artistic expression in their preferred medium. This can include drawing, photography, writing and acting.\r\n\r\nSkill Support: {Expression%}\r\n\r\nAdvancement Rate: +1 per Tier to {Expression%}\r\n\r\nTraining Value(s):\r\n “Drawing/Musical/Photography/Writing” (pick one)\r\n\r\nMastery: Any Mission that uses {Expression%} to check against Work will count as spending Time per + instead of the normal Time 1.",
                HtmlDescription = "This character is capable of deep and meaningful artistic expression in their preferred medium. This can include drawing, photography, writing and acting.<br/><br/>Skill Support: {Expression%}<br/><br/>Advancement Rate: +1 per Tier to {Expression%}<br/><br/>Training Value(s):<br/> “Drawing/Musical/Photography/Writing” (pick one)<br/><br/>Mastery: Any Mission that uses {Expression%} to check against Work will count as spending Time per + instead of the normal Time 1.",
                UsesBaseAttributesCoded = "P,E",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 7,
                Name = "Athletic Conditioning <Sport>",
                ShortName = "Athletic Cond.",
                Description = "This character is highly athletic and plays some sort of organized sport regularly.\r\n\r\nSkill Support: The nature of the sport will determine what pair of Skills count as Supported by this Ability:\r\n -Archery {Bow% and Spot/Listen%}\r\n -Baseball/Cricket {Melee Attack <Bludgeoning>% and Throw%}\r\n -Basketball {Jump/Leap% and Throw%}\r\n -Boxing {Brawl%, and Toughness%}\r\n -Football/Rugby {Resist Pain% and Grapple% or Throw%}\r\n -Golf {Melee Attack <Bludgeoning>% and Hold%}\r\n -Gymnastics {Balance% and Jump/Leap%}\r\n -Hockey {Balance% and Melee Attack <Bludgeoning>%}\r\n -Soccer {Brawl% and Endurance%}\r\n\r\nAdvancement Rate: +1 per Tier to Supported {SC%}\r\n\r\nTraining Value(s):\r\nAthletic  Equipment +1 per Tier\r\n\r\nMastery: Increase any Multiplier when using “Athletic Gear” by 1x to a maximum of 5x.",
                HtmlDescription = "This character is highly athletic and plays some sort of organized sport regularly.<br/><br/>Skill Support: The nature of the sport will determine what pair of Skills count as Supported by this Ability:<br/> -Archery {Bow% and Spot/Listen%}<br/> -Baseball/Cricket {Melee Attack <Bludgeoning>% and Throw%}<br/> -Basketball {Jump/Leap% and Throw%}<br/> -Boxing {Brawl%, and Toughness%}<br/> -Football/Rugby {Resist Pain% and Grapple% or Throw%}<br/> -Golf {Melee Attack <Bludgeoning>% and Hold%}<br/> -Gymnastics {Balance% and Jump/Leap%}<br/> -Hockey {Balance% and Melee Attack <Bludgeoning>%}<br/> -Soccer {Brawl% and Endurance%}<br/><br/>Advancement Rate: +1 per Tier to Supported {SC%}<br/><br/>Training Value(s):<br/>Athletic  Equipment +1 per Tier<br/><br/>Mastery: Increase any Multiplier when using “Athletic Gear” by 1x to a maximum of 5x.",
                UsesBaseAttributesCoded = "S,W",
                ModifiesTrainingValuesCoded = "Athletic Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 8,
                Name = "Autodidact",
                ShortName = "Autodidact",
                Description = "This character is self-taught in some area of professional expertise.\r\n\r\nTraining Value(s): +2 to one kind of Kit. This Ability can be taken at multiple Tiers, but each Tier will add Training Value to a different kind of Kit. Some examples of possible Kits include but are not limited to:\r\n -Archery Gear – A specialized kit for using bows and crossbows.\r\n -Climbing Gear – A specialized kit for climbing sheer surfaces.\r\n -Diving Gear – A specialized kit for swimming and diving.\r\n -First Aid Kit – A specialized kit for addressing minor damage.\r\n -Recon Gear – A specialized kit for navigating and scouting.\r\n -Survival Kit – A specialized kit for basic wilderness survival.\r\n -Tool Kit – A specialized kit for conducting basic repairs and maintenance.\r\n -Trapping Kit – A specialized kit for trapping a certain type of creature or opponent.\r\n\r\nMastery: Increase all Training Values granted by this Ability from +2 to +3.",
                HtmlDescription = "This character is self-taught in some area of professional expertise.<br/><br/>Training Value(s): +2 to one kind of Kit. This Ability can be taken at multiple Tiers, but each Tier will add Training Value to a different kind of Kit. Some examples of possible Kits include but are not limited to:<br/> -Archery Gear – A specialized kit for using bows and crossbows.<br/> -Climbing Gear – A specialized kit for climbing sheer surfaces.<br/> -Diving Gear – A specialized kit for swimming and diving.<br/> -First Aid Kit – A specialized kit for addressing minor damage.<br/> -Recon Gear – A specialized kit for navigating and scouting.<br/> -Survival Kit – A specialized kit for basic wilderness survival.<br/> -Tool Kit – A specialized kit for conducting basic repairs and maintenance.<br/> -Trapping Kit – A specialized kit for trapping a certain type of creature or opponent.<br/><br/>Mastery: Increase all Training Values granted by this Ability from +2 to +3.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 9,
                Name = "Basic First Aid",
                ShortName = "First Aid",
                Description = "This character has basic first aid training that is extremely useful in survival situations. The ease of instruction is offset by the relatively shallow range of applications.\r\n\r\nSkill Support: {First Aid%}\r\nAdvancement Rate: +1 per Tier to {First Aid%}\r\n\r\nTraining Value(s):\r\nFirst Aid +1 per Tier\r\n\r\nMastery: Triggered Effects and Missions that allow for up to Heal 2 can target Damage Dice assigned to an Injury instead of just a character.",
                HtmlDescription = "This character has basic first aid training that is extremely useful in survival situations. The ease of instruction is offset by the relatively shallow range of applications.<br/><br/>Skill Support: {First Aid%}<br/>Advancement Rate: +1 per Tier to {First Aid%}<br/><br/>Training Value(s):<br/>First Aid +1 per Tier<br/><br/>Mastery: Triggered Effects and Missions that allow for up to Heal 2 can target Damage Dice assigned to an Injury instead of just a character.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "First Aid Kit",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 10,
                Name = "Biker",
                ShortName = "Biker",
                Description = "This character is comfortable with motorcycles and can perform high-speed maneuvers with them in (relative) safety.\r\n\r\nSkill Support: {Pilot <Motorcycle>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Pilot <Motorcycle>%}\r\n\r\nTraining Value(s):\r\nMotorcycles +1 per Tier\r\n\r\nMastery: Add +1x Multiplier when determining how far a character can travel on a motorcycle.",
                HtmlDescription = "This character is comfortable with motorcycles and can perform high-speed maneuvers with them in (relative) safety.<br/><br/>Skill Support: {Pilot <Motorcycle>%}<br/><br/>Advancement Rate: +1 per Tier to {Pilot <Motorcycle>%}<br/><br/>Training Value(s):<br/>Motorcycles +1 per Tier<br/><br/>Mastery: Add +1x Multiplier when determining how far a character can travel on a motorcycle.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Vehicles",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 11,
                Name = "Billy Goat",
                ShortName = "Billy Goat",
                Description = "This character has a knack for being able to keep down things that are very difficult to eat.\r\n\r\nSpecial Feature(s): \r\nIf a character wants to purchase any Sustenance from a Resource Catalog, the Depletion Dice roll is treated as having “Efficient (1 per Tier)”. Another character can eat what this character purchases, but they must pass a {Composure%} check to be able to eat it.\r\n\r\nMastery: Being able to supplement edible food with food that is less than desirable allows this character to add “Efficient (Wb)” to all food supplies.",
                HtmlDescription = "This character has a knack for being able to keep down things that are very difficult to eat.<br/><br/>Special Feature(s): <br/>If a character wants to purchase any Sustenance from a Resource Catalog, the Depletion Dice roll is treated as having “Efficient (1 per Tier)”. Another character can eat what this character purchases, but they must pass a {Composure%} check to be able to eat it.<br/><br/>Mastery: Being able to supplement edible food with food that is less than desirable allows this character to add “Efficient (Wb)” to all food supplies.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 12,
                Name = "BMX",
                ShortName = "BMX",
                Description = "This character is skilled with bicycle riding and can traverse even rough terrain while on a bike.\r\n\r\nSkill Support: {Pilot <Bicycle>%}\r\nAdvancement Rate: +1 per Tier to {Pilot <Bicycle>%}\r\n\r\nTraining Value(s):\r\nBicycles +1 per Tier\r\n\r\nMastery: Add +d5! to the Sb when determining how long a character can travel per period of Time when “Traveling”.",
                HtmlDescription = "This character is skilled with bicycle riding and can traverse even rough terrain while on a bike.<br/><br/>Skill Support: {Pilot <Bicycle>%}<br/>Advancement Rate: +1 per Tier to {Pilot <Bicycle>%}<br/><br/>Training Value(s):<br/>Bicycles +1 per Tier<br/><br/>Mastery: Add +d5! to the Sb when determining how long a character can travel per period of Time when “Traveling”.",
                UsesBaseAttributesCoded = "S",
                ModifiesTrainingValuesCoded = "Vehicles",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 13,
                Name = "Bookworm",
                ShortName = "Bookworm",
                Description = "This character is well-read and can work through books quickly, with high retention. This character knows how to apply the knowledge gained or improves their intellectual and cultural standing within the Stronghold.\r\n\r\nSpecial Feature(s): \r\nEach Time 20, a character can gain +1 Competence Point(s) per Tier when undertaking a “Read/Study” Mission when researching or reading printed sources. A character can also gain +1 Competence Point(s) to be used towards “Read/Study” while the character is undertaking a Short Rest for Time 1. This can be done for Time 1 per Tier each day.\r\n\r\nMastery: Speed Reader. A character can gain a Competence Point(s) when undertaking a “Read/Study” Mission every Time 10 instead of the normal Time 20.",
                HtmlDescription = "This character is well-read and can work through books quickly, with high retention. This character knows how to apply the knowledge gained or improves their intellectual and cultural standing within the Stronghold.<br/><br/>Special Feature(s): <br/>Each Time 20, a character can gain +1 Competence Point(s) per Tier when undertaking a “Read/Study” Mission when researching or reading printed sources. A character can also gain +1 Competence Point(s) to be used towards “Read/Study” while the character is undertaking a Short Rest for Time 1. This can be done for Time 1 per Tier each day.<br/><br/>Mastery: Speed Reader. A character can gain a Competence Point(s) when undertaking a “Read/Study” Mission every Time 10 instead of the normal Time 20.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 14,
                Name = "Bushcraft",
                ShortName = "Bushcraft",
                Description = "A character, far from having merely day-to-day survival skills, focused their attention on how to make extremely durable and useful tools with what they find.\r\n\r\nSpecial Feature(s): \r\nEach Tier adds +1 Durability to any gear made by the character with the “Crude” special rule. There are also restrictions to the complexity of what a character can make with such skills. Consult the Gear Section on pg. 129. \r\n\r\nMastery: A character can make a Tool (Mastercraft) and Melee Weapon (Mastercraft) without the normal doubling of Work required.",
                HtmlDescription = "A character, far from having merely day-to-day survival skills, focused their attention on how to make extremely durable and useful tools with what they find.<br/><br/>Special Feature(s): <br/>Each Tier adds +1 Durability to any gear made by the character with the “Crude” special rule. There are also restrictions to the complexity of what a character can make with such skills. Consult the Gear Section on pg. 129. <br/><br/>Mastery: A character can make a Tool (Mastercraft) and Melee Weapon (Mastercraft) without the normal doubling of Work required.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 15,
                Name = "Caged Wisdom",
                ShortName = "Caged Wisdom",
                Description = "Incarceration is not an ideal position to be in, but a character that has done hard time has a crash course in many hard life lessons and exercises a level of ingenuity that could be useful in a survival situation. \r\n\r\nSkill Support: Select One {Diplomacy <Intimidate>%, Melee Attack <Any>%, Brawl%, or Composure%}\r\n\r\nTraining Value(s): +1 per Tier\r\nPick One:\r\n -Bludgeon\r\n -Piercing\r\n -Slashing\r\n -Any category of Work\r\n\r\nMastery: All {Diplomacy <Intimidate>%} checks targeting this character get 1 Difficulty Die.",
                HtmlDescription = "Incarceration is not an ideal position to be in, but a character that has done hard time has a crash course in many hard life lessons and exercises a level of ingenuity that could be useful in a survival situation. <br/><br/>Skill Support: Select One {Diplomacy <Intimidate>%, Melee Attack <Any>%, Brawl%, or Composure%}<br/><br/>Training Value(s): +1 per Tier<br/>Pick One:<br/> -Bludgeon<br/> -Piercing<br/> -Slashing<br/> -Any category of Work<br/><br/>Mastery: All {Diplomacy <Intimidate>%} checks targeting this character get 1 Difficulty Die.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Tools",
                ModifiesTrainingValuesOptionsCoded = "Bludgeon,Piercing,Slashing",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 16,
                Name = "Charisma",
                ShortName = "Charisma",
                Description = "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. \r\n\r\nSpecial Feature(s): \r\nEach Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.\r\n\r\nA character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.\r\n\r\nSkill Support: {Diplomacy <Barter/Bribe>%, Diplomacy <Command>%, Diplomacy <Determine Motives>%, Diplomacy <Persuade>%, Expression%}.\r\n\r\nMastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold.",
                HtmlDescription = "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. <br/><br/>Special Feature(s): <br/>Each Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.<br/><br/>A character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.<br/><br/>Skill Support: {Diplomacy <Barter/Bribe>%, Diplomacy <Command>%, Diplomacy <Determine Motives>%, Diplomacy <Persuade>%, Expression%}.<br/><br/>Mastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold.",
                UsesBaseAttributesCoded = "E",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 17,
                Name = "Civil Engineering - Profession",
                ShortName = "Civil Engineering",
                Description = "This is a professional discipline dealing with the design, construction, and maintenance of works like roads, bridges, canals, dams, and buildings. \r\n\r\nSpecial Feature(s): \r\nWhen this Character is stationed at a Stronghold with a Population bonus of more than 2 for Time 20, they will generate +1 Competence Point(s) per Tier.\r\n\r\nSkill Support: {Craft/Construct/Engineer (Structural)%}\r\n\r\nMastery: The character gets bonus Competence Point(s) from this Ability every Time 10 instead of the normal Time 20.",
                HtmlDescription = "This is a professional discipline dealing with the design, construction, and maintenance of works like roads, bridges, canals, dams, and buildings. <br/><br/>Special Feature(s): <br/>When this Character is stationed at a Stronghold with a Population bonus of more than 2 for Time 20, they will generate +1 Competence Point(s) per Tier.<br/><br/>Skill Support: {Craft/Construct/Engineer (Structural)%}<br/><br/>Mastery: The character gets bonus Competence Point(s) from this Ability every Time 10 instead of the normal Time 20.",
                UsesBaseAttributesCoded = "P,W",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 18,
                Name = "Civilian - Profession",
                ShortName = "Civilian",
                Description = "This character is well suited for some area of the private sector. Like many in the current job climate, a broad skill set is required to remain employed. \r\n\r\nSkill Support: Select one Basic or Trained Skill or {Pilot <Automobile>%}\r\n\r\nSubsequent Tiers in this Ability will require another Skill be Supported in this manner.\r\n\r\nTraining Value(s): +1 per Tier to the Training Value of two different types of Kit. Some examples of possible Kits include but are not limited to:\r\n -Archery Gear – A specialized kit for using bows and crossbows.\r\n -Climbing Gear – A specialized kit for climbing sheer surfaces.\r\n -Diving Gear – A specialized kit for swimming and diving.\r\n -First Aid Kit – A specialized kit for addressing minor damage.\r\n -Recon Gear – A specialized kit for navigating and scouting.\r\n -Survival Kit – A specialized kit for basic wilderness survival.\r\n -Tool Kit – A specialized kit for conducting basic repairs and maintenance.\r\n -Trapping Kit – A specialized kit for trapping a certain type of creature or opponent.\r\n -Vehicle (Any)- A means of transporting passengers and gear.\r\n\r\nMastery: Each Skill Supported by this Ability gains an Advancement Rate of +1.",
                HtmlDescription = "This character is well suited for some area of the private sector. Like many in the current job climate, a broad skill set is required to remain employed. <br/><br/>Skill Support: Select one Basic or Trained Skill or {Pilot <Automobile>%}<br/><br/>Subsequent Tiers in this Ability will require another Skill be Supported in this manner.<br/><br/>Training Value(s): +1 per Tier to the Training Value of two different types of Kit. Some examples of possible Kits include but are not limited to:<br/> -Archery Gear – A specialized kit for using bows and crossbows.<br/> -Climbing Gear – A specialized kit for climbing sheer surfaces.<br/> -Diving Gear – A specialized kit for swimming and diving.<br/> -First Aid Kit – A specialized kit for addressing minor damage.<br/> -Recon Gear – A specialized kit for navigating and scouting.<br/> -Survival Kit – A specialized kit for basic wilderness survival.<br/> -Tool Kit – A specialized kit for conducting basic repairs and maintenance.<br/> -Trapping Kit – A specialized kit for trapping a certain type of creature or opponent.<br/> -Vehicle (Any)- A means of transporting passengers and gear.<br/><br/>Mastery: Each Skill Supported by this Ability gains an Advancement Rate of +1.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 19,
                Name = "Clinical Skills - Profession",
                ShortName = "Clinical Skills",
                Description = "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.\r\n\r\nSkill Support: {First Aid%}\r\n\r\nAdvancement Rate: +1 per Tier to {First Aid%}\r\n\r\nTraining Value(s):\r\n -First Aid: +1 per Tier\r\n -Medical Supplies: +1 per Tier\r\n -“Medical Supplies” with one form of Injury specificity \r\n\r\nMastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both.",
                HtmlDescription = "Those skilled in medicine can identify illness, treat common ailments, and provide care to sustain life in all states of acuity.<br/><br/>Skill Support: {First Aid%}<br/><br/>Advancement Rate: +1 per Tier to {First Aid%}<br/><br/>Training Value(s):<br/> -First Aid: +1 per Tier<br/> -Medical Supplies: +1 per Tier<br/> -“Medical Supplies” with one form of Injury specificity <br/><br/>Mastery: A character can increase what Result on any Damage Dice a medicine or medical tool can target by +1 on both a character and Injury. All other rules still apply for removing Damage Dice from both.",
                UsesBaseAttributesCoded = "P,E",
                ModifiesTrainingValuesCoded = "First Aid Kit,Medical Gear",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 20,
                Name = "Combat Reflexes",
                ShortName = "Combat Reflexes",
                Description = "Extensive experience with either combat training or in live combat situations has allowed this character to develop instinctive responses regarding their own defense.\r\n\r\nSpecial Feature(s): \r\nChoose one combat Skill Check. That {SC%} will not add Speed Dice to a character’s dice pool if it is used as a Save Throw. This Ability can be taken more than once. Each time it is taken, it must apply to a different {SC%}. Every {SC%} still counts as an Action, so it adds 1 Difficulty Dice to the Dice Pools of subsequent Checks in the same Round.\r\n\r\nMastery: Targeting this character with a combat Triggered Effect in melee range requires an extra + in order to use it.",
                HtmlDescription = "Extensive experience with either combat training or in live combat situations has allowed this character to develop instinctive responses regarding their own defense.<br/><br/>Special Feature(s): <br/>Choose one combat Skill Check. That {SC%} will not add Speed Dice to a character’s dice pool if it is used as a Save Throw. This Ability can be taken more than once. Each time it is taken, it must apply to a different {SC%}. Every {SC%} still counts as an Action, so it adds 1 Difficulty Dice to the Dice Pools of subsequent Checks in the same Round.<br/><br/>Mastery: Targeting this character with a combat Triggered Effect in melee range requires an extra + in order to use it.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 21,
                Name = "Concealment",
                ShortName = "Concealment",
                Description = "This character excels at moving while remaining unseen, a useful ability when being detected is the difference between life and death.\r\n\r\nSkill Support: {Stealth%}\r\n\r\nAdvancement Rate: +1 per Tier to {Stealth%}\r\n\r\nTraining Value(s): \r\nReconnaissance: +1 per Tier\r\n\r\nMastery: Double the amount of Threat reduced with any Triggered Effect or Mission that requires {Stealth%}.",
                HtmlDescription = "This character excels at moving while remaining unseen, a useful ability when being detected is the difference between life and death.<br/><br/>Skill Support: {Stealth%}<br/><br/>Advancement Rate: +1 per Tier to {Stealth%}<br/><br/>Training Value(s): <br/>Reconnaissance: +1 per Tier<br/><br/>Mastery: Double the amount of Threat reduced with any Triggered Effect or Mission that requires {Stealth%}.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Reconnaissance Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 22,
                Name = "Contractor - Profession",
                ShortName = "Contractor",
                Description = "This character is capable of building or modifying structures.\r\n\r\nSpecial Feature(s): \r\nWhen this Character is stationed at a Stronghold for Time 20, they will gain +1 Competence Point(s) per Tier. A GM can deem that a certain Industry needs to be at a certain level in order for a character to make themselves useful in this way.\r\n\r\nTraining Value(s): \r\nTools: +1 per Tier\r\n\r\nMastery: The character gets Competence Point(s) by this Ability every Time 10 instead of the normal Time 20.",
                HtmlDescription = "This character is capable of building or modifying structures.<br/><br/>Special Feature(s): <br/>When this Character is stationed at a Stronghold for Time 20, they will gain +1 Competence Point(s) per Tier. A GM can deem that a certain Industry needs to be at a certain level in order for a character to make themselves useful in this way.<br/><br/>Training Value(s): <br/>Tools: +1 per Tier<br/><br/>Mastery: The character gets Competence Point(s) by this Ability every Time 10 instead of the normal Time 20.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 23,
                Name = "Cosmopolitan",
                ShortName = "Cosmopolitan",
                Description = "This character is immersed in the styling and mannerisms of many cultures. This is either by accident of birth in large metropolitan areas or they had to adapt to this way of life in order to do business across languages and cultures.\r\n\r\nSpecial Feature(s): \r\nThis character can reduce the Gestalt Level requirement to learn a new language by 1 Gestalt Level per Tier to a minimum of 1.\r\n\r\nSkill Support: {Diplomacy <Determine Motives>%}\r\n\r\nAdvancement Rate: Each Tier adds +1 to {Diplomacy <Determine Motives>%}\r\n\r\nMastery: This character knows every major language and most dialects within their native Language Family. See pg. 55 for more information on Language.",
                HtmlDescription = "This character is immersed in the styling and mannerisms of many cultures. This is either by accident of birth in large metropolitan areas or they had to adapt to this way of life in order to do business across languages and cultures.<br/><br/>Special Feature(s): <br/>This character can reduce the Gestalt Level requirement to learn a new language by 1 Gestalt Level per Tier to a minimum of 1.<br/><br/>Skill Support: {Diplomacy <Determine Motives>%}<br/><br/>Advancement Rate: Each Tier adds +1 to {Diplomacy <Determine Motives>%}<br/><br/>Mastery: This character knows every major language and most dialects within their native Language Family. See pg. 55 for more information on Language.",
                UsesBaseAttributesCoded = "P,E,W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 24,
                Name = "Craftsman <Material>",
                ShortName = "Craftsman",
                Description = "This character has experience working with a specific material such as wood, metal, ceramics, etc.\r\n\r\nSpecial Feature(s): \r\nUndertaking any construction or repair Mission with the named material will gain +1 Competence Point(s) per Tier towards the Work needed to accomplish the task.\r\n\r\nSkill Support: {Craft/Construct/Engineer <Material>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Construct/Engineer <Material>%}\r\n\r\nTraining Value(s):\r\nCrafting <Material> (Work) +1 per Tier\r\n\r\nMastery: All crafted Gear and Upgrades will have one higher Durability than what is normal for the type.",
                HtmlDescription = "This character has experience working with a specific material such as wood, metal, ceramics, etc.<br/><br/>Special Feature(s): <br/>Undertaking any construction or repair Mission with the named material will gain +1 Competence Point(s) per Tier towards the Work needed to accomplish the task.<br/><br/>Skill Support: {Craft/Construct/Engineer <Material>%}<br/><br/>Advancement Rate: +1 per Tier to {Construct/Engineer <Material>%}<br/><br/>Training Value(s):<br/>Crafting <Material> (Work) +1 per Tier<br/><br/>Mastery: All crafted Gear and Upgrades will have one higher Durability than what is normal for the type.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 25,
                Name = "Culinary Arts",
                ShortName = "Culinary Arts",
                Description = "This character prepares food very well.\r\n\r\nSpecial Feature(s): \r\nWhen this Character is stationed at a Stronghold for Time with any ‘Kitchen’, ‘Mess Hall’ or similar upgrade, they will gain +1 Competence Point(s) per Tier. \r\n\r\nAny Missions undertaken with Sustenance as a descriptor will add Morale 1 per Tier in this Ability if a {Survival <Cooking>%} was made by this character to accomplish the Mission.\r\n\r\nTraining Value(s):\r\nCooking (Work) +1 per Tier\r\n\r\nMastery: A character is disciplined in their use of spices and knows just how much to use. This character gets Efficient 3 (All) to any spices or ingredients.",
                HtmlDescription = "This character prepares food very well.<br/><br/>Special Feature(s): <br/>When this Character is stationed at a Stronghold for Time with any ‘Kitchen’, ‘Mess Hall’ or similar upgrade, they will gain +1 Competence Point(s) per Tier. <br/><br/>Any Missions undertaken with Sustenance as a descriptor will add Morale 1 per Tier in this Ability if a {Survival <Cooking>%} was made by this character to accomplish the Mission.<br/><br/>Training Value(s):<br/>Cooking (Work) +1 per Tier<br/><br/>Mastery: A character is disciplined in their use of spices and knows just how much to use. This character gets Efficient 3 (All) to any spices or ingredients.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 26,
                Name = "Custodian - Profession",
                ShortName = "Custodian",
                Description = "This character has a background working in the custodial services and is experienced in keeping a Stronghold clean and functional.\r\n\r\nSpecial Feature(s): \r\nWhen stationed at a Stronghold for Time 1 per Size of the Stronghold, they will generate +1 Competence Point(s).\r\n\r\nTraining Value(s):\r\nCustodian (Work) +1 per Tier\r\n\r\nMastery: Time 1 to remove 1 DP from any one Stronghold feature.",
                HtmlDescription = "This character has a background working in the custodial services and is experienced in keeping a Stronghold clean and functional.<br/><br/>Special Feature(s): <br/>When stationed at a Stronghold for Time 1 per Size of the Stronghold, they will generate +1 Competence Point(s).<br/><br/>Training Value(s):<br/>Custodian (Work) +1 per Tier<br/><br/>Mastery: Time 1 to remove 1 DP from any one Stronghold feature.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 27,
                Name = "Damage Threshold",
                ShortName = "Damage Threshold",
                Description = "Having the ability to withstand extreme physical punishment is paramount to survival.\r\n\r\nSpecial Feature(s): \r\nEach Tier allows an increase to Damage Threshold by 2. \r\n\r\nMastery: “Resilience (+1)”",
                HtmlDescription = "Having the ability to withstand extreme physical punishment is paramount to survival.<br/><br/>Special Feature(s): <br/>Each Tier allows an increase to Damage Threshold by 2. <br/><br/>Mastery: “Resilience (+1)”",
                UsesBaseAttributesCoded = "S,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 28,
                Name = "Diligence",
                ShortName = "Diligence",
                Description = "When this character starts a project, they continue it with a careful and persistent effort.\r\n\r\nSpecial Feature(s): \r\nEach Tier grants +2 Competence Point(s) if the characters are not on any Mission that is Piggybacked with any other mission.\r\n\r\nMastery: Competence Point(s) are gained after Time 10 instead of the normal Time 20.",
                HtmlDescription = "When this character starts a project, they continue it with a careful and persistent effort.<br/><br/>Special Feature(s): <br/>Each Tier grants +2 Competence Point(s) if the characters are not on any Mission that is Piggybacked with any other mission.<br/><br/>Mastery: Competence Point(s) are gained after Time 10 instead of the normal Time 20.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 29,
                Name = "Dive Certified",
                ShortName = "Dive Certified",
                Description = "This character is an exceptional swimmer and knows the finer points of using sophisticated dive equipment.\r\n\r\nSkill Support: {Swim%}\r\n\r\nTraining Value(s):\r\nDiving/Swimming Gear +1 per Tier\r\n\r\nMastery: All Diving/Swimming Gear will treat its Multiplier as being 1x higher.",
                HtmlDescription = "This character is an exceptional swimmer and knows the finer points of using sophisticated dive equipment.<br/><br/>Skill Support: {Swim%}<br/><br/>Training Value(s):<br/>Diving/Swimming Gear +1 per Tier<br/><br/>Mastery: All Diving/Swimming Gear will treat its Multiplier as being 1x higher.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Swimming/Diving",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 30,
                Name = "Early Bird",
                ShortName = "Early Bird",
                Description = "This character can function with less sleep than most.\r\n\r\nSpecial Feature(s): \r\nEach Tier reduces the amount of Time a character needs to get a Long Rest by Time 1. \r\n\r\nMastery: A character has Time 5 extra per day to represent longer waking hours.",
                HtmlDescription = "This character can function with less sleep than most.<br/><br/>Special Feature(s): <br/>Each Tier reduces the amount of Time a character needs to get a Long Rest by Time 1. <br/><br/>Mastery: A character has Time 5 extra per day to represent longer waking hours.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 31,
                Name = "Efficient",
                ShortName = "Efficient",
                Description = "This character works in a well-organized and competent way.\r\n\r\nSpecial Feature(s): \r\nEach Tier grants +1 Competence Point(s). All Depletion rolls have “Efficient (Wb)”. This cannot be combined with any other rule that has “Efficient” \r\n\r\nMastery: Being Stationed at a Stronghold or a Long Rest at Safehouse for more than Time 10 will grant a character +3 extra Competence Point(s).",
                HtmlDescription = "This character works in a well-organized and competent way.<br/><br/>Special Feature(s): <br/>Each Tier grants +1 Competence Point(s). All Depletion rolls have “Efficient (Wb)”. This cannot be combined with any other rule that has “Efficient” <br/><br/>Mastery: Being Stationed at a Stronghold or a Long Rest at Safehouse for more than Time 10 will grant a character +3 extra Competence Point(s).",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 32,
                Name = "Electrical Engineering - Profession",
                ShortName = "Electrical Eng.",
                Description = "This character has an extensive background working with electrical systems. \r\n\r\nSpecial Feature(s): \r\nEach Time 20, gain +1 Competence Point(s) per Tier that can be used on any feature of ‘Garage’, ‘Factory’, or ‘Machine Shop’.\r\n\r\nTraining Value(s):\r\nElectrical (Work) +1 per Tier\r\n\r\nMastery: Grant “Defense 2 Damage Dice” from failed {Construct/Engineer (Electrical)%}. This will not apply when making Checks representing actions with no consideration for safety.",
                HtmlDescription = "This character has an extensive background working with electrical systems. <br/><br/>Special Feature(s): <br/>Each Time 20, gain +1 Competence Point(s) per Tier that can be used on any feature of ‘Garage’, ‘Factory’, or ‘Machine Shop’.<br/><br/>Training Value(s):<br/>Electrical (Work) +1 per Tier<br/><br/>Mastery: Grant “Defense 2 Damage Dice” from failed {Construct/Engineer (Electrical)%}. This will not apply when making Checks representing actions with no consideration for safety.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 33,
                Name = "Farmer/Rancher - Profession",
                ShortName = "Farmer/Rancher",
                Description = "Working as a farmer or rancher allows a character to better sustain their needs to survive.\r\n\r\nSpecial Feature(s): \r\nEach Time 20, gain +1 Competence Point(s) per Tier that can be used on any feature of ‘Garden’, ‘Ranch’, or ‘Machine Shop’ Features.\r\n\r\nSkill Support: {Science <Farming>%, Survival <Biome>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Science <Farming>%, Survival <Biome>%}\r\n\r\nTraining Value(s):\r\nFarming/Ranching (Work) +1 per Tier\r\n\r\nMastery: A character can treat the Viability of any Stronghold they are in as being 10 higher than normal. Add +1 Competence Point(s) per Wb when operating out of any Stronghold that requires farming and/or ranching.",
                HtmlDescription = "Working as a farmer or rancher allows a character to better sustain their needs to survive.<br/><br/>Special Feature(s): <br/>Each Time 20, gain +1 Competence Point(s) per Tier that can be used on any feature of ‘Garden’, ‘Ranch’, or ‘Machine Shop’ Features.<br/><br/>Skill Support: {Science <Farming>%, Survival <Biome>%}<br/><br/>Advancement Rate: +1 per Tier to {Science <Farming>%, Survival <Biome>%}<br/><br/>Training Value(s):<br/>Farming/Ranching (Work) +1 per Tier<br/><br/>Mastery: A character can treat the Viability of any Stronghold they are in as being 10 higher than normal. Add +1 Competence Point(s) per Wb when operating out of any Stronghold that requires farming and/or ranching.",
                UsesBaseAttributesCoded = "W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 34,
                Name = "Firefighter - Profession",
                ShortName = "Firefighter",
                Description = "This character can navigate through and fight fires. \r\n\r\nSkill Support: {Composure%, Diplomacy <Command/Coax>%, Spot/Listen%, Search% and Endurance%}\r\n\r\nTraining Value(s):\r\n -Firefighting +1 per Tier\r\n -First Aid +1 per Tier\r\n\r\nMastery: Reduce any Encumbrance due to Firefighting Gear by 1. Add “Efficient (Wb)” to any Firefighting Gear with a Capacity rating.",
                HtmlDescription = "This character can navigate through and fight fires. <br/><br/>Skill Support: {Composure%, Diplomacy <Command/Coax>%, Spot/Listen%, Search% and Endurance%}<br/><br/>Training Value(s):<br/> -Firefighting +1 per Tier<br/> -First Aid +1 per Tier<br/><br/>Mastery: Reduce any Encumbrance due to Firefighting Gear by 1. Add “Efficient (Wb)” to any Firefighting Gear with a Capacity rating.",
                UsesBaseAttributesCoded = "S",
                ModifiesTrainingValuesCoded = "Firefighting,First Aid Kit",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 35,
                Name = "Grenadier",
                ShortName = "Grenadier",
                Description = "This character is skilled with thrown explosives and incendiary weapons.\r\n\r\nSkill Support: {Throw%}\r\n\r\nAdvancement Rate: +1 per Tier to {Throw%}\r\n\r\nTraining Value(s):\r\nGrenades (Thrown Weapon) +1 per Tier\r\n\r\nMastery: Thrown Weapons have the Deadly quality (Add 1 Damage Dice).",
                HtmlDescription = "This character is skilled with thrown explosives and incendiary weapons.<br/><br/>Skill Support: {Throw%}<br/><br/>Advancement Rate: +1 per Tier to {Throw%}<br/><br/>Training Value(s):<br/>Grenades (Thrown Weapon) +1 per Tier<br/><br/>Mastery: Thrown Weapons have the Deadly quality (Add 1 Damage Dice).",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Throwing",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 36,
                Name = "Gunslinger",
                ShortName = "Gunslinger",
                Description = "This character is a quick-draw and a fast shot without losing much accuracy. \r\n\r\nSpecial Feature(s): \r\nThis character ignores the limitations of degrees of success being resolved only against a single target using Accuracy. They also get Rush 1 per Tier when using Pistol weapons (so Tier 2 will grant Rush 2 and Tier 3 Rush 3 and so on).\r\n\r\nTraining Value(s):\r\nHandguns +1 per Tier\r\n\r\nMastery: Gain “Rush [1 Speed Die]” from the dice pool to a minimum of 1 Speed Die when using {Firearms <Pistol>%} Check.",
                HtmlDescription = "This character is a quick-draw and a fast shot without losing much accuracy. <br/><br/>Special Feature(s): <br/>This character ignores the limitations of degrees of success being resolved only against a single target using Accuracy. They also get Rush 1 per Tier when using Pistol weapons (so Tier 2 will grant Rush 2 and Tier 3 Rush 3 and so on).<br/><br/>Training Value(s):<br/>Handguns +1 per Tier<br/><br/>Mastery: Gain “Rush [1 Speed Die]” from the dice pool to a minimum of 1 Speed Die when using {Firearms <Pistol>%} Check.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Pistol",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 37,
                Name = "Healthy",
                ShortName = "Healthy",
                Description = "This character is generally physically fit and in good overall health.\r\n\r\nSpecial Feature(s): \r\nEach Tier allows a character to remove an additional Fatigue point when Resting.\r\n\r\nSkill Support: {Endurance%}\r\n\r\nAdvancement Rate: +1 per Tier to {Endurance%}\r\n\r\nMastery: A character can treat their Sb as being 1 point higher when determining what result on any Damage Dice assigned to a character can be removed from Natural Healing.",
                HtmlDescription = "This character is generally physically fit and in good overall health.<br/><br/>Special Feature(s): <br/>Each Tier allows a character to remove an additional Fatigue point when Resting.<br/><br/>Skill Support: {Endurance%}<br/><br/>Advancement Rate: +1 per Tier to {Endurance%}<br/><br/>Mastery: A character can treat their Sb as being 1 point higher when determining what result on any Damage Dice assigned to a character can be removed from Natural Healing.",
                UsesBaseAttributesCoded = "S,W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 38,
                Name = "Honor",
                ShortName = "Honor",
                Description = "This character is honest, true to their word, and steadfast to their principles. They are, above all other things, trustworthy and conduct themselves in a respectable fashion.\r\n\r\nSkill Support: {Diplomacy <Persuade>%, Diplomacy <Command/Coax>%, Composure%}\r\n\r\nMastery: A character can use {Diplomacy <Persuade>%} as a substitute for any other kind of {Diplomacy%}, and a character will be able to use {Diplomacy <Persuade>%} against The Living even if their rules normally do not allow it.",
                HtmlDescription = "This character is honest, true to their word, and steadfast to their principles. They are, above all other things, trustworthy and conduct themselves in a respectable fashion.<br/><br/>Skill Support: {Diplomacy <Persuade>%, Diplomacy <Command/Coax>%, Composure%}<br/><br/>Mastery: A character can use {Diplomacy <Persuade>%} as a substitute for any other kind of {Diplomacy%}, and a character will be able to use {Diplomacy <Persuade>%} against The Living even if their rules normally do not allow it.",
                UsesBaseAttributesCoded = "E,W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 39,
                Name = "Iron Will",
                ShortName = "Iron Will",
                Description = "This character is not easily shaken. They are either meditative and are able to see through the chaos of life with clarity or they are so inured to psychological damage that they are very difficult to shock. In addition to this, a character’s stoicism serves as an inspiration to others. \r\n\r\nSpecial Feature(s): \r\nCharacter has +1 starting Morale per Tier.\r\n\r\nSkill Support: {Calm Other%, Composure%} \r\n\r\nAdvancement Rate: +1 per Tier to {Calm Other%, Composure%}\r\n\r\nMastery: Character can confer a bonus +1 Morale to any Formation they are in.",
                HtmlDescription = "This character is not easily shaken. They are either meditative and are able to see through the chaos of life with clarity or they are so inured to psychological damage that they are very difficult to shock. In addition to this, a character’s stoicism serves as an inspiration to others. <br/><br/>Special Feature(s): <br/>Character has +1 starting Morale per Tier.<br/><br/>Skill Support: {Calm Other%, Composure%} <br/><br/>Advancement Rate: +1 per Tier to {Calm Other%, Composure%}<br/><br/>Mastery: Character can confer a bonus +1 Morale to any Formation they are in.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 40,
                Name = "Jury Rig <Kit Type>",
                ShortName = "Jury Rig",
                Description = "Being skilled in makeshift repairs or creating temporary contrivances, made with only the tools and materials that happen to be on hand, can easily be the difference between life and death.\r\n\r\nSpecial Feature(s): \r\nEach Tier allows “Extra Supplies” for one named type of Kit to apply to another kind of Kit for the purposes of removing Depletion Points. None of the Kits can be Value or be exclusively a Combat kit (indicated by only red icons).\r\n\r\nMastery: The Character can add one Depletion Point to a Survival or Reconnaissance Gear in order to restore 1 lost Depletion Point to any gear.",
                HtmlDescription = "Being skilled in makeshift repairs or creating temporary contrivances, made with only the tools and materials that happen to be on hand, can easily be the difference between life and death.<br/><br/>Special Feature(s): <br/>Each Tier allows “Extra Supplies” for one named type of Kit to apply to another kind of Kit for the purposes of removing Depletion Points. None of the Kits can be Value or be exclusively a Combat kit (indicated by only red icons).<br/><br/>Mastery: The Character can add one Depletion Point to a Survival or Reconnaissance Gear in order to restore 1 lost Depletion Point to any gear.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 41,
                Name = "Leadership",
                ShortName = "Leadership",
                Description = "The character demonstrates a unique charisma and ability to lead. Each Tier reduces Governance of a Stronghold they are in by 1 whenever Governance is determined and also increases the Morale of Formations they lead by 1 per Tier.\r\n\r\nTraining Value(s):\r\nCommand +1 per Tier\r\n\r\nMastery: Grant a bonus Morale 3 for all Formations where this character serves as Point.",
                HtmlDescription = "The character demonstrates a unique charisma and ability to lead. Each Tier reduces Governance of a Stronghold they are in by 1 whenever Governance is determined and also increases the Morale of Formations they lead by 1 per Tier.<br/><br/>Training Value(s):<br/>Command +1 per Tier<br/><br/>Mastery: Grant a bonus Morale 3 for all Formations where this character serves as Point.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Command Apparatus",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 42,
                Name = "Locksmith - Profession",
                ShortName = "Locksmith",
                Description = "Working with and knowing how to bypass locks allows a character to access many places they otherwise would be unable to.\r\n\r\nTraining Value(s):\r\nLocksmithing (Work) +1 per Tier\r\n\r\nMastery: Characters get +1 Competence Point(s) when undertaking anything that involves lock picking or such tasks by spending Time 1 on it.",
                HtmlDescription = "Working with and knowing how to bypass locks allows a character to access many places they otherwise would be unable to.<br/><br/>Training Value(s):<br/>Locksmithing (Work) +1 per Tier<br/><br/>Mastery: Characters get +1 Competence Point(s) when undertaking anything that involves lock picking or such tasks by spending Time 1 on it.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 43,
                Name = "Marksman",
                ShortName = "Marksman",
                Description = "This character has trained extensively with one kind of specific firearm weapon and has excellent accuracy and speed with it.\r\n\r\nSpecial Feature(s): \r\nA character chooses one model of Firearm. This training allows a character to remove 1 Speed Die from the Dice Pool when using that model. This Ability can be taken multiple times; each time it applies to a different model of firearm.\r\n\r\nTraining Value(s):\r\n (Pick One)\r\n -Long Gun +1 per  Tier\r\n -Pistol +1 per Tier\r\n\r\nMastery: The “Hit” Triggered Effect with the model of Firearm and will add 1 Damage Die to any result of resolving Degrees of Success or Difference to use it.",
                HtmlDescription = "This character has trained extensively with one kind of specific firearm weapon and has excellent accuracy and speed with it.<br/><br/>Special Feature(s): <br/>A character chooses one model of Firearm. This training allows a character to remove 1 Speed Die from the Dice Pool when using that model. This Ability can be taken multiple times; each time it applies to a different model of firearm.<br/><br/>Training Value(s):<br/> (Pick One)<br/> -Long Gun +1 per  Tier<br/> -Pistol +1 per Tier<br/><br/>Mastery: The “Hit” Triggered Effect with the model of Firearm and will add 1 Damage Die to any result of resolving Degrees of Success or Difference to use it.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesOptionsCoded = "Pistol,Long Gun",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 44,
                Name = "Martial Artist",
                ShortName = "Martial Artist",
                Description = "This character is trained in martial arts, which, given time and training, makes their whole body a weapon.\r\n\r\nSkill Support: {Martial Arts%}\r\n\r\nAdvancement Rate: +1 Advancement Rate per Tier to {Martial Arts%}\r\n\r\nTraining Value(s):\r\nMartial Arts +1 per Tier\r\n\r\nMastery: Add 1 Damage Die and 1 Speed Die to any Triggered Effect targeting an opponent when a character has used {Martial Arts%}, {Brawl%}, and/or {Grapple%}.",
                HtmlDescription = "This character is trained in martial arts, which, given time and training, makes their whole body a weapon.<br/><br/>Skill Support: {Martial Arts%}<br/><br/>Advancement Rate: +1 Advancement Rate per Tier to {Martial Arts%}<br/><br/>Training Value(s):<br/>Martial Arts +1 per Tier<br/><br/>Mastery: Add 1 Damage Die and 1 Speed Die to any Triggered Effect targeting an opponent when a character has used {Martial Arts%}, {Brawl%}, and/or {Grapple%}.",
                UsesBaseAttributesCoded = "S,W",
                ModifiesTrainingValuesCoded = "Martial Arts",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 45,
                Name = "Mechanical Engineering - Profession",
                ShortName = "Mechanical Eng.",
                Description = "Indicates a character with considerable mechanical aptitude and training in addition to a generally well-developed work ethic.\r\n\r\nSpecial Feature(s): \r\nEach Time 20, a character gets +1 Competence Point(s) per Tier if stationed in a Stronghold that can be used to take advantage of the features of ‘Garage’, ‘Factory’ or ‘Machine Shop’ upgrades.\r\n\r\nMastery: Increase the Structure of a Stronghold by 10 when stationed there.",
                HtmlDescription = "Indicates a character with considerable mechanical aptitude and training in addition to a generally well-developed work ethic.<br/><br/>Special Feature(s): <br/>Each Time 20, a character gets +1 Competence Point(s) per Tier if stationed in a Stronghold that can be used to take advantage of the features of ‘Garage’, ‘Factory’ or ‘Machine Shop’ upgrades.<br/><br/>Mastery: Increase the Structure of a Stronghold by 10 when stationed there.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 46,
                Name = "Mountaineer",
                ShortName = "Mountaineer",
                Description = "This character has an iron will when it comes to climbing high places.\r\n\r\nSkill Support: {Climb%, Composure%}\r\n\r\nAdvancement Rate: +1 Advancement Rate per Tier to {Climb%, Composure%}\r\n\r\nTraining Value(s):\r\nClimbing Gear +1 per Tier\r\n\r\nMastery: Climbing over periods of Time will add 50% to the distance traveled when determining the results of a Travel mission by climbing.",
                HtmlDescription = "This character has an iron will when it comes to climbing high places.<br/><br/>Skill Support: {Climb%, Composure%}<br/><br/>Advancement Rate: +1 Advancement Rate per Tier to {Climb%, Composure%}<br/><br/>Training Value(s):<br/>Climbing Gear +1 per Tier<br/><br/>Mastery: Climbing over periods of Time will add 50% to the distance traveled when determining the results of a Travel mission by climbing.",
                UsesBaseAttributesCoded = "S,W",
                ModifiesTrainingValuesCoded = "Climbing Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 47,
                Name = "Mule",
                ShortName = "Mule",
                Description = "This character is capable of lifting and carrying heavy burdens.\r\n\r\nSpecial Feature(s): Each Tier allows a character to carry an extra 5 Cargo worth of Gear before taking Encumbrance penalties.\r\n\r\nMastery: A character ignores the “Clumsy” rule up to “Clumsy (2)”.",
                HtmlDescription = "This character is capable of lifting and carrying heavy burdens.<br/><br/>Special Feature(s): Each Tier allows a character to carry an extra 5 Cargo worth of Gear before taking Encumbrance penalties.<br/><br/>Mastery: A character ignores the “Clumsy” rule up to “Clumsy (2)”.",
                UsesBaseAttributesCoded = "S",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 48,
                Name = "Pack Rat",
                ShortName = "Pack Rat",
                Description = "This character always has a bunch of knick-knacks on their person. Every now and then, they may actually be useful.\r\n\r\nSpecial Feature(s): \r\nEach Tier allows a character to sacrifice a Competence Point(s) to ignore the adding of 1 Depletion Point for any non-combat usable (gear with a red icon) gear every Time 20. Characters will, always count as carrying 1 Cargo worth of Gear more than they actually have.\r\n\r\nMastery: All gear with Green icons will have “Efficiency (+1)” for all Depletion Dice rolled.",
                HtmlDescription = "This character always has a bunch of knick-knacks on their person. Every now and then, they may actually be useful.<br/><br/>Special Feature(s): <br/>Each Tier allows a character to sacrifice a Competence Point(s) to ignore the adding of 1 Depletion Point for any non-combat usable (gear with a red icon) gear every Time 20. Characters will, always count as carrying 1 Cargo worth of Gear more than they actually have.<br/><br/>Mastery: All gear with Green icons will have “Efficiency (+1)” for all Depletion Dice rolled.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 49,
                Name = "Pugilist",
                ShortName = "Pugulist",
                Description = "This character is an excellent boxer or fist-fighter.\r\n\r\nSkill Support: {Brawl%}\r\n\r\nAdvancement Rate: +1 per Tier to {Brawl%}\r\n\r\nTraining Value(s):\r\nBludgeon +1 per Tier\r\n\r\nMastery: All {Brawl%} will add 1 Damage Die to the dice pool.",
                HtmlDescription = "This character is an excellent boxer or fist-fighter.<br/><br/>Skill Support: {Brawl%}<br/><br/>Advancement Rate: +1 per Tier to {Brawl%}<br/><br/>Training Value(s):<br/>Bludgeon +1 per Tier<br/><br/>Mastery: All {Brawl%} will add 1 Damage Die to the dice pool.",
                UsesBaseAttributesCoded = "S",
                ModifiesTrainingValuesCoded = "Bludgeon",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 50,
                Name = "Research <Field>",
                ShortName = "Research",
                Description = "This character is an excellent researcher in one or more scientific disciplines.\r\n\r\nSpecial Feature(s): \r\nEach Time 20, a character gets +1 Competence Point(s) per Tier if stationed in a Stronghold with any kind of scientific research facility or laboratory.\r\n\r\nSkill Support: {Science <Specialty>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Science <Specialty>%}\r\n\r\nTraining Value(s):\r\nTools, Research Equipment (Work) +1 per Tier\r\n\r\nMastery: A character can treat the Science of any Stronghold they are in as being 10 higher than normal. Add +1 Competence Point(s) per Wb when operating out of any Stronghold that has an attached or upgraded Research Facility.",
                HtmlDescription = "This character is an excellent researcher in one or more scientific disciplines.<br/><br/>Special Feature(s): <br/>Each Time 20, a character gets +1 Competence Point(s) per Tier if stationed in a Stronghold with any kind of scientific research facility or laboratory.<br/><br/>Skill Support: {Science <Specialty>%}<br/><br/>Advancement Rate: +1 per Tier to {Science <Specialty>%}<br/><br/>Training Value(s):<br/>Tools, Research Equipment (Work) +1 per Tier<br/><br/>Mastery: A character can treat the Science of any Stronghold they are in as being 10 higher than normal. Add +1 Competence Point(s) per Wb when operating out of any Stronghold that has an attached or upgraded Research Facility.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Tools",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 51,
                Name = "Resourceful <Kit Type>",
                ShortName = "Resourceful",
                Description = "Being able to make do with very little or improvise heavily is the hallmark of someone who is considered resourceful.\r\n\r\nSpecial Feature(s): \r\nChoose one kind of non-firearm Kit. Characters with this Ability will have “Efficient (1 per Tier)”. A character still gains Depletion Points with Use and will exhaust their Kit if they accumulate Depletion Points equal to the Capacity.\r\n\r\nTraining Value(s): +1 to [Named Kit] per Tier\r\n\r\nMastery: A character can remove one Depletion Die regardless of the Result.",
                HtmlDescription = "Being able to make do with very little or improvise heavily is the hallmark of someone who is considered resourceful.<br/><br/>Special Feature(s): <br/>Choose one kind of non-firearm Kit. Characters with this Ability will have “Efficient (1 per Tier)”. A character still gains Depletion Points with Use and will exhaust their Kit if they accumulate Depletion Points equal to the Capacity.<br/><br/>Training Value(s): +1 to [Named Kit] per Tier<br/><br/>Mastery: A character can remove one Depletion Die regardless of the Result.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 52,
                Name = "Salesmanship",
                ShortName = "Salesmanship",
                Description = "This character is a skilled negotiator and has well-developed instincts for appealing to a customer, distributor and vendor alike.\r\n\r\nSkill Support: {Diplomacy <Barter/Bribe>%} Advancement Rate: +1 per Tier to {Diplomacy <Barter/Bribe>%}. Each Tier will allow a character to have +1 to Reputation among organizations that keep track of such relationships with the character.\r\n\r\nNote that “Reputation” is a feature that is only really explored when using Strongholds, primarily explored in the Gamemaster’s Guide. It is just listed here for completeness.\r\n\r\nTraining Value(s):\r\nCurrency +1 per Tier\r\n\r\nMastery: Characters can add Value 100% that bringing any gear “To Market” will grant.",
                HtmlDescription = "This character is a skilled negotiator and has well-developed instincts for appealing to a customer, distributor and vendor alike.<br/><br/>Skill Support: {Diplomacy <Barter/Bribe>%} Advancement Rate: +1 per Tier to {Diplomacy <Barter/Bribe>%}. Each Tier will allow a character to have +1 to Reputation among organizations that keep track of such relationships with the character.<br/><br/>Note that “Reputation” is a feature that is only really explored when using Strongholds, primarily explored in the Gamemaster’s Guide. It is just listed here for completeness.<br/><br/>Training Value(s):<br/>Currency +1 per Tier<br/><br/>Mastery: Characters can add Value 100% that bringing any gear “To Market” will grant.",
                UsesBaseAttributesCoded = "E",
                ModifiesTrainingValuesCoded = "Value",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 53,
                Name = "Search and Rescue - Profession",
                ShortName = "Search and Rescue",
                Description = "This character can search for missing people and treat the wounded in an emergency situation.\r\n\r\nSkill Support: {Search%}\r\n\r\nAdvancement Rate: +1 Advancement Rate per Tier to {Search%}\r\n\r\nTraining Value(s):\r\n -Recon + 1 per Tier\r\n -First Aid +1 per Tier\r\n\r\nMastery: Each Time 1 devoted to any “Search and Rescue” Mission grant a bonus Competence Point(s) to roll on their behalf.",
                HtmlDescription = "This character can search for missing people and treat the wounded in an emergency situation.<br/><br/>Skill Support: {Search%}<br/><br/>Advancement Rate: +1 Advancement Rate per Tier to {Search%}<br/><br/>Training Value(s):<br/> -Recon + 1 per Tier<br/> -First Aid +1 per Tier<br/><br/>Mastery: Each Time 1 devoted to any “Search and Rescue” Mission grant a bonus Competence Point(s) to roll on their behalf.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Reconnaissance Gear,First Aid Kit",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 54,
                Name = "Sharpshooter",
                ShortName = "Sharpshooter",
                Description = "The character is skilled with using a firearm at range, possibly having a background in hunting or sniping.\r\n\r\nSpecial Feature(s): \r\nThis Ability adds 1 Damage Die per tier to “Hit” and “Headshot” Triggered Effects if a character’s Intent does not have them take a Move Action and they use their Ranged Weapon only once this turn as a Sustained Action. \r\n\r\nMastery: A character may fire upon multiple targets and still claim this bonus.",
                HtmlDescription = "The character is skilled with using a firearm at range, possibly having a background in hunting or sniping.<br/><br/>Special Feature(s): <br/>This Ability adds 1 Damage Die per tier to “Hit” and “Headshot” Triggered Effects if a character’s Intent does not have them take a Move Action and they use their Ranged Weapon only once this turn as a Sustained Action. <br/><br/>Mastery: A character may fire upon multiple targets and still claim this bonus.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 55,
                Name = "Stealthy",
                ShortName = "Stealthy",
                Description = "The character is adept at being well hidden and moving silently.\r\n\r\nSkill Support: {Stealth%}\r\n\r\nAdvancement Rate: +1 per Tier to {Stealth%}\r\n\r\nSpecial Feature(s): \r\nA character gets one extra action marker per Tier for free each round to place during Stealth Encounters.\r\n\r\nMastery: A character using {Stealth%} as a part of their intent may take another action without Difficulty Dice penalties for multiple actions.",
                HtmlDescription = "The character is adept at being well hidden and moving silently.<br/><br/>Skill Support: {Stealth%}<br/><br/>Advancement Rate: +1 per Tier to {Stealth%}<br/><br/>Special Feature(s): <br/>A character gets one extra action marker per Tier for free each round to place during Stealth Encounters.<br/><br/>Mastery: A character using {Stealth%} as a part of their intent may take another action without Difficulty Dice penalties for multiple actions.",
                UsesBaseAttributesCoded = "P",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 56,
                Name = "Support Basic Skill",
                ShortName = "Supp. Basic Skill",
                Description = "This character is extensively conditioned in some specific fashion. \r\n\r\nSkill Support: Select one Basic Skill. This can be taken up to 5 Tiers, but each Tier applies to a new Basic Skill.\r\n\r\nMastery: All Basic Skills Supported by this Ability get +1 to their Advancement Rate.",
                HtmlDescription = "This character is extensively conditioned in some specific fashion. <br/><br/>Skill Support: Select one Basic Skill. This can be taken up to 5 Tiers, but each Tier applies to a new Basic Skill.<br/><br/>Mastery: All Basic Skills Supported by this Ability get +1 to their Advancement Rate.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 57,
                Name = "Support Expert Skill",
                ShortName = "Supp. Expert Skill",
                Description = "This character has been extensively trained in a very specialized and difficult skillset.\r\n\r\nSkill Support: Select one Expert Skill. This skill now counts as being Supported. This Ability can be taken up to 5 Tiers, but each\r\n\r\nTier applies to a new Expert Skill.\r\n\r\nMastery: All Expert Skills Supported by this Ability get +1 to their Advancement Rate.",
                HtmlDescription = "This character has been extensively trained in a very specialized and difficult skillset.<br/><br/>Skill Support: Select one Expert Skill. This skill now counts as being Supported. This Ability can be taken up to 5 Tiers, but each<br/><br/>Tier applies to a new Expert Skill.<br/><br/>Mastery: All Expert Skills Supported by this Ability get +1 to their Advancement Rate.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 58,
                Name = "Support Trained Skill",
                ShortName = "Supp. Trained Skill",
                Description = "This character is well trained in some specific fashion. \r\n\r\nSkill Support: Select one Trained Skill. This skill now counts as being Supported. This Ability can be taken up to 5 Tiers, but each Tier applies to a new Trained Skill.\r\n\r\nMastery: All Trained Skills Supported by this Ability get +1 to their Advancement Rate.",
                HtmlDescription = "This character is well trained in some specific fashion. <br/><br/>Skill Support: Select one Trained Skill. This skill now counts as being Supported. This Ability can be taken up to 5 Tiers, but each Tier applies to a new Trained Skill.<br/><br/>Mastery: All Trained Skills Supported by this Ability get +1 to their Advancement Rate.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 59,
                Name = "Surgical Skills - Profession",
                ShortName = "Surgical Skills",
                Description = "This character is skilled in performing delicate surgery and is familiar with surgical instruments and procedures.\r\n\r\nSkill Support: {Advanced Medicine%}\r\n\r\nAdvancement Rate: +1 per Tier to {Advanced Medicine%}\r\n\r\nTraining Value(s):\r\n -First Aid +1 per Tier\r\n -Medical Gear +1 per Tier\r\n\r\nMastery: Any removal of Damage Dice assigned to an Injury by way of Heal or any surgical or medical procedures can increase the normal Result allowed to be removed by +1.",
                HtmlDescription = "This character is skilled in performing delicate surgery and is familiar with surgical instruments and procedures.<br/><br/>Skill Support: {Advanced Medicine%}<br/><br/>Advancement Rate: +1 per Tier to {Advanced Medicine%}<br/><br/>Training Value(s):<br/> -First Aid +1 per Tier<br/> -Medical Gear +1 per Tier<br/><br/>Mastery: Any removal of Damage Dice assigned to an Injury by way of Heal or any surgical or medical procedures can increase the normal Result allowed to be removed by +1.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "First Aid Kit,Medical Gear",
                AdvancesSkills = true,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 60,
                Name = "Survival Skills <Biome>",
                ShortName = "Survival Skills",
                Description = "This character is highly accustomed to surviving within certain Biomes and is able to utilize available natural resources effectively.\r\n\r\nSpecial Feature(s): \r\nThe character gets +1 Competence Point(s) per tier when in the named Biome.\r\n\r\nSkill Support: {Survival <Biome>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Survival <Biome>%}\r\n\r\nTraining Value(s):\r\nSurvival Kit +1 per Tier\r\n\r\nMastery: Reduce Environmental Modifiers against the character within this Biome by 1.",
                HtmlDescription = "This character is highly accustomed to surviving within certain Biomes and is able to utilize available natural resources effectively.<br/><br/>Special Feature(s): <br/>The character gets +1 Competence Point(s) per tier when in the named Biome.<br/><br/>Skill Support: {Survival <Biome>%}<br/><br/>Advancement Rate: +1 per Tier to {Survival <Biome>%}<br/><br/>Training Value(s):<br/>Survival Kit +1 per Tier<br/><br/>Mastery: Reduce Environmental Modifiers against the character within this Biome by 1.",
                UsesBaseAttributesCoded = "S,P,E,W",
                ModifiesTrainingValuesCoded = "Survival Kit",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 61,
                Name = "Swimmer",
                ShortName = "Swimmer",
                Description = "This character is a strong swimmer.\r\n\r\nSkill Support: {Endurance%, Swim%}\r\n\r\nAdvancement Rate: +1 per Tier to {Endurance%, Swim%}\r\n\r\nMastery: Swimming over periods of Time 1 will add 50% to the distance traveled when determining the results of a Travel mission by swimming.",
                HtmlDescription = "This character is a strong swimmer.<br/><br/>Skill Support: {Endurance%, Swim%}<br/><br/>Advancement Rate: +1 per Tier to {Endurance%, Swim%}<br/><br/>Mastery: Swimming over periods of Time 1 will add 50% to the distance traveled when determining the results of a Travel mission by swimming.",
                UsesBaseAttributesCoded = "S",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 62,
                Name = "Switch Hitter",
                ShortName = "Switch Hitter",
                Description = "This character has good form and follows through on their swing.\r\n\r\nSpecial Feature(s): \r\nAllows a character to make one extra {Melee Attack <Bludgeoning>%} per Tier during the Check Phase without Difficulty Dice penalties for attacking multiple targets.\r\n\r\nSkill Support: {Melee Attack <Bludgeoning>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Melee Attack <Bludgeoning>%}\r\n\r\nMastery: The “Parry” Triggered effect will add 1 Speed Die per 5 Cargo whenever a character resolves Degrees of Success or Difference to trigger it.",
                HtmlDescription = "This character has good form and follows through on their swing.<br/><br/>Special Feature(s): <br/>Allows a character to make one extra {Melee Attack <Bludgeoning>%} per Tier during the Check Phase without Difficulty Dice penalties for attacking multiple targets.<br/><br/>Skill Support: {Melee Attack <Bludgeoning>%}<br/><br/>Advancement Rate: +1 per Tier to {Melee Attack <Bludgeoning>%}<br/><br/>Mastery: The “Parry” Triggered effect will add 1 Speed Die per 5 Cargo whenever a character resolves Degrees of Success or Difference to trigger it.",
                UsesBaseAttributesCoded = "S",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 63,
                Name = "Teacher - Profession",
                ShortName = "Teacher",
                Description = "The ability to impart knowledge to another represents the fundamental core of society and culture, and is a highly prized skillset in a survival situation. \r\n\r\nSpecial Feature(s): \r\nCharacters who are advancing a Skill under the instruction of this character get an Advancement Rate bonus of +1 when spending Gestalt Levels to improve the Skill. This will also earn the teaching character Survival Points equal to the Advancement made by the character under their instruction. To instruct, the character must have a higher percentage in the desired skill and a higher Gestalt Level than the student. “Read/Study” missions under this character’s instruction grant +1 Competence Point(s) per Tier in this ability if it’s a subject relevant to their experience.\r\n\r\nMastery: A character can re-roll one die when advancing a skill under this character’s instruction.",
                HtmlDescription = "The ability to impart knowledge to another represents the fundamental core of society and culture, and is a highly prized skillset in a survival situation. <br/><br/>Special Feature(s): <br/>Characters who are advancing a Skill under the instruction of this character get an Advancement Rate bonus of +1 when spending Gestalt Levels to improve the Skill. This will also earn the teaching character Survival Points equal to the Advancement made by the character under their instruction. To instruct, the character must have a higher percentage in the desired skill and a higher Gestalt Level than the student. “Read/Study” missions under this character’s instruction grant +1 Competence Point(s) per Tier in this ability if it’s a subject relevant to their experience.<br/><br/>Mastery: A character can re-roll one die when advancing a skill under this character’s instruction.",
                UsesBaseAttributesCoded = "E",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 64,
                Name = "Therapist/Social Worker - Profession",
                ShortName = "Therapist/SW",
                Description = "An understanding of the human psyche allows a character to help others cope with the stresses of survival.\r\n\r\nSpecial Feature(s): \r\n{Diplomacy%} Skill Checks now have a base Multiplier of 1x.\r\n\r\nMastery: A character can reduce the amount of Time they can retry {Calm Other%, Composure%, or any Diplomacy%} to restore Morale 1 day per + in a {Science <Psychology>%} Check.",
                HtmlDescription = "An understanding of the human psyche allows a character to help others cope with the stresses of survival.<br/><br/>Special Feature(s): <br/>{Diplomacy%} Skill Checks now have a base Multiplier of 1x.<br/><br/>Mastery: A character can reduce the amount of Time they can retry {Calm Other%, Composure%, or any Diplomacy%} to restore Morale 1 day per + in a {Science <Psychology>%} Check.",
                UsesBaseAttributesCoded = "E",
                AdvancesSkills = false,
                IsProfessional = true
            });

            builder.HasData(new BaseAbility
            {
                Id = 65,
                Name = "Tough",
                ShortName = "Tough",
                Description = "This character has a surprising resistance to pain.\r\n\r\nSpecial Feature(s): \r\nA character does not suffer Speed penalties when using {Toughness%} as a Save Throw.\r\n\r\nSkill Support: {Toughness%}\r\n\r\nAdvancement Rate: +1 per Tier to {Toughness%}\r\n\r\nMastery: The Character may add 3 Speed Dice to their Dice Pool to target themselves with “Heal 1”.",
                HtmlDescription = "This character has a surprising resistance to pain.<br/><br/>Special Feature(s): <br/>A character does not suffer Speed penalties when using {Toughness%} as a Save Throw.<br/><br/>Skill Support: {Toughness%}<br/><br/>Advancement Rate: +1 per Tier to {Toughness%}<br/><br/>Mastery: The Character may add 3 Speed Dice to their Dice Pool to target themselves with “Heal 1”.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 66,
                Name = "Traceur",
                ShortName = "Traceur",
                Description = "This character is capable of astounding feats of strength and agility in navigating urban environments.\r\n\r\nSkill Support: {Climb%, Navigation%}\r\n\r\nAdvancement Rate: +1 per Tier to {Climb%, Navigation%}\r\n\r\nTraining Value(s):\r\nClimbing Gear +1 per Tier\r\n\r\nMastery: Each Time 1 devoted to any “Scout” Mission grant a bonus Competence Point(s) to roll on their behalf.",
                HtmlDescription = "This character is capable of astounding feats of strength and agility in navigating urban environments.<br/><br/>Skill Support: {Climb%, Navigation%}<br/><br/>Advancement Rate: +1 per Tier to {Climb%, Navigation%}<br/><br/>Training Value(s):<br/>Climbing Gear +1 per Tier<br/><br/>Mastery: Each Time 1 devoted to any “Scout” Mission grant a bonus Competence Point(s) to roll on their behalf.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Climbing Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 67,
                Name = "Trapper/Hunter",
                ShortName = "Trapper/Hunter",
                Description = "This character is capable of hunting and trapping game and has more than a passing understanding of both traps and basic survival skills. \r\n\r\nTraining Value(s):\r\n -Survival Kit +1 per Tier\r\n -Hunting/Trapping Tools (Work) +1 per Tier\r\n\r\nMatery: Each Time 1 devoted to any “Survival” Mission grant a bonus Competence Point(s) when hunting or trapping.",
                HtmlDescription = "This character is capable of hunting and trapping game and has more than a passing understanding of both traps and basic survival skills. <br/><br/>Training Value(s):<br/> -Survival Kit +1 per Tier<br/> -Hunting/Trapping Tools (Work) +1 per Tier<br/><br/>Matery: Each Time 1 devoted to any “Survival” Mission grant a bonus Competence Point(s) when hunting or trapping.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Survival Kit,Tools",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 68,
                Name = "Training, Bow/Crossbow",
                ShortName = "Archery Trn",
                Description = "This character is well trained in the use of a bow or crossbow. \r\n\r\nSkill Support: {Bow%}\r\n\r\nAdvancement Rate: +1 per Tier to {Bow%}\r\n\r\nTraining Value(s):\r\nArchery +1 per Tier\r\n\r\nMastery: Add 1 Damage Dice to “Hit” and “Headshot” when {Bow%} is used.",
                HtmlDescription = "This character is well trained in the use of a bow or crossbow. <br/><br/>Skill Support: {Bow%}<br/><br/>Advancement Rate: +1 per Tier to {Bow%}<br/><br/>Training Value(s):<br/>Archery +1 per Tier<br/><br/>Mastery: Add 1 Damage Dice to “Hit” and “Headshot” when {Bow%} is used.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Archery Gear",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 69,
                Name = "Training, Kit/Tools",
                ShortName = "Kit/Tool Trn",
                Description = "This character is well trained in the use of some kind of non-weapon equipment.\r\n\r\nTraining Value(s):\r\n[Non-Weapon Kit] +2 per Tier\r\n\r\nMastery: Any use of the named Kit or Tool for Time1 will grant a character +1 Competence Point(s) to roll on their behalf.",
                HtmlDescription = "This character is well trained in the use of some kind of non-weapon equipment.<br/><br/>Training Value(s):<br/>[Non-Weapon Kit] +2 per Tier<br/><br/>Mastery: Any use of the named Kit or Tool for Time1 will grant a character +1 Competence Point(s) to roll on their behalf.",
                UsesBaseAttributesCoded = "S,P,E,W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 70,
                Name = "Training, Long Guns",
                ShortName = "Long Gun Trn",
                Description = "This character is extensively trained with long guns and is capable of firing shots carefully and accurately.\r\n\r\nSkill Support: {Firearms <Long Gun>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Firearms <Long Gun>%}\r\n\r\nTraining Value(s):\r\nLong Gun +1 per Tier\r\n\r\nMastery: Using {Firearms <Long Gun>%} Grants an additional 1 Damage Die for the Deadly quality.",
                HtmlDescription = "This character is extensively trained with long guns and is capable of firing shots carefully and accurately.<br/><br/>Skill Support: {Firearms <Long Gun>%}<br/><br/>Advancement Rate: +1 per Tier to {Firearms <Long Gun>%}<br/><br/>Training Value(s):<br/>Long Gun +1 per Tier<br/><br/>Mastery: Using {Firearms <Long Gun>%} Grants an additional 1 Damage Die for the Deadly quality.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Long Gun",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 71,
                Name = "Training, Melee Weapons",
                ShortName = "Melee Weapon Trn",
                Description = "This character is trained with a specific kind of melee weapon, generally from the Bludgeoning, Piercing, or Slashing weapon categories.\r\n\r\nSkill Support: Select One {Melee Attack <Bludgeoning, Piercing, or Slashing>%}\r\n\r\nAdvancement Rate: Each Tier adds +1 to the Supported Skills\r\n\r\nTraining Value(s):\r\n (Pick One)\r\n -Bludgeon +1 per Tier\r\n -Piercing +1 per Tier\r\n -Slashing +1 per Tier\r\n\r\nMastery: Any {Melee Attack%} will add 1 Damage Die to the dice pool.",
                HtmlDescription = "This character is trained with a specific kind of melee weapon, generally from the Bludgeoning, Piercing, or Slashing weapon categories.<br/><br/>Skill Support: Select One {Melee Attack <Bludgeoning, Piercing, or Slashing>%}<br/><br/>Advancement Rate: Each Tier adds +1 to the Supported Skills<br/><br/>Training Value(s):<br/> (Pick One)<br/> -Bludgeon +1 per Tier<br/> -Piercing +1 per Tier<br/> -Slashing +1 per Tier<br/><br/>Mastery: Any {Melee Attack%} will add 1 Damage Die to the dice pool.",
                UsesBaseAttributesCoded = "S",
                ModifiesTrainingValuesOptionsCoded = "Bludgeon,Piercing,Slashing",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 72,
                Name = "Training, Pistol",
                ShortName = "Pistol Trn",
                Description = "This character is extensively trained with pistols and is capable of firing shots carefully and accurately.\r\n\r\nSkill Support: {Firearms <Pistol>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Firearms <Pistol>%}\r\n\r\nTraining Value(s):\r\nHandgun +1 per Tier\r\n\r\nMastery: Using {Firearms <Pistol>%} Grants an additional 1 Damage Die for the Deadly quality.",
                HtmlDescription = "This character is extensively trained with pistols and is capable of firing shots carefully and accurately.<br/><br/>Skill Support: {Firearms <Pistol>%}<br/><br/>Advancement Rate: +1 per Tier to {Firearms <Pistol>%}<br/><br/>Training Value(s):<br/>Handgun +1 per Tier<br/><br/>Mastery: Using {Firearms <Pistol>%} Grants an additional 1 Damage Die for the Deadly quality.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Pistol",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 73,
                Name = "Training, Vehicle/Vessel",
                ShortName = "Vehicle/Vessel Trn",
                Description = "This character is familiar with the working of one kind of motor vehicle or water vessel.\r\n\r\nSkill Support: {Pilot <Vehicle/Vessel Class>%}.\r\n\r\nTraining Value(s):\r\nVehicle/Vessel Class (Vehicle) +1 per Tier\r\n\r\nMastery: A character using this Vessel or Vehicle will be able to add 50% to the amount of Distance they are able to travel with any “Travel” mission they undertake with it.",
                HtmlDescription = "This character is familiar with the working of one kind of motor vehicle or water vessel.<br/><br/>Skill Support: {Pilot <Vehicle/Vessel Class>%}.<br/><br/>Training Value(s):<br/>Vehicle/Vessel Class (Vehicle) +1 per Tier<br/><br/>Mastery: A character using this Vessel or Vehicle will be able to add 50% to the amount of Distance they are able to travel with any “Travel” mission they undertake with it.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesCoded = "Vehicles",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 74,
                Name = "Trigger Discipline",
                ShortName = "Trigger Discipline",
                Description = "This character excels in the ability to carefully manage ammunition. This is an invaluable asset when ammo is scarce.\r\n\r\nSpecial Feature(s): \r\nCharacters with this Ability have “Efficient (1 per Tier)” when using firearms with a Capacity. This has no effect on firearms that do not require Depletion rolls, such as bolt action or revolvers. Depletion Points that equals Capacity will still count as exhausting an ammunition supply and requiring a Reload.\r\n\r\nTraining Value(s):\r\n (Pick One)\r\n -Long Gun +1 per Tier\r\n -Handgun +1 per Tier\r\n\r\nMastery: Remove one Depletion Die of any Result when using Firearms.",
                HtmlDescription = "This character excels in the ability to carefully manage ammunition. This is an invaluable asset when ammo is scarce.<br/><br/>Special Feature(s): <br/>Characters with this Ability have “Efficient (1 per Tier)” when using firearms with a Capacity. This has no effect on firearms that do not require Depletion rolls, such as bolt action or revolvers. Depletion Points that equals Capacity will still count as exhausting an ammunition supply and requiring a Reload.<br/><br/>Training Value(s):<br/> (Pick One)<br/> -Long Gun +1 per Tier<br/> -Handgun +1 per Tier<br/><br/>Mastery: Remove one Depletion Die of any Result when using Firearms.",
                UsesBaseAttributesCoded = "P",
                ModifiesTrainingValuesOptionsCoded = "Long Gun,Pistol",
                AdvancesSkills = true,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 75,
                Name = "Volunteer",
                ShortName = "Volunteer",
                Description = "This character has experience working as a volunteer which allows them to work with people of all kinds.\r\n\r\nSpecial Feature(s): \r\nBeing a part of any Formation will increase the starting Party Morale by 1 per Tier.\r\n\r\nSkill Support: Characters can also Support a Skill relevant to the service provided as a volunteer.\r\n\r\nMastery: +1 Competence Point(s) per Wb when in Formation.",
                HtmlDescription = "This character has experience working as a volunteer which allows them to work with people of all kinds.<br/><br/>Special Feature(s): <br/>Being a part of any Formation will increase the starting Party Morale by 1 per Tier.<br/><br/>Skill Support: Characters can also Support a Skill relevant to the service provided as a volunteer.<br/><br/>Mastery: +1 Competence Point(s) per Wb when in Formation.",
                UsesBaseAttributesCoded = "E",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 76,
                Name = "Weekend Warrior",
                ShortName = "Weekend Warrior",
                Description = "This character is an outdoor enthusiast but can’t always pry themselves away from their urban surroundings and out to the great outdoors.\r\n\r\nSpecial Feature(s): \r\nEach Tier allows an Advancement to {Survival%} or {Navigation%}. A character may also take an Advancement to {Pilot <Vehicle Class>%} of Size: 2-3.\r\n\r\nMastery: A character can add 1x to Survival Gear.",
                HtmlDescription = "This character is an outdoor enthusiast but can’t always pry themselves away from their urban surroundings and out to the great outdoors.<br/><br/>Special Feature(s): <br/>Each Tier allows an Advancement to {Survival%} or {Navigation%}. A character may also take an Advancement to {Pilot <Vehicle Class>%} of Size: 2-3.<br/><br/>Mastery: A character can add 1x to Survival Gear.",
                UsesBaseAttributesCoded = "W",
                AdvancesSkills = false,
                IsProfessional = false
            });

            builder.HasData(new BaseAbility
            {
                Id = 77,
                Name = "Wheedle",
                ShortName = "Wheedle",
                Description = "This character can use flattery, personality, and other such techniques in order to make people reveal more information.\r\n\r\nSpecial Feature(s): \r\nTreat a successful {Diplomacy%} as having one higher + than normal per Tier. This cannot be used with {Diplomacy (Intimidate)%}.\r\n\r\nSkill Support: {Expression%, Diplomacy <Command/Coax>%}\r\n\r\nAdvancement Rate: +1 per Tier to {Expression%, Diplomacy <Command/Coax>%}.\r\n\r\nMastery: Any “Assess Person” Mission of Work 5 or less will be automatically successful.",
                HtmlDescription = "This character can use flattery, personality, and other such techniques in order to make people reveal more information.<br/><br/>Special Feature(s): <br/>Treat a successful {Diplomacy%} as having one higher + than normal per Tier. This cannot be used with {Diplomacy (Intimidate)%}.<br/><br/>Skill Support: {Expression%, Diplomacy <Command/Coax>%}<br/><br/>Advancement Rate: +1 per Tier to {Expression%, Diplomacy <Command/Coax>%}.<br/><br/>Mastery: Any “Assess Person” Mission of Work 5 or less will be automatically successful.",
                UsesBaseAttributesCoded = "E",
                AdvancesSkills = true,
                IsProfessional = false
            });
        }
    }
}
