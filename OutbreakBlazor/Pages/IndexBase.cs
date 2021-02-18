using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorStrap;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OutbreakBlazor.Services;
using OutbreakModels.Models;

namespace OutbreakBlazor.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IBaseAbilityService BaseAbilityService { get; set; }
        public IEnumerable<BaseAbility> BaseAbilities { get; set; }

        [Inject]
        public IBaseAttributeService BaseAttributeService { get; set; }
        public IEnumerable<BaseAttribute> BaseAttributes { get; set; }

        [Inject]
        public IBaseSkillService BaseSkillService { get; set; }
        public IEnumerable<BaseSkill> BaseSkills { get; set; }

        [Inject]
        public IBaseTrainingValueService BaseTrainingValueService { get; set; }
        public IEnumerable<BaseTrainingValue> BaseTrainingValues { get; set; }

        [Inject]
        public IPlayerCharacterService PlayerCharacterService { get; set; }
        public IEnumerable<PlayerCharacter> PlayerCharacters { get; set; }

        [Inject]
        public IPlayerAbilityService PlayerAbilityService { get; set; }
        public IEnumerable<PlayerAbility> PlayerAbilities { get; set; }

        [Inject]
        public IPlayerSkillService PlayerSkillService { get; set; }
        public IEnumerable<PlayerSkill> PlayerSkills { get; set; }

        [Inject]
        public IPlayerAttributeService PlayerAttributeService { get; set; }
        public IEnumerable<PlayerAttribute> PlayerAttributes { get; set; }

        protected NavigationManager Navigator;

        protected bool _createNew;
        protected bool _addAbilities;
        protected bool _addSkills;
        protected bool HasInstruction;
        protected int InitialValue;
        protected int FinalValue;
        protected int Delta;

        protected override async Task OnInitializedAsync()
        {
            BaseAbilities = (await BaseAbilityService.GetBaseAbilities()).ToArray();
            BaseAttributes = (await BaseAttributeService.GetBaseAttributes()).ToArray();
            BaseSkills = (await BaseSkillService.GetBaseSkills()).ToArray();
            BaseTrainingValues = (await BaseTrainingValueService.GetTrainingValues()).ToArray();
            PlayerCharacters = (await PlayerCharacterService.GetPlayerCharacters()).ToArray();

            foreach (var ability in BaseAbilities)
            {
                if (ability.ModifiesTrainingValuesCoded != null)
                {
                    var searchValues = CsvStringToArray(ability.ModifiesTrainingValuesCoded);

                    foreach (var value in searchValues)
                    {
                        ability.ModifiesBaseTrainingValues.Add(BaseTrainingValues.FirstOrDefault(t => t.Name == value.ToString()));
                    }

                    foreach (var item in ability.ModifiesBaseTrainingValues)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
            }

        }

        public class HelperClass
        {
            public string FormString;
            public string FormString2;
        }

        public PlayerCharacter ThisCharacter = new PlayerCharacter();
        protected BaseAbility ThisBaseAbility = new BaseAbility();
        protected BaseSkill ThisBaseSkill = new BaseSkill();
        protected PlayerAbility ThisPlayerAbility = new PlayerAbility(){BaseAbility = new BaseAbility()};
        protected PlayerAttribute ThisPlayerAttribute = new PlayerAttribute() {BaseAttribute = new BaseAttribute()};

        protected PlayerSkill ThisPlayerSkill = new PlayerSkill()
        {
            BaseSkill = new BaseSkill()
        };

        protected HelperClass Helper = new HelperClass();
        public string CharacterSheetLocation;

        protected PlayerAttribute StrengthService { get; set; }
        protected PlayerAttribute PerceptionService { get; set; }
        protected PlayerAttribute EmpathyService { get; set; }
        protected PlayerAttribute WillpowerService { get; set; }

        protected async Task HandleNewCharacterClick()
        {
            _createNew = !_createNew;

            ThisCharacter.Age = 30;

            var strength = new PlayerAttribute
            {
                Value = 30,
                BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Strength"),
                Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(strength);
            StrengthService = strength;

            var perception = new PlayerAttribute
            {
                Value = 30,
                BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Perception"),
                Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(perception);
            PerceptionService = perception;

            var empathy = new PlayerAttribute
            {
                Value = 30,
                BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Empathy"),
                Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(empathy);
            EmpathyService = empathy;

            var willpower = new PlayerAttribute
            {
                Value = 30,
                BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Willpower"),
                Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(willpower);
            WillpowerService = willpower;

            ThisCharacter.DamageThreshold = strength.Bonus + willpower.Bonus;
            ThisCharacter.Morale = empathy.Bonus + willpower.Bonus;
            ThisCharacter.CargoCapacity = strength.Bonus;
            ThisCharacter.SurvivalPoints = 25;

            foreach (var skill in BaseSkills)
            {
                InitializePlayerSkills(skill);
            }

            foreach (var trainingValue in BaseTrainingValues)
            {
                InitializePlayerTrainingValues(trainingValue);
            }

            SetGestalt();

            foreach (var ability in BaseAbilities)
            {
                if (ability.UsesBaseAttributesCoded.Contains("S"))
                {
                    ability.UsesBaseAttributes.Add(StrengthService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("P"))
                {
                    ability.UsesBaseAttributes.Add(PerceptionService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("E"))
                {
                    ability.UsesBaseAttributes.Add(EmpathyService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("W"))
                {
                    ability.UsesBaseAttributes.Add(WillpowerService.BaseAttribute);
                }
            }

            ThisCharacter = await PlayerCharacterService.CreatePlayerCharacter(ThisCharacter);
        }

        protected async Task<PlayerCharacter> HandleOnValidPlayerCharacterSubmit()
        {
            //Sync the attribute services with the attributes in the list
            //Sync Strength
            var strength = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength");
            strength.Value = StrengthService.Value;
            strength.Points = StrengthService.Points;
            strength.Notes = StrengthService.Notes;
            //Sync Perception
            var perception = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception");
            perception.Value = PerceptionService.Value;
            perception.Points = PerceptionService.Points;
            perception.Notes = PerceptionService.Notes;
            //Sync Empathy
            var empathy = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy");
            empathy.Value = EmpathyService.Value;
            empathy.Points = EmpathyService.Points;
            empathy.Notes = EmpathyService.Notes;
            //Sync Willpower
            var willpower = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower");
            willpower.Value = WillpowerService.Value;
            willpower.Points = WillpowerService.Points;
            willpower.Notes = WillpowerService.Notes;
            //Update secondary characteristics
            ThisCharacter.DamageThreshold = strength.Bonus + willpower.Bonus;
            ThisCharacter.Morale = empathy.Bonus + willpower.Bonus;
            ThisCharacter.CargoCapacity = strength.Bonus;

            _addAbilities = true;

            return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
        }

        protected async Task<PlayerAttribute> HandleIncrementPlayerAttribute(PlayerAttribute attribute)
        {
            var rand = new Random();
            var inListAttribute =
                ThisCharacter.PlayerAttributes.FirstOrDefault(a =>
                    a.BaseAttribute.Name == attribute.BaseAttribute.Name);
            var valueToRemove = 0;

            if (InitialValue < attribute.Value)
            {
                var advanceValue = 0;
                advanceValue = rand.Next(1, 4);
                ThisCharacter.GestaltLevel -= InitialValue / 10;
                attribute.Value = InitialValue + advanceValue;
                inListAttribute.Value = attribute.Value;
                attribute.AdvancementValues.Add(advanceValue);
                inListAttribute.AdvancementValues = attribute.AdvancementValues;
                if (attribute.Points < attribute.Bonus)
                {
                    attribute.Points = attribute.Bonus;
                    inListAttribute.Points = inListAttribute.Bonus;
                    if (ThisCharacter.PlayerAbilities != null)
                    {
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            if (ability.AddedUsingBaseAttributeCode == attribute.BaseAttribute.Name)
                            {
                                attribute.Points -= 1;
                                inListAttribute.Points -= 1;
                            }
                        }
                    }

                }
            }
            else if (InitialValue > attribute.Value)
            {
                if (attribute.AdvancementValues.Count > 0)
                {
                    valueToRemove = attribute.AdvancementValues[^1];
                    attribute.Value = InitialValue - valueToRemove;
                    inListAttribute.Value = attribute.Value;
                    attribute.AdvancementValues.Remove(attribute.AdvancementValues[^1]);
                    inListAttribute.AdvancementValues = attribute.AdvancementValues;
                    ThisCharacter.GestaltLevel += attribute.Bonus;
                }
                if (attribute.Bonus < InitialValue / 10)
                {
                    attribute.Points -= 1;
                    inListAttribute.Points -= 1;
                }

            }
            else
            {
                attribute.Value = InitialValue;
                inListAttribute.Value = InitialValue;
            }

            if (inListAttribute.Id == 0)
            {
                return await PlayerAttributeService.CreatePlayerAttribute(inListAttribute);
            }

            foreach (var skill in ThisCharacter.PlayerSkills)
            {
                var primaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.PrimaryAttributeBaseAttributeId);
                var secondaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.SecondaryAttributeBaseAttributeId);

                if (primaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (skill.BaseSkill.Type == "Basic" || skill.BaseSkill.Type == "Trained")
                    {
                        if (InitialValue < attribute.Value)
                        {
                            skill.Value += attribute.AdvancementValues[^1];
                        }
                        else
                        {
                            skill.Value -= valueToRemove;
                        }
                    }

                    else if (skill.BaseSkill.Type == "Expert")
                    {
                        if (InitialValue / 10 > attribute.Value / 10)
                        {
                            skill.Value -= 1;
                        }
                        else if (InitialValue / 10 < attribute.Value / 10)
                        {
                            skill.Value += 1;
                        }
                    }
                }
                else if (secondaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (InitialValue / 10 > attribute.Value / 10)
                    {
                        skill.Value -= 1;
                    }
                    else if (InitialValue / 10 < attribute.Value / 10)
                    {
                        skill.Value += 1;
                    }
                }
            }

            InitialValue = attribute.Value;
            return await PlayerAttributeService.UpdatePlayerAttribute(inListAttribute.Id, inListAttribute);
        }

        protected async Task HandleOnValidBaseAbilitySubmit()
        {
            if (!string.IsNullOrEmpty(Helper.FormString))
            {
                var tempAbility = new PlayerAbility
                {
                    BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString)), Tier = 1
                };

                if (tempAbility.BaseAbility.Description.Contains("Skill Support: {"))
                {
                    var end = tempAbility.BaseAbility.Description.IndexOf("}");

                    var rawSkillNames = tempAbility.BaseAbility.Description.Remove(end);
                    rawSkillNames = rawSkillNames.Remove(0, rawSkillNames.IndexOf("{") + 1);

                    var SkillNames = new List<string>();
                    var FinalList = new List<string>();

                    while (rawSkillNames.Length > 0)
                    {
                        var tempString = rawSkillNames.Remove(rawSkillNames.IndexOf('%'));
                        SkillNames.Add(tempString);
                        tempString = "";
                        rawSkillNames = rawSkillNames.Remove(0, rawSkillNames.IndexOf('%') + 1);
                    }

                    foreach (var name in SkillNames)
                    {
                        if (name.Contains(','))
                        {
                            FinalList.Add(name.Remove(0, 2));
                        }

                        if (name.Contains('('))
                        {
                            var position = name.IndexOf("(");
                            FinalList.Add(name.Remove(position-1));
                        }
                        else
                        {
                            FinalList.Add(name);
                        }
                    }

                    foreach (var name in FinalList)
                    {
                        foreach (var skill in ThisCharacter.PlayerSkills)
                        {
                            if (skill.BaseSkill.Name == name)
                            {
                                skill.IsSupported = true;
                                tempAbility.SupportsPlayerSkills.Add(skill);
                            }
                        }
                    }
                }

                tempAbility = await PlayerAbilityService.CreatePlayerAbility(tempAbility);

                if (tempAbility.AddedUsingBaseAttributeCode == null)
                {
                    if (tempAbility.BaseAbility.UsesBaseAttributes.Count == 1)
                    {
                        tempAbility.AddedUsingBaseAttributeCode = tempAbility.BaseAbility.UsesBaseAttributes[0].Name;
                        onPlayerAbilitySingleAttributeAddSpendSelectionToggleOn(tempAbility);
                    }
                    else
                    {
                        onPlayerAbilityToggleOn(tempAbility);
                    }
                }

                ThisCharacter.PlayerAbilities.Add(tempAbility);
            }
        }

        protected async Task<PlayerAbility> HandleOnValidPlayerAbilitySubmit()
        {
            Delta = FinalValue - InitialValue;
            UpdateGestalt();
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
        }

        protected async Task<PlayerAbility> HandleIncrementPlayerAbility(PlayerAbility ability)
        {
            if (ability.Tier == 6)
            {
                ability.Tier = 5;
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            if (ability.Tier > InitialValue)
            {
                onPlayerAbilitySpendSelectionToggleOn(ability);

                if (ThisCharacter.TrainingValues != null)
                {
                    foreach (var trainingValue in ThisCharacter.TrainingValues)
                    {
                        foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                        {
                            if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                            {
                                trainingValue.Value += 1;
                            }
                        }
                    }
                }
            }
            else if (ability.Tier < InitialValue)
            {
                if (ability.Tier == 0)
                {
                    ability.Tier = 1;
                    return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
                }

                if (ability.AdvancedUsing.Count > 0)
                {
                    if (ability.AdvancedUsing[^1] == "gestalt")
                    {
                        ThisCharacter.GestaltLevel += (ability.Tier + 1);
                        ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                    }

                    else if (ability.AdvancedUsing[^1] == "gestaltDouble")
                    {
                        ThisCharacter.GestaltLevel += (ability.Tier + 1)*2;
                        ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                    }

                    else if (ability.AdvancedUsing[^1] == "points" || ability.AdvancedUsing[^1] == "pointsDouble")
                    {
                        ThisCharacter.PlayerAttributes.FirstOrDefault(a =>
                            a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode).Points += 1;
                        ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                    }
                }
                
                
                if (ThisCharacter.TrainingValues != null)
                {
                    foreach (var trainingValue in ThisCharacter.TrainingValues)
                    {
                        foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                        {
                            if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                            {
                                trainingValue.Value -= 1;
                            }
                        }
                    }
                }
            }

            InitialValue = ability.Tier;

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }

        protected async Task<PlayerAbility> HandleIncreasePlayerAbility(PlayerAbility ability)
        {
            if (ability.Tier == 5)
            {
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            onPlayerAbilitySpendSelectionToggleOn(ability);

            ability.Tier += 1;

            if (ThisCharacter.TrainingValues == null)
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);

            foreach (var trainingValue in ThisCharacter.TrainingValues)
            {
                foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                {
                    if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                    {
                        trainingValue.Value += 1;
                    }
                }
            }

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }

        protected async Task<PlayerAbility> HandleDecreasePlayerAbility(PlayerAbility ability)
        {
            if (ability.Tier == 1)
            {
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            if (ability.AdvancedUsing.Count > 0)
            {
                if (ability.AdvancedUsing[^1] == "gestalt")
                {
                    ThisCharacter.GestaltLevel += (ability.Tier);
                    ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                }

                else if (ability.AdvancedUsing[^1] == "gestaltDouble")
                {
                    ThisCharacter.GestaltLevel += (ability.Tier) * 2;
                    ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                }

                else if (ability.AdvancedUsing[^1] == "points" || ability.AdvancedUsing[^1] == "pointsDouble")
                {
                    ThisCharacter.PlayerAttributes.FirstOrDefault(a =>
                        a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode).Points += 1;
                    ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                }

                ability.Tier -= 1;
            }

            if (ThisCharacter.TrainingValues != null)
            {
                foreach (var trainingValue in ThisCharacter.TrainingValues)
                {
                    foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                    {
                        if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                        {
                            trainingValue.Value -= 1;
                        }
                    }
                }
            }

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }

        protected void DeletePlayerAbility(PlayerAbility ability)
        {
            ThisCharacter.PlayerAbilities.Remove(ability);
            var attribute = ThisCharacter.PlayerAttributes
                .FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);
            for (int i = ability.AdvancedUsing.Count-1; i > -1; i--)
            {
                if (ability.AdvancedUsing[i] == "gestalt")
                {
                    if (i == 0)
                    {
                        ThisCharacter.GestaltLevel += (5 - attribute.Bonus);
                    }
                    else
                    {
                        ThisCharacter.GestaltLevel += ability.Tier;
                    }
                }
                else if (ability.AdvancedUsing[i] == "gestaltDouble")
                {
                    if (i == 0)
                    {
                        ThisCharacter.GestaltLevel += (5 - attribute.Bonus)*2;
                    }
                    else
                    {
                        ThisCharacter.GestaltLevel += ability.Tier*2;
                    }
                }
                else if (ability.AdvancedUsing[i] == "points" || ability.AdvancedUsing[i] == "pointsDouble")
                {
                    attribute.Points += 1;
                }

                ability.Tier -= 1;
            }

            ability.AdvancedUsing = new List<string>();
        }

        protected void InitializePlayerSkills(BaseSkill skill)
        {
            var playerSkill = new PlayerSkill();
            if (skill.Type == "Expert")
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1 || skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                    playerSkill.AttributeValue += StrengthService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2 || skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                    playerSkill.AttributeValue += PerceptionService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3 || skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                    playerSkill.AttributeValue += EmpathyService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4 || skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                    playerSkill.AttributeValue += WillpowerService.Bonus;
                }
            }
            else
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Value;
                    playerSkill.AttributeValue += StrengthService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Value;
                    playerSkill.AttributeValue += PerceptionService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Value;
                    playerSkill.AttributeValue += EmpathyService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Value;
                    playerSkill.AttributeValue += WillpowerService.Value;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                    playerSkill.AttributeValue += StrengthService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                    playerSkill.AttributeValue += PerceptionService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                    playerSkill.AttributeValue += EmpathyService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                    playerSkill.AttributeValue += WillpowerService.Bonus;
                }
            }

            playerSkill.BaseSkill = skill;

            if (skill.Name == "Diplomacy <Barter/Bribe>" || 
                skill.Name == "Diplomacy <Command>" || 
                skill.Name == "Diplomacy <Determine Motives>" || 
                skill.Name == "Diplomacy <Intimidate>" || 
                skill.Name == "Diplomacy <Persuade>")
            {
                var specialty = skill.Name.Remove(0, 11);
                specialty = specialty.Remove(specialty.Length - 1);
                playerSkill.Specialty = specialty;
                playerSkill.IsSpecialized = true;
            }

            ThisCharacter.PlayerSkills.Add(playerSkill);
        }

        protected async Task<PlayerSkill> HandleIncrementPlayerSkill(PlayerSkill skill)
        {
            if (skill.Advancements < 0)
            {
                skill.Advancements = 0;
                skill.Value = InitialValue;
            }
            if (skill.Value > InitialValue)
            {
                var totalAdvancement = 0;

                if (skill.BaseSkill.Type == "Basic")
                {
                    if (skill.IsSupported)
                    {
                        var roll = RollD5("Highest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
                        }
                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        skill.Value = InitialValue + roll;
                        skill.AdvancementsList.Add(roll);
                    }
                }

                else if (skill.BaseSkill.Type == "Trained")
                {
                    if (skill.IsSpecialized)
                    {
                        var roll = RollD5("Highest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        if (skill.IsSupported)
                        {
                            foreach (var ability in ThisCharacter.PlayerAbilities)
                            {
                                foreach (var playerSkill in ability.SupportsPlayerSkills)
                                {
                                    if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                    {
                                        if (ability.BaseAbility.AdvancesSkills)
                                        {
                                            totalAdvancement += ability.Tier;
                                        }
                                    }
                                }
                            }
                        }
                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else if (skill.IsSupported)
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
                        }
                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        var roll = RollD5("Lowest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        skill.Value = InitialValue + roll;
                        skill.AdvancementsList.Add(roll);
                    }
                }

                else if (skill.BaseSkill.Type == "Expert")
                {
                    if (skill.IsSpecialized)
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        if (skill.IsSupported)
                        {
                            foreach (var ability in ThisCharacter.PlayerAbilities)
                            {
                                foreach (var playerSkill in ability.SupportsPlayerSkills)
                                {
                                    if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                    {
                                        if (ability.BaseAbility.AdvancesSkills)
                                        {
                                            totalAdvancement += ability.Tier;
                                        }
                                    }
                                }
                            }
                        }

                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    if (skill.IsSupported)
                    {
                        var roll = RollD5("Lowest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
                        }
                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        skill.Value = InitialValue;
                    }
                }

                if (skill.Advancements > 5)
                {
                    var lastAdvancement = skill.AdvancementsList[^1];
                    ThisCharacter.GestaltLevel += 1;
                    skill.Advancements -= 1;
                    skill.Value -= lastAdvancement;
                    skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
                }
            }

            else if (skill.Value < InitialValue && skill.AdvancementsList.Count != 0)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value = InitialValue - lastAdvancement;
                skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
            }

            else
            {
                skill.Value = InitialValue;
            }

            InitialValue = skill.Value;

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }

        protected async Task<PlayerSkill> HandleIncreasePlayerSkill(PlayerSkill skill)
        {
            InitialValue = skill.Value;

            var totalAdvancement = 0;

            if (skill.AdvancementsList.Count==5)
            {
                return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
            }

            if (skill.BaseSkill.Type == "Basic")
            {
                if (skill.IsSupported)
                {
                    var roll = RollD5("Highest");
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    totalAdvancement += roll;
                    foreach (var ability in ThisCharacter.PlayerAbilities)
                    {
                        foreach (var playerSkill in ability.SupportsPlayerSkills)
                        {
                            if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                            {
                                if (ability.BaseAbility.AdvancesSkills)
                                {
                                    totalAdvancement += ability.Tier;
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                }
                else
                {
                    var roll = RollD5();
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    skill.Value = InitialValue + roll;
                    skill.AdvancementsList.Add(roll);
                }
            }

            else if (skill.BaseSkill.Type == "Trained")
            {
                if (skill.IsSpecialized)
                {
                    var roll = RollD5("Highest");
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    totalAdvancement += roll;
                    if (skill.IsSupported)
                    {
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                }
                else if (skill.IsSupported)
                {
                    var roll = RollD5();
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    totalAdvancement += roll;
                    foreach (var ability in ThisCharacter.PlayerAbilities)
                    {
                        foreach (var playerSkill in ability.SupportsPlayerSkills)
                        {
                            if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                            {
                                if (ability.BaseAbility.AdvancesSkills)
                                {
                                    totalAdvancement += ability.Tier;
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                }
                else
                {
                    var roll = RollD5("Lowest");
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    skill.Value = InitialValue + roll;
                    skill.AdvancementsList.Add(roll);
                }
            }

            else if (skill.BaseSkill.Type == "Expert")
            {
                var roll = 0;
                if (skill.IsSpecialized && skill.IsSupported)
                {
                    roll = RollD5();
                }
                else if (skill.IsSupported)
                {
                    roll = RollD5("Lowest");
                }
                else
                {
                    return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
                }

                ThisCharacter.GestaltLevel -= 1;
                skill.Advancements += 1;
                totalAdvancement += roll;

                foreach (var ability in ThisCharacter.PlayerAbilities)
                {
                    foreach (var playerSkill in ability.SupportsPlayerSkills)
                    {
                        if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                        {
                            if (ability.BaseAbility.AdvancesSkills)
                            {
                                totalAdvancement += ability.Tier;
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(skill.Specialty))
                    {
                        if (ability.BaseAbility.Name == "BMX")
                        {
                            if (skill.Specialty.ToLower() == "bicycle" || 
                                skill.Specialty.ToLower() == "bicycle" || 
                                skill.Specialty.ToLower() == "bike" || 
                                skill.Specialty.ToLower() == "bikes")
                            {
                                totalAdvancement += ability.Tier;
                            }
                        }
                        else if (ability.BaseAbility.Name == "Biker")
                        {
                            if (skill.Specialty.ToLower() == "motorcycle" || 
                                skill.Specialty.ToLower() == "motorcycles" || 
                                skill.Specialty.ToLower() == "dirt bike" || 
                                skill.Specialty.ToLower() == "dirt bikes")
                            {
                                totalAdvancement += ability.Tier;
                            }
                        }
                        else if (ability.BaseAbility.Name == "Training, Vehicle/Vessel")
                        {
                            if (skill.BaseSkill.Name == "Pilot")
                            {
                                totalAdvancement += ability.Tier;
                            }
                        }
                    }
                    
                }

                skill.Value = InitialValue + totalAdvancement;
                skill.AdvancementsList.Add(totalAdvancement);
            }

            if (skill.Advancements > 5)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value -= lastAdvancement;
                skill.AdvancementsList.Remove(lastAdvancement);
            }

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }

        protected async Task<PlayerSkill> HandleDecreasePlayerSkill(PlayerSkill skill)
        {
            InitialValue = skill.Value;

            if (skill.AdvancementsList.Count>0)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value = InitialValue - lastAdvancement;
                skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
            }

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }

        protected void HandleSpecializeSkill(PlayerSkill skill)
        {
            var newPlayerSkill = new PlayerSkill
            {
                BaseSkill = skill.BaseSkill,
                Value = skill.Value,
                IsSupported = skill.IsSupported,
                Advancements = skill.Advancements,
                AdvancementsList = skill.AdvancementsList,
                PlayerCharacter = skill.PlayerCharacter,
                Notes = skill.Notes,
                AttributeValue = skill.AttributeValue,
                IsSpecialized = true
            };

            ThisCharacter.GestaltLevel -= skill.AdvancementsList.Count;

            ThisPlayerSkill = newPlayerSkill;

            onSpecializePlayerSkillToggleOn(ThisPlayerSkill);
        }

        protected void HandleRemovePlayerSkill(PlayerSkill skill)
        {
            ThisCharacter.SpecializedPlayerSkills.Remove(skill);
            ThisCharacter.GestaltLevel += skill.AdvancementsList.Count;
        }

        protected void InitializePlayerTrainingValues(BaseTrainingValue value)
        {
            var tempPlayerTrainingValue = new PlayerTrainingValue { BaseTrainingValue = value, Value = 0 };
            ThisCharacter.TrainingValues.Add(tempPlayerTrainingValue);
        }

        protected int RollD5(string type = "Default")
        {
            var rand = new Random();
            var rolls = new List<int>();

            var roll1 = rand.Next(1, 7);
            if (roll1 == 6)
            {
                roll1 = 5;
            }
            rolls.Add(roll1);

            var roll2 = rand.Next(1, 7);
            if (roll2 == 6)
            {
                roll2 = 5;
            }
            rolls.Add(roll2);

            if (type == "Highest")
            {
                return rolls.Max();
            }

            if (type == "Lowest")
            {
                return rolls.Min();
            }

            return roll1;
        }

        protected void SetGestalt()
        {
            if (ThisCharacter.Age < 36)
            {
                ThisCharacter.GestaltLevel = ThisCharacter.Age;
            }
            else
            {
                ThisCharacter.GestaltLevel = (ThisCharacter.Age - 35) / 5 + 35;
            }
        }

        protected void UpdateGestalt()
        {
            var positiveCounter = 0;
            var negativeCounter = 0;

            while (Delta > 0)
            {
                InitialValue++;
                positiveCounter += InitialValue;
                Delta--;
            }

            while (Delta < 0)
            {
                negativeCounter -= InitialValue;
                InitialValue--;
                Delta++;
            }

            if (positiveCounter > 0)
            {
                ThisCharacter.GestaltLevel -= positiveCounter;
            }
            else
            {
                ThisCharacter.GestaltLevel -= negativeCounter;
            }

        }

        protected void HandleToggleSkills()
        {
            _addSkills = !_addSkills;
        }

        protected BSModal GestaltDescription { get; set; }
        protected void ToggleGestaltDescription(MouseEventArgs e)
        {
            GestaltDescription.Toggle();
        }

        protected BSModal SurvivalPointsDescription { get; set; }
        protected void ToggleSurvivalPointsDescription(MouseEventArgs e)
        {
            SurvivalPointsDescription.Toggle();
        }

        protected BSModal CompetencePointsDescription { get; set; }
        protected void ToggleCompetencePointsDescription(MouseEventArgs e)
        {
            CompetencePointsDescription.Toggle();
        }

        protected BSModal StrengthDescription { get; set; }
        protected void onStrengthToggle(MouseEventArgs e)
        {
            StrengthDescription.Toggle();
        }

        protected BSModal PerceptionDescription { get; set; }
        protected void onPerceptionToggle(MouseEventArgs e)
        {
            PerceptionDescription.Toggle();
        }

        protected BSModal EmpathyDescription { get; set; }
        protected void onEmpathyToggle(MouseEventArgs e)
        {
            EmpathyDescription.Toggle();
        }

        protected BSModal WillpowerDescription { get; set; }
        protected void onWillpowerToggle(MouseEventArgs e)
        {
            WillpowerDescription.Toggle();
        }

        protected BSModal WelcomeModal { get; set; }
        protected void ToggleWelcomeModal(MouseEventArgs e)
        {
            WelcomeModal.Toggle();
        }

        protected BSModal BaseAbilityDescription { get; set; }
        protected void onBaseAbilityToggleOn(BaseAbility ability)
        {
            ThisBaseAbility = ability;
            BaseAbilityDescription.Toggle();
        }
        protected void onBaseAbilityToggleOff()
        {
            BaseAbilityDescription.Toggle();
        }

        protected BSModal PlayerAbilityAttributeSelection { get; set; }
        protected void onPlayerAbilityToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilityAttributeSelection.Toggle();
        }
        protected void onPlayerAbilityToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add(Helper.FormString2);
            HasInstruction = false;

            PlayerAbilityAttributeSelection.Toggle();
        }
        protected void onPlayerAbilityToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
            ability.AdvancedUsing.Add(Helper.FormString2);

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
                ability.AdvancedUsing[^1] += "Double";
            }

            HasInstruction = false;

            PlayerAbilityAttributeSelection.Toggle();
        }


        protected BSModal PlayerAbilitySpendSelection { get; set; }
        protected void onPlayerAbilitySpendSelectionToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilitySpendSelection.Toggle();
        }
        protected void onPlayerAbilitySpendSelectionToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add(Helper.FormString2);
            HasInstruction = false;

            PlayerAbilitySpendSelection.Toggle();
        }
        protected void onPlayerAbilitySpendSelectionToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
            ability.AdvancedUsing.Add(Helper.FormString2);

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
                ability.AdvancedUsing[^1] += "Double";
            }

            HasInstruction = false;

            PlayerAbilitySpendSelection.Toggle();
        }


        protected BSModal PlayerAbilitySingleAttributeAddSpendSelection { get; set; }
        protected void onPlayerAbilitySingleAttributeAddSpendSelectionToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }
        protected void onPlayerAbilitySingleAttributeAddSpendSelectionToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add(Helper.FormString2);
            HasInstruction = false;

            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }
        protected void onPlayerAbilitySingleAttributeAddSpendSelectionToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
            ability.AdvancedUsing.Add(Helper.FormString2);

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
                ability.AdvancedUsing[^1] += "Double";
            }

            HasInstruction = false;

            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }


        protected async Task<PlayerAbility> UpdateThisPlayerAttribute()
        {
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
        }

        protected BSModal BaseSkillDescription { get; set; }
        protected void onBaseSkillToggleOn(BaseSkill skill)
        {
            ThisBaseSkill = skill;
            BaseSkillDescription.Toggle();
        }
        protected void onBaseSkillToggleOff()
        {
            BaseSkillDescription.Toggle();
        }

        protected BSModal SpecializePlayerSkill { get; set; }
        protected void onSpecializePlayerSkillToggleOn(PlayerSkill skill)
        {
            ThisPlayerSkill = skill;
            SpecializePlayerSkill.Toggle();
        }
        protected async Task onSpecializePlayerSkillToggleOff()
        {
            if (!string.IsNullOrWhiteSpace(ThisPlayerSkill.Specialty))
            {
                var newPlayerSkill = ThisPlayerSkill;
                var skillName = newPlayerSkill.BaseSkill.Name;
                var skillSpeciality = newPlayerSkill.Specialty.ToLower();

                if (skillName == "Pilot")
                {
                    if (skillSpeciality == "motorcycle" || skillSpeciality == "motorcycles" || skillSpeciality == "dirt bike" || skillSpeciality == "dirt bikes")
                    {
                        if (ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility.Name == "Biker") != null)
                        {
                            newPlayerSkill.IsSupported = true;
                        }
                    }
                    if (skillSpeciality == "bicycle" || skillSpeciality == "bicycles" || skillSpeciality == "bike" || skillSpeciality == "bikes")
                    {
                        if (ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility.Name == "BMX") != null)
                        {
                            newPlayerSkill.IsSupported = true;
                        }
                    }
                    if (ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility.Name == "Training, Vehicle/Vessel") != null)
                    {
                        newPlayerSkill.IsSupported = true;
                    }
                }

                newPlayerSkill = await PlayerSkillService.CreatePlayerSkill(newPlayerSkill);

                ThisCharacter.SpecializedPlayerSkills.Add(newPlayerSkill);

                ThisPlayerSkill = new PlayerSkill(){BaseSkill = new BaseSkill()};
            }
            else
            {
                ThisCharacter.GestaltLevel += ThisPlayerSkill.AdvancementsList.Count;
            }

            SpecializePlayerSkill.Toggle();
        }

        protected Array CsvStringToArray(string values)
        {
            return values.Split(',');
        }

        protected string getAttributeValueByBaseAttributeName(string baseAttributeName)
        {
            var playerAttribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == baseAttributeName);
            if (playerAttribute == null)
            {
                return 0.ToString();
            }

            return playerAttribute.Value.ToString();
        }

        protected string getPlayerSkillAdvancementsByBaseSkillName(string baseSkillName, bool specialized = false)
        {
            PlayerSkill skill;

            if (specialized)
            {
                skill = ThisCharacter.SpecializedPlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }
            else
            {
                skill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }

            if (skill == null || skill.Advancements == 0)
            {
                return "";
            }

            return skill.Advancements.ToString();
        }

        protected string getPlayerSkillValueByBaseSkillName(string baseSkillName, bool specialized = false)
        {
            PlayerSkill skill;

            if (specialized)
            {
                skill = ThisCharacter.SpecializedPlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }
            else
            {
                skill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }

            if (skill == null || skill.Value == 0)
            {
                return "";
            }

            return skill.Value.ToString();
        }

        protected string getPlayerSkillSpecialtyByBaseSkillName(string baseSkillName, bool specialized = false)
        {
            PlayerSkill skill;

            if (specialized)
            {
                skill = ThisCharacter.SpecializedPlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }
            else
            {
                skill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == baseSkillName);
            }

            if (skill == null || skill.Value == 0)
            {
                return "";
            }

            return skill.Notes.ToString();
        }

        protected string getPlayerTrainingValueValueByBaseTrainingValueName(string baseTrainingValueName)
        {
            var playerTrainingValue = ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == baseTrainingValueName);

            if (playerTrainingValue == null || playerTrainingValue.Value == 0)
            {
                return "";
            }

            return playerTrainingValue.Value.ToString();
        }

        public void GeneratePdf()
        {
            var TempSpecializedSkills = ThisCharacter.SpecializedPlayerSkills.ToList();

            var spewFontSize = 20;
            var headerFontSize = 13;
            var resourcesFontSize = 12;
            var skillsFontSize = 10;
            var skillsAdvancementsSize = 5;
            var trainingValueFontSize = 12;
            var abilitiesFontSize = 9;

            var skillIndentLeft = 151;
            var skillIndentRight = 302;
            var skillAdvancementsIndentLeft = 47;
            var skillAdvancementsIndentRight = 198;
            var trainingValueIndentTopLeft = 438;
            var trainingValueIndentTopRight = 519;
            var trainingValueIndentBottomLeft = 399;
            var trainingValueIndentBottomMiddle = 479;
            var trainingValueIndentBottomRight = 559;
            var abilitiesNameIndent = -90;
            var abilitiesTierIndent = 430;
            var abilitiesNotesIndent = 450;

            List<Label> page0Labels = new List<Label>();
            List<Label> page1Labels = new List<Label>();
            TransparencyGroup page0TransparencyGroup = new TransparencyGroup(0.25f);
            TransparencyGroup page1TransparencyGroup = new TransparencyGroup(0.35f);

            page0Labels.Add(new Label(ThisCharacter.FullName, 100, 30, 504, 100, Font.Helvetica, headerFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Strength").Substring(0, 1), 70, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Strength").Substring(1, 1), 94, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Perception").Substring(0, 1), 128, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Perception").Substring(1, 1), 152, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Empathy").Substring(0, 1), 187, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Empathy").Substring(1, 1), 211, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Willpower").Substring(0, 1), 245, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getAttributeValueByBaseAttributeName("Willpower").Substring(1, 1), 269, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(ThisCharacter.SurvivalPoints.ToString(), 104, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.GestaltLevel.ToString(), 193, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.CompetencePoints.ToString(), 277, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));

            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Balance"), skillAdvancementsIndentLeft, 293, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Brawl"), skillAdvancementsIndentLeft, 309, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Climb"), skillAdvancementsIndentLeft, 325, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Composure"), skillAdvancementsIndentLeft, 341, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Dodge"), skillAdvancementsIndentLeft, 357, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Endurance"), skillAdvancementsIndentLeft, 373, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Expression"), skillAdvancementsIndentLeft, 389, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Grapple"), skillAdvancementsIndentLeft, 405, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Bow/Crossbow"), skillAdvancementsIndentLeft, 441, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Calm Other"), skillAdvancementsIndentLeft, 457, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy"), skillAdvancementsIndentLeft, 473, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Barter/Bribe>"), skillAdvancementsIndentLeft, 489, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Command>"), skillAdvancementsIndentLeft, 505, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Determine Motives>"), skillAdvancementsIndentLeft, 521, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Intimidate>"), skillAdvancementsIndentLeft, 537, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Persuade>"), skillAdvancementsIndentLeft, 553, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Digital Systems"), skillAdvancementsIndentLeft, 569, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Advanced Medicine"), skillAdvancementsIndentLeft, 603, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Craft/Construct/Engineer"), skillAdvancementsIndentLeft, 619, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            if (TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Craft/Construct/Engineer") != null)
            {
                for (int i = 635; i < 684; i += 16)
                {
                    var skillToRemove = TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Craft/Construct/Engineer");
                    if (skillToRemove != null)
                    {
                        page0Labels.Add(new Label(skillToRemove.Advancements.ToString(), skillAdvancementsIndentLeft, i, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
                        page0Labels.Add(new Label(skillToRemove.Specialty, skillAdvancementsIndentLeft + 22, i - 3, 504, 100, Font.Helvetica, skillsFontSize - 1, TextAlign.Left));
                        page0TransparencyGroup.Add(new Label(skillToRemove.Value.ToString(), skillIndentLeft, i - 2, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
                        TempSpecializedSkills.Remove(skillToRemove);
                    }
                }
            }
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Martial Arts"), skillAdvancementsIndentLeft, 699, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Pilot"), skillAdvancementsIndentLeft, 715, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            if (TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Pilot") != null)
            {
                for (int i = 731; i < 748; i += 16)
                {
                    var skillToRemove = TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Pilot");
                    if (skillToRemove != null)
                    {
                        page0Labels.Add(new Label(skillToRemove.Advancements.ToString(), skillAdvancementsIndentLeft, i, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
                        page0Labels.Add(new Label(skillToRemove.Specialty, skillAdvancementsIndentLeft + 22, i - 3, 504, 100, Font.Helvetica, skillsFontSize - 1, TextAlign.Left));
                        page0TransparencyGroup.Add(new Label(skillToRemove.Value.ToString(), skillIndentLeft, i - 2, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
                        TempSpecializedSkills.Remove(skillToRemove);
                    }
                }
            }
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Balance"), skillIndentLeft, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Brawl"), skillIndentLeft, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Climb"), skillIndentLeft, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Composure"), skillIndentLeft, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Dodge"), skillIndentLeft, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Endurance"), skillIndentLeft, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Expression"), skillIndentLeft, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Grapple"), skillIndentLeft, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Bow/Crossbow"), skillIndentLeft, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Calm Other"), skillIndentLeft, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentLeft, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Diplomacy <Barter/Bribe>"), skillIndentLeft, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Diplomacy <Command>"), skillIndentLeft, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Diplomacy <Determine Motives>"), skillIndentLeft, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Diplomacy <Intimidate>"), skillIndentLeft, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Diplomacy <Persuade>"), skillIndentLeft, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Digital Systems"), skillIndentLeft, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Advanced Medicine"), skillIndentLeft, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Craft/Construct/Engineer"), skillIndentLeft, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Martial Arts"), skillIndentLeft, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Pilot"), skillIndentLeft, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Hold"), skillAdvancementsIndentRight, 293, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Jump/Leap"), skillAdvancementsIndentRight, 309, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Lift/Pull"), skillAdvancementsIndentRight, 325, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Resist Pain"), skillAdvancementsIndentRight, 341, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Search"), skillAdvancementsIndentRight, 357, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Spot/Listen"), skillAdvancementsIndentRight, 373, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Stealth"), skillAdvancementsIndentRight, 389, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label("", skillAdvancementsIndentRight, 405, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Firearms <Long Gun>"), skillAdvancementsIndentRight, 441, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Firearms <Pistol>"), skillAdvancementsIndentRight, 457, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("First Aid"), skillAdvancementsIndentRight, 473, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Bludgeoning>"), skillAdvancementsIndentRight, 489, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Piercing>"), skillAdvancementsIndentRight, 505, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Slashing>"), skillAdvancementsIndentRight, 521, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Navigation"), skillAdvancementsIndentRight, 537, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Swim"), skillAdvancementsIndentRight, 553, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Throw"), skillAdvancementsIndentRight, 569, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Ride"), skillAdvancementsIndentRight, 603, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            if (TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Ride") != null)
            {
                var skillToRemove = TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Ride");
                if (skillToRemove != null)
                {
                    page0Labels.Add(new Label(skillToRemove.Advancements.ToString(), skillAdvancementsIndentRight, 619, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
                    page0Labels.Add(new Label(skillToRemove.Specialty, skillAdvancementsIndentRight + 22, 616, 504, 100, Font.Helvetica, skillsFontSize - 1, TextAlign.Left));
                    page0TransparencyGroup.Add(new Label(skillToRemove.Value.ToString(), skillIndentRight, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
                    TempSpecializedSkills.Remove(skillToRemove);
                }
            }
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Science"), skillAdvancementsIndentRight, 635, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            if (TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Science") != null)
            {
                for (int i = 651; i < 668; i += 16)
                {
                    var skillToRemove = TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Science");
                    if (skillToRemove != null)
                    {
                        page0Labels.Add(new Label(skillToRemove.Advancements.ToString(), skillAdvancementsIndentRight, i, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
                        page0Labels.Add(new Label(skillToRemove.Specialty, skillAdvancementsIndentRight + 22, i - 3, 504, 100, Font.Helvetica, skillsFontSize - 1, TextAlign.Left));
                        page0TransparencyGroup.Add(new Label(skillToRemove.Value.ToString(), skillIndentRight, i - 2, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
                        TempSpecializedSkills.Remove(skillToRemove);
                    }
                }
            }
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Survival"), skillAdvancementsIndentRight, 683, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            if (TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Survival") != null)
            {
                for (int i = 699; i < 732; i += 16)
                {
                    var skillToRemove = TempSpecializedSkills.FirstOrDefault(s => s.BaseSkill.Name == "Survival");
                    if (skillToRemove != null)
                    {
                        page0Labels.Add(new Label(skillToRemove.Advancements.ToString(), skillAdvancementsIndentRight, i, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
                        page0Labels.Add(new Label(skillToRemove.Specialty, skillAdvancementsIndentRight + 22, i - 3, 504, 100, Font.Helvetica, skillsFontSize - 1, TextAlign.Left));
                        page0TransparencyGroup.Add(new Label(skillToRemove.Value.ToString(), skillIndentRight, i - 2, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
                        TempSpecializedSkills.Remove(skillToRemove);
                    }
                }
            }
            page0Labels.Add(new Label(getPlayerSkillAdvancementsByBaseSkillName("Toughness"), skillAdvancementsIndentRight, 747, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Hold"), skillIndentRight, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Jump/Leap"), skillIndentRight, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Lift/Pull"), skillIndentRight, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Resist Pain"), skillIndentRight, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Search"), skillIndentRight, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Spot/Listen"), skillIndentRight, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Stealth"), skillIndentRight, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Firearms <Long Gun>"), skillIndentRight, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Firearms <Pistol>"), skillIndentRight, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("First Aid"), skillIndentRight, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Melee Attack <Bludgeoning>"), skillIndentRight, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Melee Attack <Piercing>"), skillIndentRight, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Melee Attack <Slashing>"), skillIndentRight, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Navigation"), skillIndentRight, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Swim"), skillIndentRight, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Throw"), skillIndentRight, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Ride"), skillIndentRight, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Science"), skillIndentRight, 633, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 649, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 665, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Survival"), skillIndentRight, 681, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 729, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerSkillValueByBaseSkillName("Toughness"), skillIndentRight, 745, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(ThisCharacter.DamageThreshold.ToString(), 520, 132, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.Morale.ToString(), 445, 313, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Archery Gear"), trainingValueIndentTopLeft, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Bludgeon"), trainingValueIndentTopLeft, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Piercing"), trainingValueIndentTopLeft, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Slashing"), trainingValueIndentTopLeft, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Long Gun"), trainingValueIndentTopRight, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Pistol"), trainingValueIndentTopRight, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Throwing"), trainingValueIndentTopRight, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Martial Arts"), trainingValueIndentTopRight, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Athletic Gear"), trainingValueIndentBottomLeft, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Climbing Gear"), trainingValueIndentBottomLeft, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Command Apparatus"), trainingValueIndentBottomLeft, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Firefighting"), trainingValueIndentBottomLeft, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("First Aid Kit"), trainingValueIndentBottomMiddle, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Medical Gear"), trainingValueIndentBottomMiddle, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Reconnaissance Gear"), trainingValueIndentBottomMiddle, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Survival Kit"), trainingValueIndentBottomMiddle, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Swimming/Diving"), trainingValueIndentBottomRight, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Tools"), trainingValueIndentBottomRight, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Value"), trainingValueIndentBottomRight, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(getPlayerTrainingValueValueByBaseTrainingValueName("Vehicles"), trainingValueIndentBottomRight, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            if (ThisCharacter.PlayerAbilities != null)
            {
                var Y = 102;

                foreach (var ability in ThisCharacter.PlayerAbilities)
                {
                    var abilityNotesMaxLength = 35;

                    if (ability.Notes != null && ability.Notes.Length < abilityNotesMaxLength)
                    {
                        abilityNotesMaxLength = ability.Notes.Length;
                        page1TransparencyGroup.Add(new Label(ability.Notes.Substring(0, abilityNotesMaxLength), abilitiesNotesIndent, Y, 504, 100, Font.Helvetica, abilitiesFontSize, TextAlign.Left));

                    }

                    page1Labels.Add(new Label(ability.BaseAbility.ShortName, abilitiesNameIndent, Y, 504, 100, Font.Helvetica, abilitiesFontSize, TextAlign.Right));
                    page1TransparencyGroup.Add(new Label(ability.Tier.ToString(), abilitiesTierIndent, Y, 504, 100, Font.Helvetica, abilitiesFontSize, TextAlign.Left));

                    Y += 16;
                }
            }
            MergeDocument mergeDoc = new MergeDocument(GetPath(@".\wwwroot\CharacterSheets\OutbreakTemplate.pdf"));

            Page page0 = mergeDoc.Pages[0];
            Page page1 = mergeDoc.Pages[1];

            foreach (var label in page0Labels)
            {
                page0.Elements.Add(label);
            }

            foreach (var label in page1Labels)
            {
                page1.Elements.Add(label);
            }

            page0.Elements.Add(page0TransparencyGroup);
            page1.Elements.Add(page1TransparencyGroup);

            var rand = new Random().Next(1, 1000000000);
            var path = $@".\wwwroot\CharacterSheets\Outbreak_Undead_Character_{rand}.pdf";
            
            mergeDoc.Draw(path);

            CharacterSheetLocation = $"/CharacterSheets/Outbreak_Undead_Character_{rand}.pdf";
        }

        internal static string GetPath(string filePath)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, filePath);
        }
    }
}
