using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutbreakModels.Models;

namespace OutbreakApi.FluentConfigs
{
    public class BaseSkillConfig : IEntityTypeConfiguration<BaseSkill>
    {
        public void Configure(EntityTypeBuilder<BaseSkill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(5000);
            builder.Property(s => s.HtmlDescription)
                .IsRequired()
                .HasMaxLength(5000);
            builder.Property(s => s.Type)
                .HasMaxLength(20);
            builder.Property(s => s.ShortName)
                .HasMaxLength(18);

            builder.HasOne<BaseAttribute>(s => s.PrimaryAttribute);
            builder.HasOne<BaseAttribute>(s => s.SecondaryAttribute);

            builder.HasData(new BaseSkill
            {
                Id = 1,
                Name = "Advanced Medicine",
                ShortName = "Adv. Medicine",
                Description = "{Advanced Medicine%} {AMed%}\r\nPb + Wb\r\nTime 1 or more\r\n\r\nThis is a Character’s ability to perform advanced medical procedures such as administering proper physical therapy and surgery. This comes from an advanced understanding of the body that goes beyond simply treating an injury. \r\n\r\nSpecialization: A character can specialize in a Field of Medicine (Cardiology, Radiology, Oncology, Dentistry, Immunology, Genetics, Hematology, Surgery, Pathology, etc) or treatment of a specific Injury type. \r\n\r\nDiagnose: +  A character can assess the severity and possible diseases of one observed symptom of a character. Each + allows an additional symptom to be assessed and dismissed if it is inconsequential.\r\n\r\nStabilize: +  Add Time 1 per Wb of the character to the “Treatment Time” of a character with an \"Injury\". This is the only Triggered Effect that {Advanced Medicine%} can utilize during an Encounter.\r\n\r\nSuppress Symptom: + – This reduces the effective Viral total of a character by 1 per + when determining the Symptoms of disease, if they scale in severity by Viral total. This will not reduce the Viral total, but it can stave off more severe Symptoms, or even prevent death if the symptom is lethal at a certain Viral total. This treatment lasts Time 20.\r\n\r\nHeal: +  Any Damage Dice result can be removed from a character.\r\n\r\nTreat Injury: +  One “Injury” is considered successfully “Treated”. Characters acting as physicians to the wounded character can continue to make {Advanced Medicine%} checks in order to generate enough + for potentially more Treat “Injury” Triggered Effects so long as the physician has remaining “Treatment Time” to re-try.\r\n\r\nExpedite Recovery: +  Reduce the “Recovery Time” by Time 10 per + resolved.\r\n\r\nExtreme Measures: +  Counts as having two Injuries “Treated” but it carries with it a dire lasting consequence. Consult the Injury entry for some examples of what this could be.\r\n\r\nComplication: - -  Reduce available “Treatment Time” to perform {Advanced Medicine%} by Time 1 .\r\n\r\nBotch: -  Add Time 10 to the “Recovery Time”.\r\n\r\nAggravate Injury: - – Aggravate any Damage Dice assigned to a character to the Injury attempting to be treated.\r\nLabor: Each + will count as Time in various amounts as per ‘Advanced Medicine’ Missions depending upon the facilities the treatment takes place in:\r\n -Field: Time 1 per +\r\n -Clinic, Poor: Time 2 per +\r\n -Infirmary: Time 3 per +\r\n -Dedicated Medical Care Facility: Time 4 per +\r\n\r\nModifiers to {Advanced Medicine%}\r\n -Viral: 1 Difficulty Dice per point",
                HtmlDescription = "{Advanced Medicine%} {AMed%}<br/>Pb + Wb<br/>Time 1 or more<br/><br/>This is a Character’s ability to perform advanced medical procedures such as administering proper physical therapy and surgery. This comes from an advanced understanding of the body that goes beyond simply treating an injury. <br/><br/>Specialization: A character can specialize in a Field of Medicine (Cardiology, Radiology, Oncology, Dentistry, Immunology, Genetics, Hematology, Surgery, Pathology, etc) or treatment of a specific Injury type. <br/><br/>Diagnose: +  A character can assess the severity and possible diseases of one observed symptom of a character. Each + allows an additional symptom to be assessed and dismissed if it is inconsequential.<br/><br/>Stabilize: +  Add Time 1 per Wb of the character to the “Treatment Time” of a character with an \"Injury\". This is the only Triggered Effect that {Advanced Medicine%} can utilize during an Encounter.<br/><br/>Suppress Symptom: + – This reduces the effective Viral total of a character by 1 per + when determining the Symptoms of disease, if they scale in severity by Viral total. This will not reduce the Viral total, but it can stave off more severe Symptoms, or even prevent death if the symptom is lethal at a certain Viral total. This treatment lasts Time 20.<br/><br/>Heal: +  Any Damage Dice result can be removed from a character.<br/><br/>Treat Injury: +  One “Injury” is considered successfully “Treated”. Characters acting as physicians to the wounded character can continue to make {Advanced Medicine%} checks in order to generate enough + for potentially more Treat “Injury” Triggered Effects so long as the physician has remaining “Treatment Time” to re-try.<br/><br/>Expedite Recovery: +  Reduce the “Recovery Time” by Time 10 per + resolved.<br/><br/>Extreme Measures: +  Counts as having two Injuries “Treated” but it carries with it a dire lasting consequence. Consult the Injury entry for some examples of what this could be.<br/><br/>Complication: - -  Reduce available “Treatment Time” to perform {Advanced Medicine%} by Time 1 .<br/><br/>Botch: -  Add Time 10 to the “Recovery Time”.<br/><br/>Aggravate Injury: - – Aggravate any Damage Dice assigned to a character to the Injury attempting to be treated.<br/>Labor: Each + will count as Time in various amounts as per ‘Advanced Medicine’ Missions depending upon the facilities the treatment takes place in:<br/> -Field: Time 1 per +<br/> -Clinic, Poor: Time 2 per +<br/> -Infirmary: Time 3 per +<br/> -Dedicated Medical Care Facility: Time 4 per +<br/><br/>Modifiers to {Advanced Medicine%}<br/> -Viral: 1 Difficulty Dice per point",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 2,
                Name = "Balance",
                ShortName = "Balance",
                Description = "Balance\r\nBalance%} {Bal%}\r\nPerception + Wb\r\n\r\n%D 3 Speed Dice (Involved) for ‘Actions’,\r\n%D 2 Speed Dice (Average) for ‘Save Throws’\r\n\r\nBalance represents a character’s ability to remain on their feet or maintain some other advantageous posture.\r\n\r\nStability:  + Move at Normal Movement Rate while balancing, adding Speed Dice to the Dice Pool normally for movement. \r\nStagger:  -  Move at Crawling Movement Rate while balancing, adding Speed Dice to the Dice Pool normally for movement. \r\nFall:  - - - - - You have fallen! Depending on the height of the fall, or what is down below, this could be very bad. Every 10 feet falling will take 1 Damage Die Bludgeoning. Falling on objects or on the Character’s own Weapons or Gear may add [Piercing, Slashing] to the Damage types. \r\nRegain Footing: Save Throw – Success in a Save Throw for a Character to remain on their feet or to maintain their position prevents the addition of any Speed Dice due to the Triggered effect of an Opponent in Melee or Grapple Range. Any success in this Save Throw will allow a Character to remain upright, failure will cause a Character to become Knocked Prone as per the Triggered Effect of the same name.\r\n\r\nModifiers to {Balance%}\r\n -Encumbrance\r\n -Environmental Modifiers: 1 Difficulty Dice per Feature that makes gripping or footing difficult, such as rain or ice.",
                HtmlDescription = "Balance<br/>Balance%} {Bal%}<br/>Perception + Wb<br/><br/>%D 3 Speed Dice (Involved) for ‘Actions’,<br/>%D 2 Speed Dice (Average) for ‘Save Throws’<br/><br/>Balance represents a character’s ability to remain on their feet or maintain some other advantageous posture.<br/><br/>Stability:  + Move at Normal Movement Rate while balancing, adding Speed Dice to the Dice Pool normally for movement. <br/>Stagger:  -  Move at Crawling Movement Rate while balancing, adding Speed Dice to the Dice Pool normally for movement. <br/>Fall:  - - - - - You have fallen! Depending on the height of the fall, or what is down below, this could be very bad. Every 10 feet falling will take 1 Damage Die Bludgeoning. Falling on objects or on the Character’s own Weapons or Gear may add [Piercing, Slashing] to the Damage types. <br/>Regain Footing: Save Throw – Success in a Save Throw for a Character to remain on their feet or to maintain their position prevents the addition of any Speed Dice due to the Triggered effect of an Opponent in Melee or Grapple Range. Any success in this Save Throw will allow a Character to remain upright, failure will cause a Character to become Knocked Prone as per the Triggered Effect of the same name.<br/><br/>Modifiers to {Balance%}<br/> -Encumbrance<br/> -Environmental Modifiers: 1 Difficulty Dice per Feature that makes gripping or footing difficult, such as rain or ice.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 3,
                Name = "Bow/Crossbow",
                ShortName = "Bow/Crossbow",
                Description = "Bow/Crossbow\r\n{Bow/Crossbow%} {Bow%}\r\nPerception + Sb\r\n%D 3 Speed Dice (Involved)\r\n\r\nSpecializations: Bow, Crossbow, Compound Bow, Compound Crossbow, Hand/ Wrist Crossbow.\r\nThis is a character’s ability to use bow and crossbow weapons.\r\n\r\nAim: +\r\nReload (1): + or add Speed Dice as per the entry of the weapon.\r\nHit: + 1 Damage Die Piercing, Slashing; Accuracy, Ranged.\r\nHeadshot: + + + + + 4 Damage Dice Piercing, Slashing, 0 Defense; Instant, Ranged.\r\n\r\nModifiers to {Bow/Crossbow%}\r\n -Environmental Modifiers\r\n -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks)\r\n -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                HtmlDescription = "Bow/Crossbow<br/>{Bow/Crossbow%} {Bow%}<br/>Perception + Sb<br/>%D 3 Speed Dice (Involved)<br/><br/>Specializations: Bow, Crossbow, Compound Bow, Compound Crossbow, Hand/ Wrist Crossbow.<br/>This is a character’s ability to use bow and crossbow weapons.<br/><br/>Aim: +<br/>Reload (1): + or add Speed Dice as per the entry of the weapon.<br/>Hit: + 1 Damage Die Piercing, Slashing; Accuracy, Ranged.<br/>Headshot: + + + + + 4 Damage Dice Piercing, Slashing, 0 Defense; Instant, Ranged.<br/><br/>Modifiers to {Bow/Crossbow%}<br/> -Environmental Modifiers<br/> -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks)<br/> -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 1
            });

            builder.HasData(new BaseSkill
            {
                Id = 4,
                Name = "Brawl",
                ShortName = "Brawl",
                Description = "Brawl\r\n{Brawl%} {Brl%}\r\nStrength + Pb\r\n%D 2 Speed Dice (Average)\r\n\r\nBrawl is the ability to fight without weapons, it mainly applies for punch and kick attacks. It can only be used within Grapple Range.\r\n\r\nHit: + 1 Damage Die Bludgeoning, add 1 Speed Die to target dice pool, Accuracy\r\nStun: + 1 Speed Die \r\nFeint: +  Gain 1 Defense against Melee and Grapple range attacks\r\nDisarm: + +\r\nKnock Prone: + + +\r\nHaymaker: + + + + 1 Damage Die and 1 Speed Die per Sb, Add 2 Speed Dice to target dice pool\r\nHarm: 1 degree of difference – A Character’s insistence or desperation when using no weapons works against them. They take 1 Damage Die per DoD between the Check of the Character and the Save Throw of their Opponent.\r\n\r\nModifiers to {Brawl%}\r\n -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties",
                HtmlDescription = "Brawl<br/>{Brawl%} {Brl%}<br/>Strength + Pb<br/>%D 2 Speed Dice (Average)<br/><br/>Brawl is the ability to fight without weapons, it mainly applies for punch and kick attacks. It can only be used within Grapple Range.<br/><br/>Hit: + 1 Damage Die Bludgeoning, add 1 Speed Die to target dice pool, Accuracy<br/>Stun: + 1 Speed Die <br/>Feint: +  Gain 1 Defense against Melee and Grapple range attacks<br/>Disarm: + +<br/>Knock Prone: + + +<br/>Haymaker: + + + + 1 Damage Die and 1 Speed Die per Sb, Add 2 Speed Dice to target dice pool<br/>Harm: 1 degree of difference – A Character’s insistence or desperation when using no weapons works against them. They take 1 Damage Die per DoD between the Check of the Character and the Save Throw of their Opponent.<br/><br/>Modifiers to {Brawl%}<br/> -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 5,
                Name = "Calm Other",
                ShortName = "Calm Other",
                Description = "Calm Other\r\n{Calm Other%} {Calm%}\r\nEmpathy + Wb\r\n%D 3 Speed Dice (Involved)\r\n\r\nThis Skill allows a character to bring another character to their senses.\r\n\r\nThis may be used in place of {Diplomacy} against hostile opponents that aren’t immune to {Diplomacy}.\r\n\r\nCalm: + Remove “Panic”, “Fear”, or “Rage”\r\nBig Picture Perspective: + per target Wb – Character can prevent the loss of another character’s Morale.\r\nRally (X): +  Prevent the loss of party Morale while in Formation, where X is equal to the number of + spent on the effect.",
                HtmlDescription = "Calm Other<br/>{Calm Other%} {Calm%}<br/>Empathy + Wb<br/>%D 3 Speed Dice (Involved)<br/><br/>This Skill allows a character to bring another character to their senses.<br/><br/>This may be used in place of {Diplomacy} against hostile opponents that aren’t immune to {Diplomacy}.<br/><br/>Calm: + Remove “Panic”, “Fear”, or “Rage”<br/>Big Picture Perspective: + per target Wb – Character can prevent the loss of another character’s Morale.<br/>Rally (X): +  Prevent the loss of party Morale while in Formation, where X is equal to the number of + spent on the effect.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 6,
                Name = "Climb",
                ShortName = "Climb",
                Description = "Climb\r\n{Climb%} {Clim%}\r\nStrength + Wb\r\n%D 2 Speed Dice (Average)\r\nClimb represents a character’s ability to heft their own weight and their gear as they scale a surface not easily traversed with normal movement. \r\n\r\nClimb:  +  Move up to the Crawling Movement Rate while climbing.\r\nSlip:  - - Add 1 Difficulty Die to the character’s next Dice Pool.\r\nFall: - - - - - A character falls. Falling from a great height will also inflict Damage. Every 10 feet falling will take 1 Damage Die Bludgeoning. Falling on objects or on the character’s own Weapons or Gear may add [Piercing, Slashing] to the Damage types. \r\n\r\nModifiers to {Climb%}\r\n -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties\r\n -Environmental Modifiers: \r\n  -Sheer Surfaces: 9 Difficulty Dice\r\n  -Hand holds (naturally occurring or man-made): Add +\r\n  -Ladders: Add + + +",
                HtmlDescription = "Climb<br/>{Climb%} {Clim%}<br/>Strength + Wb<br/>%D 2 Speed Dice (Average)<br/>Climb represents a character’s ability to heft their own weight and their gear as they scale a surface not easily traversed with normal movement. <br/><br/>Climb:  +  Move up to the Crawling Movement Rate while climbing.<br/>Slip:  - - Add 1 Difficulty Die to the character’s next Dice Pool.<br/>Fall: - - - - - A character falls. Falling from a great height will also inflict Damage. Every 10 feet falling will take 1 Damage Die Bludgeoning. Falling on objects or on the character’s own Weapons or Gear may add [Piercing, Slashing] to the Damage types. <br/><br/>Modifiers to {Climb%}<br/> -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties<br/> -Environmental Modifiers: <br/>  -Sheer Surfaces: 9 Difficulty Dice<br/>  -Hand holds (naturally occurring or man-made): Add +<br/>  -Ladders: Add + + +",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 7,
                Name = "Composure",
                ShortName = "Composure",
                Description = "Composure\r\n{Composure%} {Cmps%}\r\nWillpower + Eb\r\n%D 2 Speed Dice (Average) per - to a minimum of 1 Speed Die.\r\n\r\nComposure represents the ability of the character to keep themselves and others calm, collected, and in control. It allows them to resist manipulation, overcome fear, and manage their psychological state under extreme stress. \r\n\r\nCalm: + Remove “Panic”, “Fear”, or “Rage” can target self, but counts as a Save Throw.\r\nBig Picture Perspective: + per target Wb – Character can prevent the loss of another character’s Morale. This cannot be used during an Encounter to prevent the loss of Morale due to character death.\r\n\r\nConviction: Save Throw – This Save Throw is the ability to resist commands, coercion, bribery and other forms of Diplomacy. Being able to do this well makes a character hard-headed, stubborn, and strong-willed, but being able to resist some forms of Diplomacy is a beneficial ability. To succeed, the Save Throw is an Opposed Check versus the opponent’s {Diplomacy%}. \r\n\r\nResolve: Save Throw – This Save Throw allows a character to overcome the effects of Panic, Fear, or Rage. A character needs to pass this Save Throw only once during an Encounter, when an Encounter begins with Panic-causing or Fear-inducing opponents. Other conditional events that require Save Throws will likewise require only one Save Throw to resist Panic, Fear, or Rage .\r\n\r\nCope: Save Throw – This Save Throw allows a character to prevent the loss of Morale when encountering a Stressor, if no Coping Mechanism is available. A character needs to pass this Save Throw only once during an Encounter, when an Encounter begins with Morale loss inducing opponents. Other conditional events that require Save Throws will likewise require only one Save Throw to prevent loss of Morale.\r\n\r\nModifiers to {Composure%}\r\n -1 Difficulty Dice: A character is under duress and has every reason to be intimidated or afraid.\r\n -2 Difficulty Dice: A character is dealing with someone they trust and is likely to let their guard down or care about the individual in a way that indicates trust. Save throw only.\r\n -3 Difficulty Dice: A character is dealing with a close friend or family who they intrinsically trust. Save throw only.",
                HtmlDescription = "Composure<br/>{Composure%} {Cmps%}<br/>Willpower + Eb<br/>%D 2 Speed Dice (Average) per - to a minimum of 1 Speed Die.<br/><br/>Composure represents the ability of the character to keep themselves and others calm, collected, and in control. It allows them to resist manipulation, overcome fear, and manage their psychological state under extreme stress. <br/><br/>Calm: + Remove “Panic”, “Fear”, or “Rage” can target self, but counts as a Save Throw.<br/>Big Picture Perspective: + per target Wb – Character can prevent the loss of another character’s Morale. This cannot be used during an Encounter to prevent the loss of Morale due to character death.<br/><br/>Conviction: Save Throw – This Save Throw is the ability to resist commands, coercion, bribery and other forms of Diplomacy. Being able to do this well makes a character hard-headed, stubborn, and strong-willed, but being able to resist some forms of Diplomacy is a beneficial ability. To succeed, the Save Throw is an Opposed Check versus the opponent’s {Diplomacy%}. <br/><br/>Resolve: Save Throw – This Save Throw allows a character to overcome the effects of Panic, Fear, or Rage. A character needs to pass this Save Throw only once during an Encounter, when an Encounter begins with Panic-causing or Fear-inducing opponents. Other conditional events that require Save Throws will likewise require only one Save Throw to resist Panic, Fear, or Rage .<br/><br/>Cope: Save Throw – This Save Throw allows a character to prevent the loss of Morale when encountering a Stressor, if no Coping Mechanism is available. A character needs to pass this Save Throw only once during an Encounter, when an Encounter begins with Morale loss inducing opponents. Other conditional events that require Save Throws will likewise require only one Save Throw to prevent loss of Morale.<br/><br/>Modifiers to {Composure%}<br/> -1 Difficulty Dice: A character is under duress and has every reason to be intimidated or afraid.<br/> -2 Difficulty Dice: A character is dealing with someone they trust and is likely to let their guard down or care about the individual in a way that indicates trust. Save throw only.<br/> -3 Difficulty Dice: A character is dealing with a close friend or family who they intrinsically trust. Save throw only.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 4,
                SecondaryAttributeBaseAttributeId = 3
            });

            builder.HasData(new BaseSkill
            {
                Id = 8,
                Name = "Craft/Construct/Engineer",
                ShortName = "Craft/Cons./Eng.",
                Description = "Craft/Construct/Engineer\r\n{Craft/Construct/Engineer%}\r\n{CCE%}\r\nPb + Wb\r\nTime 1\r\n\r\nSpecializations: A character can specialize in crafting using a specific material, manufacturing process or product.(Electronics, Metalworking, Carpentry, Masonry, Leatherworking, etc.) This can be used for routine maintenance of such materials or products as well.\r\n\r\nLabor: This is a character’s ability to manufacture and repair using a specific material or process. Each + in this Check will reduce the Time required for Repair, Maintenance and Construction Missions, depending on certain factors as described:\r\n -Field/Stranger’s Workbench: Time 1 per +\r\n -Workshop, Poor: Time 2 per +\r\n -Workshop: Time 3 per +\r\n -Dedicated Factory: Time 4 per +\r\n\r\nMake note of any gear or tools that may count as bonus generated by a successful {Craft/Construct/Engineer%} Check.",
                HtmlDescription = "Craft/Construct/Engineer<br/>{Craft/Construct/Engineer%}<br/>{CCE%}<br/>Pb + Wb<br/>Time 1<br/><br/>Specializations: A character can specialize in crafting using a specific material, manufacturing process or product.(Electronics, Metalworking, Carpentry, Masonry, Leatherworking, etc.) This can be used for routine maintenance of such materials or products as well.<br/><br/>Labor: This is a character’s ability to manufacture and repair using a specific material or process. Each + in this Check will reduce the Time required for Repair, Maintenance and Construction Missions, depending on certain factors as described:<br/> -Field/Stranger’s Workbench: Time 1 per +<br/> -Workshop, Poor: Time 2 per +<br/> -Workshop: Time 3 per +<br/> -Dedicated Factory: Time 4 per +<br/><br/>Make note of any gear or tools that may count as bonus generated by a successful {Craft/Construct/Engineer%} Check.",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 9,
                Name = "Digital Systems",
                ShortName = "Digital Sys.",
                Description = "Digital Systems\r\n{Digital Systems%} {DgtlSys%}\r\nPerception + Wb\r\nVaries tremendously, typically %D 4 Speed Dice (Intense)\r\nSpecializations: Specific operating system or network.\r\n\r\nThis Skill is a character’s familiarity with digital systems such as data processors, data storage, and networks. It does not deal with undermining, altering, or creating the architecture of a digital system, just the normal functioning as well as maintenance, diagnostics, solutions, and workarounds for common problems encountered during the use of digital systems.\r\n\r\nLabor: + Count as having spent Time 1 to navigate and use a computer or digital network.\r\n\r\nGenerally, a Labor Value of 5 is all that's needed for standard access. The Character can access the system and make use of general user functions. This can be used to launch applications, open common files, and review basic system information.\r\nAdmin Access: + + +  The character can access the system with full Admin rights. This can be used to enable or disable any basic Security, install or remove applications, and review or modify system files.\r\nDeep Search: + + + + +  The character can locate any hidden files that could be of use on the system. Requires “Admin Access” to use. \r\n\r\nMinor Glitch: -  The character encounters a random minor glitch, bug, or issue that prevents any progress. Resolve by adding Time 1 to the amount needed to gain the desired access.\r\nSystem Crash: - - -  The character encounters (or causes) a minor crash that requires a reboot of the system or device. Anything the character was actively working on will be lost.\r\nData Corruption: - - - - -  A critical corruption or system failure (like the notorious “Blue Screen of Death”) occurs preventing the continued use of the system or device until it has been properly repaired or restored. This could result in potential data loss at GM discretion for the cost of Threat 1 for assorted files or Threat 10 to require the system or device to be restored (losing all data not externally backed up).\r\n\r\nThe failure represented by “Data Corruption” is not meant to be a feature of standard operations. This must be in conjunction with digging deep into system files to apply or circumvent some application or firewall that prevents the kind of access a character wishes to have.\r\n\r\nModifiers to {Digital Systems%}\r\n -Security: 1 Difficulty Dice per Security rating of system (this can be a stat included with all digital devices, which could be modified with the Programming specialization of {Sci%} beyond the base rating).",
                HtmlDescription = "Digital Systems<br/>{Digital Systems%} {DgtlSys%}<br/>Perception + Wb<br/>Varies tremendously, typically %D 4 Speed Dice (Intense)<br/>Specializations: Specific operating system or network.<br/><br/>This Skill is a character’s familiarity with digital systems such as data processors, data storage, and networks. It does not deal with undermining, altering, or creating the architecture of a digital system, just the normal functioning as well as maintenance, diagnostics, solutions, and workarounds for common problems encountered during the use of digital systems.<br/><br/>Labor: + Count as having spent Time 1 to navigate and use a computer or digital network.<br/><br/>Generally, a Labor Value of 5 is all that's needed for standard access. The Character can access the system and make use of general user functions. This can be used to launch applications, open common files, and review basic system information.<br/>Admin Access: + + +  The character can access the system with full Admin rights. This can be used to enable or disable any basic Security, install or remove applications, and review or modify system files.<br/>Deep Search: + + + + +  The character can locate any hidden files that could be of use on the system. Requires “Admin Access” to use. <br/><br/>Minor Glitch: -  The character encounters a random minor glitch, bug, or issue that prevents any progress. Resolve by adding Time 1 to the amount needed to gain the desired access.<br/>System Crash: - - -  The character encounters (or causes) a minor crash that requires a reboot of the system or device. Anything the character was actively working on will be lost.<br/>Data Corruption: - - - - -  A critical corruption or system failure (like the notorious “Blue Screen of Death”) occurs preventing the continued use of the system or device until it has been properly repaired or restored. This could result in potential data loss at GM discretion for the cost of Threat 1 for assorted files or Threat 10 to require the system or device to be restored (losing all data not externally backed up).<br/><br/>The failure represented by “Data Corruption” is not meant to be a feature of standard operations. This must be in conjunction with digging deep into system files to apply or circumvent some application or firewall that prevents the kind of access a character wishes to have.<br/><br/>Modifiers to {Digital Systems%}<br/> -Security: 1 Difficulty Dice per Security rating of system (this can be a stat included with all digital devices, which could be modified with the Programming specialization of {Sci%} beyond the base rating).",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 10,
                Name = "Diplomacy <Barter/Bribe>",
                ShortName = "Barter/Bribe",
                Description = "Barter/Bribe\r\n{Diplomacy (Barter/Bribe)%}\r\n{Dipl-BB% or BB%}\r\nEmpathy + Wb\r\n\r\nTo attempt an exchange of goods or services for either a desired item or items, information, a service or amnesty.\r\n\r\nBarter: +  Opposed Check against an NPC’s {Diplomacy (Barter/ Bribe)%}. Each + increases the Value of what a character has to offer for trade.\r\nSales: +  Add an additional 25% to all items brought to market at a Stronghold or other vendor with an Economy. This can stack to gain a maximum bonus of an additional 100%\r\nUpsell: +  Increase Reputation by 1 with a party who is buying or selling from the character.\r\nCall in Favor: +  Reduce loss of Reputation when using the Service of an organization by 1 per + . This reduction is done to a minimum of 1.\r\nBad Sales Pitch: - -  Reduce the Value of what a Character is bartering with by 1.",
                HtmlDescription = "Barter/Bribe<br/>{Diplomacy (Barter/Bribe)%}<br/>{Dipl-BB% or BB%}<br/>Empathy + Wb<br/><br/>To attempt an exchange of goods or services for either a desired item or items, information, a service or amnesty.<br/><br/>Barter: +  Opposed Check against an NPC’s {Diplomacy (Barter/ Bribe)%}. Each + increases the Value of what a character has to offer for trade.<br/>Sales: +  Add an additional 25% to all items brought to market at a Stronghold or other vendor with an Economy. This can stack to gain a maximum bonus of an additional 100%<br/>Upsell: +  Increase Reputation by 1 with a party who is buying or selling from the character.<br/>Call in Favor: +  Reduce loss of Reputation when using the Service of an organization by 1 per + . This reduction is done to a minimum of 1.<br/>Bad Sales Pitch: - -  Reduce the Value of what a Character is bartering with by 1.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 11,
                Name = "Diplomacy <Command>",
                ShortName = "Command",
                Description = "Command\r\n{Diplomacy (Command)%} {Dipl–Cd% or CD%}\r\nEmpathy + Wb\r\n\r\nTo force by means of will or authority to get others to act in accordance with a character’s wishes.\r\n\r\nIssue Orders: + per Wb – Direct an NPC to do what you ask.\r\nRally: +\r\nI’ll take it from here: + + + Character usurps leadership and takes “Point” position in a Formation. Reduce Morale by 1.",
                HtmlDescription = "Command<br/>{Diplomacy (Command)%} {Dipl–Cd% or CD%}<br/>Empathy + Wb<br/><br/>To force by means of will or authority to get others to act in accordance with a character’s wishes.<br/><br/>Issue Orders: + per Wb – Direct an NPC to do what you ask.<br/>Rally: +<br/>I’ll take it from here: + + + Character usurps leadership and takes “Point” position in a Formation. Reduce Morale by 1.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 12,
                Name = "Diplomacy <Determine Motives>",
                ShortName = "Det. Motives",
                Description = "Determine Motives\r\n{Diplomacy (Determine Motives)%}\r\n{Dipl–DM% or DM%}\r\nEmpathy + Pb\r\n\r\nTo see what motivates others by means of analyzing their behavior, rhetorical methods and physical cues during a conversation.\r\n\r\nAnalyze: + Count as having spent Time 1 on “We Know this Much” Missions in regards to living, thinking targets.\r\nDetect Lies: + An opposed check to an opponent’s {Expression%} when they are lying. Add - to the Result of an {Expression%}. Multiple + may be used for this to reflect discerning a more convincing lie.",
                HtmlDescription = "Determine Motives<br/>{Diplomacy (Determine Motives)%}<br/>{Dipl–DM% or DM%}<br/>Empathy + Pb<br/><br/>To see what motivates others by means of analyzing their behavior, rhetorical methods and physical cues during a conversation.<br/><br/>Analyze: + Count as having spent Time 1 on “We Know this Much” Missions in regards to living, thinking targets.<br/>Detect Lies: + An opposed check to an opponent’s {Expression%} when they are lying. Add - to the Result of an {Expression%}. Multiple + may be used for this to reflect discerning a more convincing lie.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 13,
                Name = "Diplomacy <Intimidate>",
                ShortName = "Intimidate",
                Description = "Intimidate\r\n{Diplomacy (Intimidate)%} {Dipl–Int% or Int%}\r\nEmpathy + Sb\r\n\r\nTo force, by threat of violence against the target or another, one to act in accordance with the will of the character.\r\n\r\nStartle: + 1 Speed Die 1 Difficulty Dice\r\nFrighten Off: + per Wb – Cause one Living target to Flee an Encounter.\r\nBreak Will: + + per Wb – -1 Morale.\r\nTerrify: + + + + + 3 Speed Dice 3 Difficulty Dice",
                HtmlDescription = "Intimidate<br/>{Diplomacy (Intimidate)%} {Dipl–Int% or Int%}<br/>Empathy + Sb<br/><br/>To force, by threat of violence against the target or another, one to act in accordance with the will of the character.<br/><br/>Startle: + 1 Speed Die 1 Difficulty Dice<br/>Frighten Off: + per Wb – Cause one Living target to Flee an Encounter.<br/>Break Will: + + per Wb – -1 Morale.<br/>Terrify: + + + + + 3 Speed Dice 3 Difficulty Dice",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 1
            });

            builder.HasData(new BaseSkill
            {
                Id = 14,
                Name = "Diplomacy <Persuade>",
                ShortName = "Persuade",
                Description = "Persuade\r\n{Diplomacy (Persuade)%} {Dipl-P% or P%}\r\nEmpathy + Pb\r\n\r\nTo convince another to act in a way that is the will of the character. This can take the form of begging, pleading, or reasoning.\r\n\r\nRhetoric: +  Count as having spent Time 1 when undertaking Diplomacy Missions.\r\nAppeal: +  Increase starting Morale of an ally NPC by 1.\r\nConviction: + +  Increase starting Morale of a Formation by 1.\r\nThis is Madness: + + +  Prevent the loss of Survival Points when aborting a Mission.\r\nOratory: + + + + +  Alter the attitude of an NPC positively, (Red to Orange or Orange to Blue).\r\nSlip of the Tongue: - - -  Reduce party or ally morale by 1.\r\nGrave Offense: - - - - -  Alter the attitude of an NPC negatively, (Blue to Orange or Orange to Red)",
                HtmlDescription = "Persuade<br/>{Diplomacy (Persuade)%} {Dipl-P% or P%}<br/>Empathy + Pb<br/><br/>To convince another to act in a way that is the will of the character. This can take the form of begging, pleading, or reasoning.<br/><br/>Rhetoric: +  Count as having spent Time 1 when undertaking Diplomacy Missions.<br/>Appeal: +  Increase starting Morale of an ally NPC by 1.<br/>Conviction: + +  Increase starting Morale of a Formation by 1.<br/>This is Madness: + + +  Prevent the loss of Survival Points when aborting a Mission.<br/>Oratory: + + + + +  Alter the attitude of an NPC positively, (Red to Orange or Orange to Blue).<br/>Slip of the Tongue: - - -  Reduce party or ally morale by 1.<br/>Grave Offense: - - - - -  Alter the attitude of an NPC negatively, (Blue to Orange or Orange to Red)",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 15,
                Name = "Dodge",
                ShortName = "Dodge",
                Description = "Dodge{Dodge%} {Do%}\r\nPerception + Sb\r\n%D 2 Speed Dice (Average) for ‘Actions’,\r\n%D 1 Speed Die (Quick) for ‘Save Throws’\r\n\r\nDodge is the ability to move in a way that can mitigate or avoid harm directed at a character.\r\n\r\nDodge: + Add 1 Defense vs. Ranged and Blast attack. Can be used as a Save Throw.\r\nFull Dodge: + + + 4 Speed Dice\r\nMitigate: Save Throw – This Save Throw is made versus an opponent’s “Ranged” or “Blast” attack. After a Ranged attack that causes Damage, a character can attempt a {Dodge%}.\r\nStumble: - - -  Add 1 Difficulty Dice to the next Dice Pool.\r\nOff-Balance: 2 Degrees of Difference - Add 1 Difficulty Dice to the next Dice Pool\r\n\r\nModifiers to {Dodge%}\r\n -Encumbrance: Adds Difficulty Dice and/or Speed Dice penalties.",
                HtmlDescription = "Dodge{Dodge%} {Do%}<br/>Perception + Sb<br/>%D 2 Speed Dice (Average) for ‘Actions’,<br/>%D 1 Speed Die (Quick) for ‘Save Throws’<br/><br/>Dodge is the ability to move in a way that can mitigate or avoid harm directed at a character.<br/><br/>Dodge: + Add 1 Defense vs. Ranged and Blast attack. Can be used as a Save Throw.<br/>Full Dodge: + + + 4 Speed Dice<br/>Mitigate: Save Throw – This Save Throw is made versus an opponent’s “Ranged” or “Blast” attack. After a Ranged attack that causes Damage, a character can attempt a {Dodge%}.<br/>Stumble: - - -  Add 1 Difficulty Dice to the next Dice Pool.<br/>Off-Balance: 2 Degrees of Difference - Add 1 Difficulty Dice to the next Dice Pool<br/><br/>Modifiers to {Dodge%}<br/> -Encumbrance: Adds Difficulty Dice and/or Speed Dice penalties.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 1
            });

            builder.HasData(new BaseSkill
            {
                Id = 16,
                Name = "Endurance",
                ShortName = "Endurance",
                Description = "Endurance\r\n{Endurance%} {End%}\r\nStrength + Wb\r\n1 Time outside of Encounters,\r\n%D 2 Speed Dice (Average) for ‘Save Throws’\r\n\r\nA character’s Endurance is their physical stamina and ability to metabolize toxic material and poisons.\r\n\r\nRun: + Maintain Running Speed for this period of 1 Time.\r\nHitting a Stride: + + + Maintain Running Speed for this period of 1 Time without adding Fatigue.\r\nComplete Exhaustion: - - - - -  A character is so exhausted they must pause and take a Short Rest, during which they can only remove Fatigue.\r\nMetabolize Poison: Save Throw  + Gain Resilience (X) against Poison damage, where X is the number of + spent on this Triggered Effect.\r\nCatastrophe: - - - - -  Resisting Poison Damage with a Catastrophe will Aggravate all existing Damage Dice assigned to the character from [Po] damage to the Poisoned Injury.\r\n\r\nModifiers to {Endurance%}\r\n -Encumbrance: Add Difficulty Dice and/or Speed Dice penalties.*\r\n\r\n -Environmental Modifiers*\r\n -Strength of Poison: When attempting to be metabolized (if specified)\r\n -Sustaining Action: Add 1 Difficulty Dice per 1 Time spent Running + 1 Difficulty Dice per Fatigue\r\n\r\n*Applies to {Endurance%} to maintain speed.",
                HtmlDescription = "Endurance<br/>{Endurance%} {End%}<br/>Strength + Wb<br/>1 Time outside of Encounters,<br/>%D 2 Speed Dice (Average) for ‘Save Throws’<br/><br/>A character’s Endurance is their physical stamina and ability to metabolize toxic material and poisons.<br/><br/>Run: + Maintain Running Speed for this period of 1 Time.<br/>Hitting a Stride: + + + Maintain Running Speed for this period of 1 Time without adding Fatigue.<br/>Complete Exhaustion: - - - - -  A character is so exhausted they must pause and take a Short Rest, during which they can only remove Fatigue.<br/>Metabolize Poison: Save Throw  + Gain Resilience (X) against Poison damage, where X is the number of + spent on this Triggered Effect.<br/>Catastrophe: - - - - -  Resisting Poison Damage with a Catastrophe will Aggravate all existing Damage Dice assigned to the character from [Po] damage to the Poisoned Injury.<br/><br/>Modifiers to {Endurance%}<br/> -Encumbrance: Add Difficulty Dice and/or Speed Dice penalties.*<br/><br/> -Environmental Modifiers*<br/> -Strength of Poison: When attempting to be metabolized (if specified)<br/> -Sustaining Action: Add 1 Difficulty Dice per 1 Time spent Running + 1 Difficulty Dice per Fatigue<br/><br/>*Applies to {Endurance%} to maintain speed.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 17,
                Name = "Expression",
                ShortName = "Expression",
                Description = "Expression\r\n{Expression%} {Exp%}\r\nEmpathy + Pb\r\n%D 3 Speed Dice (Involved)\r\n\r\nThis is a character’s ability to convey information. It requires clarity of message, appropriateness of vocabulary, and skill at manipulating their own emotions and words to convey a performance or communicate a message effectively. Videography and photography as well as artistic pursuits can fall into this category. This also applies to lies, polemics, apologetics and rhetoric.\r\n\r\nSpecializations\r\nEthos (appeals to credibility and character), Pathos (appeals to emotion), Logos (appeals to reason), Artistic medium (photography, videography, writing, painting, drawing etc.)\r\n\r\nConvey Information: + Count as + in a Labor Triggered Effect when another uses this character’s reference to guide their actions (i.e. following their directions, writing a coded message, etc). For example, a character leaves a note for another character to find as to where to meet up later.\r\nLie: +  An opposed check to another’s {Diplomacy-Determine Motives%}. Fool or misdirect an opponent about the nature of your business, the extent of your networks, or generally giving an untruthful response convincingly. Multiple + may be used for this to reflect a more convincing lie.\r\n\r\nDeflect Attention: +  An opposed check to another’s {Spot/Listen%} by using cunning and subterfuge. This character successfully deflects the attention of one target, effectively negating a + of their opponent with a + of their own. \r\n\r\nModifiers to {Expression%} \r\n -Language barrier\r\n -Relationship with target\r\n -Outrageousness of lie (if lying)\r\n -Clarity of media (video and photographs vs the written word)",
                HtmlDescription = "Expression<br/>{Expression%} {Exp%}<br/>Empathy + Pb<br/>%D 3 Speed Dice (Involved)<br/><br/>This is a character’s ability to convey information. It requires clarity of message, appropriateness of vocabulary, and skill at manipulating their own emotions and words to convey a performance or communicate a message effectively. Videography and photography as well as artistic pursuits can fall into this category. This also applies to lies, polemics, apologetics and rhetoric.<br/><br/>Specializations<br/>Ethos (appeals to credibility and character), Pathos (appeals to emotion), Logos (appeals to reason), Artistic medium (photography, videography, writing, painting, drawing etc.)<br/><br/>Convey Information: + Count as + in a Labor Triggered Effect when another uses this character’s reference to guide their actions (i.e. following their directions, writing a coded message, etc). For example, a character leaves a note for another character to find as to where to meet up later.<br/>Lie: +  An opposed check to another’s {Diplomacy-Determine Motives%}. Fool or misdirect an opponent about the nature of your business, the extent of your networks, or generally giving an untruthful response convincingly. Multiple + may be used for this to reflect a more convincing lie.<br/><br/>Deflect Attention: +  An opposed check to another’s {Spot/Listen%} by using cunning and subterfuge. This character successfully deflects the attention of one target, effectively negating a + of their opponent with a + of their own. <br/><br/>Modifiers to {Expression%} <br/> -Language barrier<br/> -Relationship with target<br/> -Outrageousness of lie (if lying)<br/> -Clarity of media (video and photographs vs the written word)",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 18,
                Name = "Firearms <Long Gun>",
                ShortName = "Long Gun",
                Description = "Long Gun\r\n{Firearms (Long Gun)%} {Frm-L%}\r\n\r\nThis involves the use of two-handed firearms, mainly rifles and shotguns, although shorter carbine models are also covered by this skill. Successfully making this Check indicates you have hit your intended target. Many Difficulty penalties may apply to moving targets and those behind cover.\r\n\r\nModifiers to {Firearms%}\r\n -Environmental Modifiers\r\n -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks made)\r\n -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                HtmlDescription = "Long Gun<br/>{Firearms (Long Gun)%} {Frm-L%}<br/><br/>This involves the use of two-handed firearms, mainly rifles and shotguns, although shorter carbine models are also covered by this skill. Successfully making this Check indicates you have hit your intended target. Many Difficulty penalties may apply to moving targets and those behind cover.<br/><br/>Modifiers to {Firearms%}<br/> -Environmental Modifiers<br/> -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks made)<br/> -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 19,
                Name = "Firearms <Pistol>",
                ShortName = "Pistol",
                Description = "Pistol\r\n{Firearms (Pistol)%} {Frm-P%}\r\n\r\nThis involves the use of any model of handgun, including both magazine fed and revolvers. Successfully making this Check indicates you have hit your intended target. Many Difficulty penalties may apply to moving targets and those behind cover.\r\n\r\nModifiers to {Firearms%}\r\n -Environmental Modifiers\r\n -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks made)\r\n -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                HtmlDescription = "Pistol<br/>{Firearms (Pistol)%} {Frm-P%}<br/><br/>This involves the use of any model of handgun, including both magazine fed and revolvers. Successfully making this Check indicates you have hit your intended target. Many Difficulty penalties may apply to moving targets and those behind cover.<br/><br/>Modifiers to {Firearms%}<br/> -Environmental Modifiers<br/> -Multiple Uses: If in the same Round (1 Difficulty Dice per additional Use to all Skill Checks made)<br/> -Multiple Targets: If in the same Round (1 Difficulty Dice per additional target past the first to all Skill Checks made)",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 20,
                Name = "First Aid",
                ShortName = "First Aid",
                Description = "First Aid\r\n{First Aid%} {FAid%}\r\nPerception + Wb\r\nSustained\r\n%D 3 Speed Dice (Involved) + 2 Speed Dice per Health Point lost,\r\nTime 1\r\n\r\nA character can treat minor injuries and to stabilize someone who is wounded.\r\n\r\nFirst Aid: +  [Heal 1 + 1 per + , max 3]\r\nRedress Wound: + per HP lost – Reduce Recovery Time by Time 1. Usable only during periods of Long Rest.\r\nTreat Minor Injury: + + Plus + per HP lost – One Injury is considered successfully Treated. This cannot be used on a character that has an Injury Magnitude greater than Lv.2. Characters acting as physicians to the wounded character can continue to make {First Aid%} checks in order to generate enough + for potentially more Treat Minor Injury Triggered Effects so long as the physician has remaining Treatment Time to re-try.\r\nWound Care: +  Increase Heal value of one character by +1 to a maximum of Heal 5 during Natural Healing\r\nStabilize: +  Increase the window for an Injured character's Treatment Time by Time 1\r\nComplication: -  Reduce an Injured character's Treatment Time by Time 1\r\nAggravate Injury: -  Aggravate 1 Damage Die to an Injury with a Result of 1 per - resolved in this way.\r\nHarm: - - - - -  Deal 1 Damage Die\r\n\r\nModifiers to {First Aid%}\r\n -Environmental Modifiers\r\n -Self-surgery: 2 Difficulty Dice when targeting yourself with {First Aid%}.\r\n -No Supplies: 2 Difficulty Dice when Performing the check without at least a 0x Multiplier of First Aid or Medical gear.",
                HtmlDescription = "First Aid<br/>{First Aid%} {FAid%}<br/>Perception + Wb<br/>Sustained<br/>%D 3 Speed Dice (Involved) + 2 Speed Dice per Health Point lost,<br/>Time 1<br/><br/>A character can treat minor injuries and to stabilize someone who is wounded.<br/><br/>First Aid: +  [Heal 1 + 1 per + , max 3]<br/>Redress Wound: + per HP lost – Reduce Recovery Time by Time 1. Usable only during periods of Long Rest.<br/>Treat Minor Injury: + + Plus + per HP lost – One Injury is considered successfully Treated. This cannot be used on a character that has an Injury Magnitude greater than Lv.2. Characters acting as physicians to the wounded character can continue to make {First Aid%} checks in order to generate enough + for potentially more Treat Minor Injury Triggered Effects so long as the physician has remaining Treatment Time to re-try.<br/>Wound Care: +  Increase Heal value of one character by +1 to a maximum of Heal 5 during Natural Healing<br/>Stabilize: +  Increase the window for an Injured character's Treatment Time by Time 1<br/>Complication: -  Reduce an Injured character's Treatment Time by Time 1<br/>Aggravate Injury: -  Aggravate 1 Damage Die to an Injury with a Result of 1 per - resolved in this way.<br/>Harm: - - - - -  Deal 1 Damage Die<br/><br/>Modifiers to {First Aid%}<br/> -Environmental Modifiers<br/> -Self-surgery: 2 Difficulty Dice when targeting yourself with {First Aid%}.<br/> -No Supplies: 2 Difficulty Dice when Performing the check without at least a 0x Multiplier of First Aid or Medical gear.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 21,
                Name = "Grapple",
                ShortName = "Grapple",
                Description = "Grapple\r\n{Grapple%} {Grpl%}\r\nStrength + Wb\r\n%D 1 Speed Die (Quick)\r\n\r\nThis involves very close quarters fighting, resembling wrestling. This is used at Grapple Range and is an Opposed Check. At this Range, the mistakes made by an opponent can potentially be more vital than one’s own abilities (though dumb luck is never a viable replacement for skill).\r\n\r\nBreak Hold: + + or 1 Degree of Difference – Break any Clinched character Status Effect.\r\nChoke Hold: + + 2 Speed Dice per consecutive Round on Living Targets. Target is Clinched and Prone\r\nJoint Lock: + + or 1 DoD 1 Damage Die Bludgeoning, 0 Def. Target is Clinched, Accuracy\r\nLethal Leverage: + + + or 2 DoD and a Clinched Target – 4 Damage Dice Bludgeoning, 0 Def\r\nParry: +\r\nPin: + + + or 2 DoD and a Prone target – 4 Speed Dice and 2 Difficulty Dice. Both the character and opponent will count as being Prone in the next Round. \r\nTakedown: + + + or 2 DoD – Target is Prone\r\n\r\nModifiers to {Grapple%}\r\n- Environmental Modifiers",
                HtmlDescription = "Grapple<br/>{Grapple%} {Grpl%}<br/>Strength + Wb<br/>%D 1 Speed Die (Quick)<br/><br/>This involves very close quarters fighting, resembling wrestling. This is used at Grapple Range and is an Opposed Check. At this Range, the mistakes made by an opponent can potentially be more vital than one’s own abilities (though dumb luck is never a viable replacement for skill).<br/><br/>Break Hold: + + or 1 Degree of Difference – Break any Clinched character Status Effect.<br/>Choke Hold: + + 2 Speed Dice per consecutive Round on Living Targets. Target is Clinched and Prone<br/>Joint Lock: + + or 1 DoD 1 Damage Die Bludgeoning, 0 Def. Target is Clinched, Accuracy<br/>Lethal Leverage: + + + or 2 DoD and a Clinched Target – 4 Damage Dice Bludgeoning, 0 Def<br/>Parry: +<br/>Pin: + + + or 2 DoD and a Prone target – 4 Speed Dice and 2 Difficulty Dice. Both the character and opponent will count as being Prone in the next Round. <br/>Takedown: + + + or 2 DoD – Target is Prone<br/><br/>Modifiers to {Grapple%}<br/>- Environmental Modifiers",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 22,
                Name = "Hold",
                ShortName = "Hold",
                Description = "Hold\r\n{Hold%} {Hld%}\r\nWillpower + Pb\r\n%D 3 Speed Dice (Involved)\r\n\r\nA character waits for the opportune moment to make their action.\r\n\r\nAim: +\r\nHold: + Remove 1 Speed Die from the character’s next Dice Pool to a minimum of 1 Speed Die.\r\nReact: + Generate + for the character in the following Round.\r\nWell Timed: +  Use an attack Triggered Effect against an Engaged target and add the “Instant” rule to that attack.\r\nOverwatch: +  Characters get the Rush 1 per + when using {Bow/ Crossbow%, Throw} or Efficient rule per + if they make {Firearms%} during their Intent phase.\r\nPerfect Timing: + + Generate + + + for the character in the following Round to spend on Triggered Effects that target an opponent.\r\n\r\nModifiers to {Hold%}\r\n -1 Difficulty Dice per point of Fatigue\r\n -Characters with 0 Morale or who are Panicked are unable to perform a ‘Hold’ action.",
                HtmlDescription = "Hold<br/>{Hold%} {Hld%}<br/>Willpower + Pb<br/>%D 3 Speed Dice (Involved)<br/><br/>A character waits for the opportune moment to make their action.<br/><br/>Aim: +<br/>Hold: + Remove 1 Speed Die from the character’s next Dice Pool to a minimum of 1 Speed Die.<br/>React: + Generate + for the character in the following Round.<br/>Well Timed: +  Use an attack Triggered Effect against an Engaged target and add the “Instant” rule to that attack.<br/>Overwatch: +  Characters get the Rush 1 per + when using {Bow/ Crossbow%, Throw} or Efficient rule per + if they make {Firearms%} during their Intent phase.<br/>Perfect Timing: + + Generate + + + for the character in the following Round to spend on Triggered Effects that target an opponent.<br/><br/>Modifiers to {Hold%}<br/> -1 Difficulty Dice per point of Fatigue<br/> -Characters with 0 Morale or who are Panicked are unable to perform a ‘Hold’ action.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 4,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 23,
                Name = "Jump/Leap",
                ShortName = "Jump/Leap",
                Description = "Jump/Leap\r\n{Jump/Leap%} {J/L%}\r\nStrength + Pb\r\n%D (Instant)\r\n%D 1 Speed Die (Quick)\r\n\r\nA character can jump vertically or horizontally across obstacles.\r\n\r\nStick Landing: + Successfully make and land the attempted jump or leap.\r\nFumble: -  The character either lands in an awkward fashion or is hanging precariously. Add 2 Speed Dice to the Dice Pool, and the character may require a {Climb%} at the Gamemaster’s discretion, based on circumstances.\r\nPlummet: - - - - -  Character is “Prone” from a hard landing. If the fall was from a considerable height the character may also suffer DmdD for Falling Damage (see page 95) as normal.\r\n\r\nModifiers to {Jump/Leap%}\r\n -Distance: Add 1 Difficulty Dice for every 25% of the character's height for vertical or for every 100% of the character’s height for horizontal jumps.\r\n -Encumbrance",
                HtmlDescription = "Jump/Leap<br/>{Jump/Leap%} {J/L%}<br/>Strength + Pb<br/>%D (Instant)<br/>%D 1 Speed Die (Quick)<br/><br/>A character can jump vertically or horizontally across obstacles.<br/><br/>Stick Landing: + Successfully make and land the attempted jump or leap.<br/>Fumble: -  The character either lands in an awkward fashion or is hanging precariously. Add 2 Speed Dice to the Dice Pool, and the character may require a {Climb%} at the Gamemaster’s discretion, based on circumstances.<br/>Plummet: - - - - -  Character is “Prone” from a hard landing. If the fall was from a considerable height the character may also suffer DmdD for Falling Damage (see page 95) as normal.<br/><br/>Modifiers to {Jump/Leap%}<br/> -Distance: Add 1 Difficulty Dice for every 25% of the character's height for vertical or for every 100% of the character’s height for horizontal jumps.<br/> -Encumbrance",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 24,
                Name = "Lift/Pull",
                ShortName = "Lift/Pull",
                Description = "Lift/Pull\r\n{Lift/Pull%} {L/P%}\r\nStrength + Wb\r\n%D 1 Speed Die per 5 Cargo lifted or pulled beyond Sb to a minimum of 1 Speed Die.\r\n\r\nA character can lift, pull, or push heavy objects.\r\n\r\nLifting: +  5 Cargo per Sb can be lifted to a maximum of 3xSb.\r\nPulling/Pushing: + 10 Cargo per Sb (max. 5xSb) can be pulled or pushed at a character’s normal walking speed.\r\n\r\nModifiers to {Lift/Pull%}\r\n -Encumbrance\r\n -Fatigue",
                HtmlDescription = "Lift/Pull<br/>{Lift/Pull%} {L/P%}<br/>Strength + Wb<br/>%D 1 Speed Die per 5 Cargo lifted or pulled beyond Sb to a minimum of 1 Speed Die.<br/><br/>A character can lift, pull, or push heavy objects.<br/><br/>Lifting: +  5 Cargo per Sb can be lifted to a maximum of 3xSb.<br/>Pulling/Pushing: + 10 Cargo per Sb (max. 5xSb) can be pulled or pushed at a character’s normal walking speed.<br/><br/>Modifiers to {Lift/Pull%}<br/> -Encumbrance<br/> -Fatigue",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 25,
                Name = "Martial Arts",
                ShortName = "Martial Arts",
                Description = "Martial Arts\r\n{Martial Arts%} {MtlA%}\r\nSb + Pb\r\n%D 1 Speed Die (Quick)\r\n\r\n{Martial Arts%} are similar to {Grapple%} or {Brawl%} only with a reduced Speed Dice and with a generally lower chance of success without exceptional training. Succeeding in this Check allows characters to use any Triggered Effects listed for either {Grapple%} or {Brawl%}. All Triggered Effects have “Accuracy” as a rule if they do not already. \r\n\r\nSpecializations: A specific form of martial arts can be specialized in.\r\n\r\nSave Throw: This can be used as a Save Throw for most combat checks.",
                HtmlDescription = "Martial Arts<br/>{Martial Arts%} {MtlA%}<br/>Sb + Pb<br/>%D 1 Speed Die (Quick)<br/><br/>{Martial Arts%} are similar to {Grapple%} or {Brawl%} only with a reduced Speed Dice and with a generally lower chance of success without exceptional training. Succeeding in this Check allows characters to use any Triggered Effects listed for either {Grapple%} or {Brawl%}. All Triggered Effects have “Accuracy” as a rule if they do not already. <br/><br/>Specializations: A specific form of martial arts can be specialized in.<br/><br/>Save Throw: This can be used as a Save Throw for most combat checks.",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 26,
                Name = "Melee Attack <Bludgeoning>",
                ShortName = "Bludgeoning",
                Description = "Bludgeoning\r\n{Melee Attack (Bludgeoning)%}\r\n{MA-Bl%}\r\nStrength + Pb\r\nVaries, but typically 1 Speed Die per 5 encumbrance per attack.\r\n\r\nThis involves the use of impact melee weapons. Items like clubs, batons, staves, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target.\r\n\r\nModifiers to {Melee Attack%}\r\n -Encumbrance: Adds Difficulty Dice and/or Speed Dice\r\n -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.\r\n -Environmental Modifiers\r\n -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                HtmlDescription = "Bludgeoning<br/>{Melee Attack (Bludgeoning)%}<br/>{MA-Bl%}<br/>Strength + Pb<br/>Varies, but typically 1 Speed Die per 5 encumbrance per attack.<br/><br/>This involves the use of impact melee weapons. Items like clubs, batons, staves, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target.<br/><br/>Modifiers to {Melee Attack%}<br/> -Encumbrance: Adds Difficulty Dice and/or Speed Dice<br/> -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.<br/> -Environmental Modifiers<br/> -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 27,
                Name = "Melee Attack <Piercing>",
                ShortName = "Piercing",
                Description = "Piercing\r\n{Melee Attack (Piercing)%} {MAPi%}\r\nStrength + Pb\r\nVaries, but typically 1 Speed Die per 5 encumbrance per attack.\r\n\r\nThis involves the use of stabbing melee weapons. Items like knives, picks, swords, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target. Many weapons of this type also work as Slashing Weapons.\r\n\r\nModifiers to {Melee Attack%}\r\n -Encumbrance: Adds Difficulty Dice and/or Speed Dice\r\n -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.\r\n -Environmental Modifiers\r\n -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                HtmlDescription = "Piercing<br/>{Melee Attack (Piercing)%} {MAPi%}<br/>Strength + Pb<br/>Varies, but typically 1 Speed Die per 5 encumbrance per attack.<br/><br/>This involves the use of stabbing melee weapons. Items like knives, picks, swords, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target. Many weapons of this type also work as Slashing Weapons.<br/><br/>Modifiers to {Melee Attack%}<br/> -Encumbrance: Adds Difficulty Dice and/or Speed Dice<br/> -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.<br/> -Environmental Modifiers<br/> -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 28,
                Name = "Melee Attack <Slashing>",
                ShortName = "Slashing",
                Description = "Slashing\r\n{Melee Attack (Slashing)%} {MASl%}\r\nStrength + Pb\r\nVaries, but typically 1 Speed Die per 5 encumbrance per attack.\r\n\r\nThis involves the use of cutting melee weapons. Items like knives, scalpels, swords, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target. Many weapons of this type also work as Piercing Weapons.\r\n\r\nModifiers to {Melee Attack%}\r\n -Encumbrance: Adds Difficulty Dice and/or Speed Dice\r\n -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.\r\n -Environmental Modifiers\r\n -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                HtmlDescription = "Slashing<br/>{Melee Attack (Slashing)%} {MASl%}<br/>Strength + Pb<br/>Varies, but typically 1 Speed Die per 5 encumbrance per attack.<br/><br/>This involves the use of cutting melee weapons. Items like knives, scalpels, swords, and more are also covered by this Skill. Successfully making this Check indicates you have hit your intended target. Many weapons of this type also work as Piercing Weapons.<br/><br/>Modifiers to {Melee Attack%}<br/> -Encumbrance: Adds Difficulty Dice and/or Speed Dice<br/> -Injury: 1 Difficulty Dice and 1 Speed Die per Health Point lost.<br/> -Environmental Modifiers<br/> -Obstruction: 1 Difficulty Dice per feature that obscures vision or restricts freedom of movement, such as confined spaces.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 2
            });

            builder.HasData(new BaseSkill
            {
                Id = 29,
                Name = "Navigation",
                ShortName = "Navigation",
                Description = "Navigation\r\n{Navigation%} {Nav%}\r\nPerception + Wb\r\nTime 1\r\n\r\nSpecialization: Specific Region, Biome: Alpine, Chaparral, Desert, Glacier, Rainforest, Savanna, Taiga/Boreal Forest, Temperate Forest, Temperate Grassland, Tundra, Coral Reef, Estuaries, Marine Characters can navigate terrain and avoid potential hazards. The Check without any modifiers assumes natural navigational ability with no landmarks or signs. This will rarely be the case, save for the most desolate, disorienting, alien, or featureless of terrains or locations.\r\n\r\nEscape: +  A character finds an escape route for themselves and/or nearby characters during an Encounter. They must move to it in order to flee.\r\nCover Tracks: + +  A character negates any Threat that would be generated by fleeing.\r\nEvade Pursuers: + + + A character can avoid any opponents that are “Out of Bounds” as they flee.\r\nPerfect Escape: + + + + +  A character negates any Threat that would be generated by fleeing and will prevent the GM from getting a free Encounter%. \r\nWrong Turn: - or Threat 5 – The GM can add a + to any E% they make after the characters flee. This can be used multiple times, each time resolving a - as a + for the E%.\r\nNavigate: +  Increase Pb or Wb of character by +1 when determining how far a character can Travel during a period of Time. This is increased to +5 in Urban areas.\r\nClear Path: + + + + +  A {Navigation%} is accomplished without requiring an additional Encounter check to be rolled afterwards.\r\nCatastrophe: - - - - - – Characters think they are successfully avoiding danger, but in reality they have been detected and opponents are sneaking around in order to surprise them or laying traps ahead of them. Add Threat d5!\r\n\r\nNavigation checks using d5! will have the following additional effects:\r\n -Lost: Add Threat 5 per Difficulty Dice rolled if any ! result.\r\n -Trapped: - - - - -  Make an additional E% as the character puts themselves and their party in peril with poor directions.\r\n -Misstep: Threat 1  Each - in a {Navigation%} will count as a + in a GM’s {E%}\r\n\r\nRecognizable Landmarks\r\n+ : Clearly marked directional signs or trail markers\r\n+ + : Emergency Track Lighting\r\n+ + + : Temporary Signage to indicate status of road\r\n\r\nModifiers to {Navigation%}\r\n -A character that is intimately familiar with the area can navigate without difficulty.\r\n   1 Difficulty Dice: A character has a lot of experience in the area but rarely goes off the beaten path.\r\n   2 Difficulty Dice: A character has some experience in the area but never ventures too far off main thoroughfares. The area is familiar, but characters don't know many alternate routes.\r\n   3 Difficulty Dice: A character knows only the area by a loose knowledge of landmarks.\r\n   4 Difficulty Dice: A character finds the area foreign and only navigates with tremendous difficulty.\r\n -1 Difficulty Dice per Environmental Modifier for features that obscure vision, such as fog, or greatly restrict one or more form of travel, such as a rainstorm.\r\n -Difficulty Penalties due to Injuries also impede Navigation.",
                HtmlDescription = "Navigation<br/>{Navigation%} {Nav%}<br/>Perception + Wb<br/>Time 1<br/><br/>Specialization: Specific Region, Biome: Alpine, Chaparral, Desert, Glacier, Rainforest, Savanna, Taiga/Boreal Forest, Temperate Forest, Temperate Grassland, Tundra, Coral Reef, Estuaries, Marine Characters can navigate terrain and avoid potential hazards. The Check without any modifiers assumes natural navigational ability with no landmarks or signs. This will rarely be the case, save for the most desolate, disorienting, alien, or featureless of terrains or locations.<br/><br/>Escape: +  A character finds an escape route for themselves and/or nearby characters during an Encounter. They must move to it in order to flee.<br/>Cover Tracks: + +  A character negates any Threat that would be generated by fleeing.<br/>Evade Pursuers: + + + A character can avoid any opponents that are “Out of Bounds” as they flee.<br/>Perfect Escape: + + + + +  A character negates any Threat that would be generated by fleeing and will prevent the GM from getting a free Encounter%. <br/>Wrong Turn: - or Threat 5 – The GM can add a + to any E% they make after the characters flee. This can be used multiple times, each time resolving a - as a + for the E%.<br/>Navigate: +  Increase Pb or Wb of character by +1 when determining how far a character can Travel during a period of Time. This is increased to +5 in Urban areas.<br/>Clear Path: + + + + +  A {Navigation%} is accomplished without requiring an additional Encounter check to be rolled afterwards.<br/>Catastrophe: - - - - - – Characters think they are successfully avoiding danger, but in reality they have been detected and opponents are sneaking around in order to surprise them or laying traps ahead of them. Add Threat d5!<br/><br/>Navigation checks using d5! will have the following additional effects:<br/> -Lost: Add Threat 5 per Difficulty Dice rolled if any ! result.<br/> -Trapped: - - - - -  Make an additional E% as the character puts themselves and their party in peril with poor directions.<br/> -Misstep: Threat 1  Each - in a {Navigation%} will count as a + in a GM’s {E%}<br/><br/>Recognizable Landmarks<br/>+ : Clearly marked directional signs or trail markers<br/>+ + : Emergency Track Lighting<br/>+ + + : Temporary Signage to indicate status of road<br/><br/>Modifiers to {Navigation%}<br/> -A character that is intimately familiar with the area can navigate without difficulty.<br/>   1 Difficulty Dice: A character has a lot of experience in the area but rarely goes off the beaten path.<br/>   2 Difficulty Dice: A character has some experience in the area but never ventures too far off main thoroughfares. The area is familiar, but characters don't know many alternate routes.<br/>   3 Difficulty Dice: A character knows only the area by a loose knowledge of landmarks.<br/>   4 Difficulty Dice: A character finds the area foreign and only navigates with tremendous difficulty.<br/> -1 Difficulty Dice per Environmental Modifier for features that obscure vision, such as fog, or greatly restrict one or more form of travel, such as a rainstorm.<br/> -Difficulty Penalties due to Injuries also impede Navigation.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 30,
                Name = "Pilot",
                ShortName = "Pilot",
                Description = "Pilot\r\n{Pilot%} {Pil%}\r\nPb + Wb\r\nVaries\r\n\r\nSpecializations: A character can specialize in the class of vehicle being piloted.\r\n\r\nTravel: +  Increase the Wb or Pb of a character by 1 when determining how far a vehicle can be used to Travel during a period of Time with this character at the wheel.\r\nManeuver: + Avoid a road hazard that costs up to Threat per + . This includes deploying Opponents.\r\n\r\nModifiers to {Pilot%}\r\n -Environmental Modifiers",
                HtmlDescription = "Pilot<br/>{Pilot%} {Pil%}<br/>Pb + Wb<br/>Varies<br/><br/>Specializations: A character can specialize in the class of vehicle being piloted.<br/><br/>Travel: +  Increase the Wb or Pb of a character by 1 when determining how far a vehicle can be used to Travel during a period of Time with this character at the wheel.<br/>Maneuver: + Avoid a road hazard that costs up to Threat per + . This includes deploying Opponents.<br/><br/>Modifiers to {Pilot%}<br/> -Environmental Modifiers",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 31,
                Name = "Resist Pain",
                ShortName = "Resist Pain",
                Description = "Resist Pain\r\n{Resist Pain%} {RPai%}\r\nWillpower + Sb\r\n%D (Instant),\r\n%D 1 Speed Die for ‘Save Throw’ per Health Point lost\r\n\r\nA character can outright resist or temporarily suppress crippling pain by sheer force of will, sometimes with the aid of painkilling drugs.\r\n\r\nWork Through It: + Ignore Pain or 1 Health Point loss worth of effects determined by the Injury they have.This effect lasts until the character is forced to “Aggravate” an Injury in any way, or until the following day for Chronic Pain.\r\nTreatment: + Increase the character's Window of Treatment Time by 1 Time if they are Injured.\r\n\r\nModifiers to {Resist Pain%}\r\n -Injury: Add 1 Difficulty Dice per Health Point lost.",
                HtmlDescription = "Resist Pain<br/>{Resist Pain%} {RPai%}<br/>Willpower + Sb<br/>%D (Instant),<br/>%D 1 Speed Die for ‘Save Throw’ per Health Point lost<br/><br/>A character can outright resist or temporarily suppress crippling pain by sheer force of will, sometimes with the aid of painkilling drugs.<br/><br/>Work Through It: + Ignore Pain or 1 Health Point loss worth of effects determined by the Injury they have.This effect lasts until the character is forced to “Aggravate” an Injury in any way, or until the following day for Chronic Pain.<br/>Treatment: + Increase the character's Window of Treatment Time by 1 Time if they are Injured.<br/><br/>Modifiers to {Resist Pain%}<br/> -Injury: Add 1 Difficulty Dice per Health Point lost.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 4,
                SecondaryAttributeBaseAttributeId = 1

            });

            builder.HasData(new BaseSkill
            {
                Id = 32,
                Name = "Ride",
                ShortName = "Ride",
                Description = "Ride\r\n{Ride%} {Rid%}\r\nEb + Wb\r\nVaries\r\n\r\nSpecializations: A character can specialize in the animal being ridden.\r\n\r\nA character is able to ride an animal.\r\n\r\nRide: + Add Morale 1 to the “Animal Fellowship – Ride” Formation. This can be attempted once per Time 1\r\nCalm Animal: + 2 Speed Dice – Prevent the loss of 1 Morale to the “Animal Fellowship” Formation.\r\nSpook: -  Remove 1 Morale from the “Animal Fellowship” Formation.\r\nOrnery: - - - - -  Break “Animal Fellowship” Formation.",
                HtmlDescription = "Ride<br/>{Ride%} {Rid%}<br/>Eb + Wb<br/>Varies<br/><br/>Specializations: A character can specialize in the animal being ridden.<br/><br/>A character is able to ride an animal.<br/><br/>Ride: + Add Morale 1 to the “Animal Fellowship – Ride” Formation. This can be attempted once per Time 1<br/>Calm Animal: + 2 Speed Dice – Prevent the loss of 1 Morale to the “Animal Fellowship” Formation.<br/>Spook: -  Remove 1 Morale from the “Animal Fellowship” Formation.<br/>Ornery: - - - - -  Break “Animal Fellowship” Formation.",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 3,
                SecondaryAttributeBaseAttributeId = 4
            }); builder.HasData(new BaseSkill
            {
                Id = 33,
                Name = "Science",
                ShortName = "Science",
                Description = "Science\r\n{Science%} {Sci%}\r\nPb + Wb\r\nTime 1 for analysis\r\n%D 4 Speed Dice (Intense) for quick observations.\r\nDetailed research can be multiple periods of Time.\r\n\r\nA character’s Science is their comprehension of the scientific method as well as current understanding of a specific field of science and its application. This is also a measure of deductive and inductive reasoning as it relates to a specific field.\r\n\r\nLabor: For the purposes of creating/modifying something or performing Research, most of those sorts of Missions require a Labor Value . Each + in an appropriate {Science%} Check will count as a certain amount of Time spent towards the completion of that mission. There are multiple situations that alter this rate upon a successful {Sci%} Check, as described:\r\n -Field Observation, Identification: Time 1 for Research only\r\n -Laboratory, Poor: Time 2 per +\r\n -Laboratory: Time 3 per +\r\n -Dedicated Research Facility: Time 4 per +\r\n\r\nMake note of any gear or tools that may help add to the amount of Time a successful {Science%} Check counts towards as well.\r\n\r\nModifiers to {Science%}\r\nScience is modified by the method used, accessibility to proper equipment, and the field’s current understanding of the subject.\r\n -Standard Testing Method: No modifier\r\n -Impromptu Testing Method: 2 Difficulty Dice\r\n -Routine Observation/Data Analysis/Repeated Testing Procedure: No modifier\r\n -Significant Observation: 2 Difficulty Dice\r\n -New Frontier of Discovery: 3 Difficulty Dice\r\n -Discovery completely new to Science: 4 Difficulty Dice\r\n -Proper Tools/Equipment: No modifier\r\n -Improper Tools/Equipment: 1 Difficulty Dice",
                HtmlDescription = "Science<br/>{Science%} {Sci%}<br/>Pb + Wb<br/>Time 1 for analysis<br/>%D 4 Speed Dice (Intense) for quick observations.<br/>Detailed research can be multiple periods of Time.<br/><br/>A character’s Science is their comprehension of the scientific method as well as current understanding of a specific field of science and its application. This is also a measure of deductive and inductive reasoning as it relates to a specific field.<br/><br/>Labor: For the purposes of creating/modifying something or performing Research, most of those sorts of Missions require a Labor Value . Each + in an appropriate {Science%} Check will count as a certain amount of Time spent towards the completion of that mission. There are multiple situations that alter this rate upon a successful {Sci%} Check, as described:<br/> -Field Observation, Identification: Time 1 for Research only<br/> -Laboratory, Poor: Time 2 per +<br/> -Laboratory: Time 3 per +<br/> -Dedicated Research Facility: Time 4 per +<br/><br/>Make note of any gear or tools that may help add to the amount of Time a successful {Science%} Check counts towards as well.<br/><br/>Modifiers to {Science%}<br/>Science is modified by the method used, accessibility to proper equipment, and the field’s current understanding of the subject.<br/> -Standard Testing Method: No modifier<br/> -Impromptu Testing Method: 2 Difficulty Dice<br/> -Routine Observation/Data Analysis/Repeated Testing Procedure: No modifier<br/> -Significant Observation: 2 Difficulty Dice<br/> -New Frontier of Discovery: 3 Difficulty Dice<br/> -Discovery completely new to Science: 4 Difficulty Dice<br/> -Proper Tools/Equipment: No modifier<br/> -Improper Tools/Equipment: 1 Difficulty Dice",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 34,
                Name = "Search",
                ShortName = "Search",
                Description = "Search\r\n{Search%} {Srch%}\r\nPerception + Wb\r\nVaries\r\n\r\nCharacters can search for people as well as useful objects. This Check represents a very thorough search of an area.\r\n\r\nScrounge: +  Survivors count as having spent an additional 1 Time searching a Location. Characters can pool their + together if they are collectively searching in this way.\r\nConfound: - - When Searching a location characters must spend an additional 1 Time when doing so to unlock what is available at Resource Level.\r\n\r\nModifiers to {Search%}\r\n -Environmental Modifiers (Darkness, Noise): 1 Difficulty Dice per Environmental Modifier. Some gear can offset this penalty.",
                HtmlDescription = "Search<br/>{Search%} {Srch%}<br/>Perception + Wb<br/>Varies<br/><br/>Characters can search for people as well as useful objects. This Check represents a very thorough search of an area.<br/><br/>Scrounge: +  Survivors count as having spent an additional 1 Time searching a Location. Characters can pool their + together if they are collectively searching in this way.<br/>Confound: - - When Searching a location characters must spend an additional 1 Time when doing so to unlock what is available at Resource Level.<br/><br/>Modifiers to {Search%}<br/> -Environmental Modifiers (Darkness, Noise): 1 Difficulty Dice per Environmental Modifier. Some gear can offset this penalty.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 35,
                Name = "Spot/Listen",
                ShortName = "Spot/Listen",
                Description = "Spot/Listen\r\n{Spot/Listen%} {Spt/Li%}\r\nPerception + Wb\r\n%D 1 Speed Die (Quick)\r\n\r\nThis is a superficial scan of an area. Although this implies vision, it is also used for listening, and smell. This generally covers what can be detected with only a few moments worth of effort.\r\n\r\nDetect: + Remove “Hidden” status to one target within 10' per Pb for each + .\r\nFind: +  Survivors count as having spent Time 1 searching a Location. Multiple + can be spent on this and each one will count as an additional Time 1 spent searching. This can only be used to find things at Resource Level 1.\r\n\r\nModifiers to {Spot/Listen%}\r\n -Environmental Modifiers (Darkness, Noise):  1 Difficulty Dice per Environmental Modifier.\r\n -Some gear can offset this penalty.",
                HtmlDescription = "Spot/Listen<br/>{Spot/Listen%} {Spt/Li%}<br/>Perception + Wb<br/>%D 1 Speed Die (Quick)<br/><br/>This is a superficial scan of an area. Although this implies vision, it is also used for listening, and smell. This generally covers what can be detected with only a few moments worth of effort.<br/><br/>Detect: + Remove “Hidden” status to one target within 10' per Pb for each + .<br/>Find: +  Survivors count as having spent Time 1 searching a Location. Multiple + can be spent on this and each one will count as an additional Time 1 spent searching. This can only be used to find things at Resource Level 1.<br/><br/>Modifiers to {Spot/Listen%}<br/> -Environmental Modifiers (Darkness, Noise):  1 Difficulty Dice per Environmental Modifier.<br/> -Some gear can offset this penalty.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 36,
                Name = "Stealth",
                ShortName = "Stealth",
                Description = "Stealth\r\n{Stealth%} {Stlh%}\r\nPerception + Wb \r\nTime 1 outside of Encounter,\r\n%D 3 Speed Dice (Involved) for 'Actions'\r\n%D 1 Speed Die per Pb of the opponent for Opposed Check\r\n\r\nStealth allows a character or party of characters to avoid danger by hiding from view and moving quietly. Difficulties may arise when trying to succeed against opponents with advanced sensory organs or those that use some scanning device. \r\n\r\nEvade: +  Reduce the result of an E% by + or count as Time 1 towards Lay Low Missions.\r\nSneak: +  A “Hidden” character can move at a Crawl pace and remain “Hidden”. Add 1 Speed Die to the Dice Pool for movement at that rate normally.\r\nDistract: + Add 1 Speed Die to target. This is increased to 2 Speed Dice if user is Hidden.\r\nHide: + The character or one object is “Hidden”.\r\nEvasive Maneuvers: +  Reduce the Risk Pool of the GM by W 1.\r\nAmbush: + + + + + or 4 Degrees of Difference – The character gets a Surprise Round to any opponents deployed in an Encounter.\r\nSpotted: - - - - -  A character is spotted and isn’t aware of it. All opponents get a Surprise Round against the character. \r\n\r\nModifiers to {Stealth%}\r\n -Evasion: To avoid Encounters, add 1 Difficulty Dice per Pb of the opponents being hidden from.\r\n -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties\r\n -Environmental Modifiers - Many Environmental modifiers can reduce Difficulty Dice or add bonus + , such as Darkness.",
                HtmlDescription = "Stealth<br/>{Stealth%} {Stlh%}<br/>Perception + Wb <br/>Time 1 outside of Encounter,<br/>%D 3 Speed Dice (Involved) for 'Actions'<br/>%D 1 Speed Die per Pb of the opponent for Opposed Check<br/><br/>Stealth allows a character or party of characters to avoid danger by hiding from view and moving quietly. Difficulties may arise when trying to succeed against opponents with advanced sensory organs or those that use some scanning device. <br/><br/>Evade: +  Reduce the result of an E% by + or count as Time 1 towards Lay Low Missions.<br/>Sneak: +  A “Hidden” character can move at a Crawl pace and remain “Hidden”. Add 1 Speed Die to the Dice Pool for movement at that rate normally.<br/>Distract: + Add 1 Speed Die to target. This is increased to 2 Speed Dice if user is Hidden.<br/>Hide: + The character or one object is “Hidden”.<br/>Evasive Maneuvers: +  Reduce the Risk Pool of the GM by W 1.<br/>Ambush: + + + + + or 4 Degrees of Difference – The character gets a Surprise Round to any opponents deployed in an Encounter.<br/>Spotted: - - - - -  A character is spotted and isn’t aware of it. All opponents get a Surprise Round against the character. <br/><br/>Modifiers to {Stealth%}<br/> -Evasion: To avoid Encounters, add 1 Difficulty Dice per Pb of the opponents being hidden from.<br/> -Encumbrance: will add Difficulty Dice and/or Speed Dice penalties<br/> -Environmental Modifiers - Many Environmental modifiers can reduce Difficulty Dice or add bonus + , such as Darkness.",
                Type = "Basic",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 37,
                Name = "Survival",
                ShortName = "Survival",
                Description = "Survival\r\n{Survival%} {Srvl%}\r\nPb + Wb\r\nTime 1\r\n\r\nThe character can survive without the support of a functioning society in the wilderness. The characters can find food, water, and shelter. They are also able to make tools out of available materials.\r\n\r\nSpecializations: Biome – Alpine, Chaparral, Desert, Glacier, Rainforest, Savanna, Taiga/Boreal Forest, Temperate Forest, Temperate Grassland, Tundra, Coral Reef, Estuaries, Marine\r\n\r\nChart Course: +  Increase Pb or Wb by 1 when determining how far a character can travel through the wilderness during a period of Time.\r\nSurvival: +  Start fire without matches, create trail marker, Provide the food and water needs (a Ration) for one character.\r\nLabor: +  Reduce Time needed for a “Set up Camp, Bushcraft” or some similar Mission by Time 1.\r\nBushcraft: Varies – Craft simple tool or weapon of Cargo 3 or less using available materials. Such crafted materials count as “Crude” so would have a 0x Multiplier and a low Durability and be subject to “Degradation”.\r\n\r\nNote: Advanced toolmaking can be undertaken with “Construct Simple Gear”.\r\n\r\nSave Throw: This can be used as a Save Throw against an E% to determine if a character can survive as described above.\r\n\r\nModifiers to {Survival%}\r\n -Environmental Modifiers",
                HtmlDescription = "Survival<br/>{Survival%} {Srvl%}<br/>Pb + Wb<br/>Time 1<br/><br/>The character can survive without the support of a functioning society in the wilderness. The characters can find food, water, and shelter. They are also able to make tools out of available materials.<br/><br/>Specializations: Biome – Alpine, Chaparral, Desert, Glacier, Rainforest, Savanna, Taiga/Boreal Forest, Temperate Forest, Temperate Grassland, Tundra, Coral Reef, Estuaries, Marine<br/><br/>Chart Course: +  Increase Pb or Wb by 1 when determining how far a character can travel through the wilderness during a period of Time.<br/>Survival: +  Start fire without matches, create trail marker, Provide the food and water needs (a Ration) for one character.<br/>Labor: +  Reduce Time needed for a “Set up Camp, Bushcraft” or some similar Mission by Time 1.<br/>Bushcraft: Varies – Craft simple tool or weapon of Cargo 3 or less using available materials. Such crafted materials count as “Crude” so would have a 0x Multiplier and a low Durability and be subject to “Degradation”.<br/><br/>Note: Advanced toolmaking can be undertaken with “Construct Simple Gear”.<br/><br/>Save Throw: This can be used as a Save Throw against an E% to determine if a character can survive as described above.<br/><br/>Modifiers to {Survival%}<br/> -Environmental Modifiers",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 38,
                Name = "Swim",
                ShortName = "Swim",
                Description = "Swim\r\n{Swim%}\r\nStrength + Wb\r\nVaries, generally\r\n%D 2 Speed Dice (Average) + 1 Speed Die per Environmental Modifier\r\n\r\nThis represents a character's ability to swim and maneuver underwater.\r\n\r\nStroke: + per 5 Cargo carried – The character can move at Crawling pace.\r\nTread Water: -  The character is unable to move, but has managed to at least stay afloat.\r\nGoing Under: - - - - -  The character struggles, being pulled under the surface unable to get air and taking 1 Damage Die per Round. This continues until gaining a + on another {Swim%} (this is treated as a Save Throw and will not allow movement).\r\n\r\nModifiers to {Swim%}\r\n -Encumbrance: Adds Difficulty Dice and/or Speed Dice\r\n -Environmental Modifiers",
                HtmlDescription = "Swim<br/>{Swim%}<br/>Strength + Wb<br/>Varies, generally<br/>%D 2 Speed Dice (Average) + 1 Speed Die per Environmental Modifier<br/><br/>This represents a character's ability to swim and maneuver underwater.<br/><br/>Stroke: + per 5 Cargo carried – The character can move at Crawling pace.<br/>Tread Water: -  The character is unable to move, but has managed to at least stay afloat.<br/>Going Under: - - - - -  The character struggles, being pulled under the surface unable to get air and taking 1 Damage Die per Round. This continues until gaining a + on another {Swim%} (this is treated as a Save Throw and will not allow movement).<br/><br/>Modifiers to {Swim%}<br/> -Encumbrance: Adds Difficulty Dice and/or Speed Dice<br/> -Environmental Modifiers",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });

            builder.HasData(new BaseSkill
            {
                Id = 39,
                Name = "Throw",
                ShortName = "Throw",
                Description = "Throw\r\n{Throw%} {Thrw%}\r\nPerception + Sb\r\nVaries, generally (1 Speed Die per 5 Cargo)\r\n\r\nSpecialization: Knife, Axe, Throwing Star, Sling, Launcher (grenade launchers and mortars) Blunt (Stones, Bricks, etc), Grenade, Incendiary (Molotov Cocktails of varying size)\r\n\r\nThis is a character’s ability to hit their target at a distance, with throwing weapons. It also involves weapons that lob or fling their ammunition, such as slings and grenade launchers.\r\n\r\nDistract: + 1 Speed Die. Depending on what is used, it can apply to a single target or all targets within audible range. Doing this while Hidden will add 1 Speed Die per + .\r\nCook Grenade: + Opponents require an extra + to counter “Blast” Triggered Effect from Grenades with {Dodge%}, requiring + + to count as a single + on the Save Throw. This carries some obvious risks to the thrower, so it adds 2 Speed Dice to the Dice Pool and 1 Difficulty Dice to their next action.",
                HtmlDescription = "Throw<br/>{Throw%} {Thrw%}<br/>Perception + Sb<br/>Varies, generally (1 Speed Die per 5 Cargo)<br/><br/>Specialization: Knife, Axe, Throwing Star, Sling, Launcher (grenade launchers and mortars) Blunt (Stones, Bricks, etc), Grenade, Incendiary (Molotov Cocktails of varying size)<br/><br/>This is a character’s ability to hit their target at a distance, with throwing weapons. It also involves weapons that lob or fling their ammunition, such as slings and grenade launchers.<br/><br/>Distract: + 1 Speed Die. Depending on what is used, it can apply to a single target or all targets within audible range. Doing this while Hidden will add 1 Speed Die per + .<br/>Cook Grenade: + Opponents require an extra + to counter “Blast” Triggered Effect from Grenades with {Dodge%}, requiring + + to count as a single + on the Save Throw. This carries some obvious risks to the thrower, so it adds 2 Speed Dice to the Dice Pool and 1 Difficulty Dice to their next action.",
                Type = "Trained",
                PrimaryAttributeBaseAttributeId = 2,
                SecondaryAttributeBaseAttributeId = 1
            });

            builder.HasData(new BaseSkill
            {
                Id = 40,
                Name = "Toughness",
                ShortName = "Toughness",
                Description = "Toughness\r\n{Toughness%} {Tgh%}\r\nSb + Wb\r\nInstant\r\n%D 4 Speed Dice (Intense) for Save Throws or Speed Dice penalty based upon Injury being resisted.\r\n\r\nA character’s Toughness is their physical resolve and durability. When a character is required to make a Check using their Toughness, it is to spare themselves from death after having taken massive damage or to buy extra time in order to get proper treatment for Injuries they sustain.\r\n\r\nAbsorb Damage: +  Gain Resistance 1 +1 per + spent until the end of the round.\r\nTreatment: +  Add Time 3 to the character's Treatment Window if they are Injured.\r\nSave Throw: Making a Save Throw using {Toughness%} means that a character takes the full force of an injury but somehow shrugs it off.",
                HtmlDescription = "Toughness<br/>{Toughness%} {Tgh%}<br/>Sb + Wb<br/>Instant<br/>%D 4 Speed Dice (Intense) for Save Throws or Speed Dice penalty based upon Injury being resisted.<br/><br/>A character’s Toughness is their physical resolve and durability. When a character is required to make a Check using their Toughness, it is to spare themselves from death after having taken massive damage or to buy extra time in order to get proper treatment for Injuries they sustain.<br/><br/>Absorb Damage: +  Gain Resistance 1 +1 per + spent until the end of the round.<br/>Treatment: +  Add Time 3 to the character's Treatment Window if they are Injured.<br/>Save Throw: Making a Save Throw using {Toughness%} means that a character takes the full force of an injury but somehow shrugs it off.",
                Type = "Expert",
                PrimaryAttributeBaseAttributeId = 1,
                SecondaryAttributeBaseAttributeId = 4
            });
        }
    }

}
