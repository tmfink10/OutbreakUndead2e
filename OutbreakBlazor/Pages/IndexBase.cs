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
using Microsoft.AspNetCore.Cors;
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

        protected string CollapseRightSidebar = "block";
        protected string HighlightStrength = "";
        protected string HighlightPerception = "";
        protected string HighlightEmpathy = "";
        protected string HighlightWillpower = "";
        protected string HighlightGestalt = "";
        protected bool Disable;
        protected bool CreateNew;
        protected bool AddAbilities;
        protected bool AddSkills;
        protected bool HasInstruction;
        protected int InitialValue;
        protected int StartingAttributePoints = 0;
        protected List<string> ActionsLog = new List<string>();
        protected List<PlayerSkill> BasicSkills = new List<PlayerSkill>();
        protected List<PlayerSkill> BasicSkillsLeftTable = new List<PlayerSkill>();
        protected List<PlayerSkill> BasicSkillsRightTable = new List<PlayerSkill>();
        protected List<PlayerSkill> TrainedSkills = new List<PlayerSkill>();
        protected List<PlayerSkill> TrainedSkillsLeftTable = new List<PlayerSkill>();
        protected List<PlayerSkill> TrainedSkillsRightTable = new List<PlayerSkill>();
        protected List<PlayerSkill> ExpertSkills = new List<PlayerSkill>();
        protected List<PlayerSkill> ExpertSkillsLeftTable = new List<PlayerSkill>();
        protected List<PlayerSkill> ExpertSkillsRightTable = new List<PlayerSkill>();
        protected List<PlayerSkill> SpecializedSkillsLeftTable = new List<PlayerSkill>();
        protected List<PlayerSkill> SpecializedSkillsRightTable = new List<PlayerSkill>();
        protected List<HelperClass> Helpers = new List<HelperClass>();

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
                }
            }

        }

        public class HelperClass
        {
            public string name;
            public string style;
            public string FormString;
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
            AddToActionsLog($"<div align=\"center\"><b>***** New Character Created *****</b></div>");
            CreateNew = !CreateNew;

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

        protected void DoNothing()
        {
            return;
        }
        protected async Task<PlayerCharacter> HandleOnValidPlayerCharacterSubmit()
        {
            SortSkills();
            
            AddAbilities = true;

            return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
        }

        protected async Task<PlayerAttribute> HandleIncreasePlayerAttribute(PlayerAttribute attribute)
        {
            var initialValue = attribute.Value;
            var rand = new Random();
            var advanceValue = rand.Next(1, 4);

            if (initialValue == 45)
            {
                AddToActionsLog($"{attribute.BaseAttribute.Name} already advanced to maximum value of 45");
                return await PlayerAttributeService.UpdatePlayerAttribute(attribute.Id, attribute);
            }

            if (StartingAttributePoints > 0)
            {
                attribute.Value++;
                StartingAttributePoints--;

                AddToActionsLog($"{attribute.BaseAttribute.Name} value increased by 1 to {attribute.Value} -1 free point");

                return await PlayerAttributeService.UpdatePlayerAttribute(attribute.Id, attribute);
            }

            ThisCharacter.GestaltLevel -= initialValue / 10;
            attribute.Value += advanceValue;
            if (attribute.Value > 45)
            {
                advanceValue = attribute.Value - 45;
                attribute.Value = 45;
                AddToActionsLog($"{attribute.BaseAttribute.Name} value adjusted down to maximum value of 45");
            }

            attribute.AdvancementValues.Add(advanceValue);

            if (initialValue / 10 < attribute.Value / 10)
            {
                attribute.Points += 1;
            }

            foreach (var skill in ThisCharacter.PlayerSkills)
            {
                var primaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.PrimaryAttributeBaseAttributeId);
                var secondaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.SecondaryAttributeBaseAttributeId);

                if (primaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (skill.BaseSkill.Type == "Basic" || skill.BaseSkill.Type == "Trained")
                    {
                        skill.Value += attribute.AdvancementValues[^1];
                    }

                    else if (skill.BaseSkill.Type == "Expert")
                    {
                        if (initialValue / 10 < attribute.Value / 10)
                        {
                            skill.Value += 1;
                        }
                    }
                }
                else if (secondaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (initialValue / 10 < attribute.Value / 10)
                    {
                        skill.Value += 1;
                    }
                }
            }

            if (attribute.Points < 0)
            {
                HighlightAttribute(attribute);
            }
            else if (attribute.Points >= 0)
            {
                ClearHighlightAttribute(attribute);
            }

            AddToActionsLog($"Result(D3): +{advanceValue} to {attribute.BaseAttribute.Name}");
            AddToActionsLog($"Spent {initialValue/10} Gestalt to advance {attribute.BaseAttribute.Name}");

            return await PlayerAttributeService.UpdatePlayerAttribute(attribute.Id, attribute);
        }
        protected async Task<PlayerAttribute> HandleDecreasePlayerAttribute(PlayerAttribute attribute)
        {
            var initialValue = attribute.Value;
            var valueToRemove = 0;

            if (attribute.AdvancementValues.Count > 0)
            {
                valueToRemove = attribute.AdvancementValues[^1];
                attribute.Value -= valueToRemove;
                attribute.AdvancementValues.RemoveAt(attribute.AdvancementValues.Count-1);
                ThisCharacter.GestaltLevel += attribute.Value/10;

                AddToActionsLog($"{attribute.BaseAttribute.Name} value reduced by {valueToRemove} to {attribute.Value}");
                AddToActionsLog($"Refunded {attribute.Value / 10} Gestalt");

            }
            else
            {
                StartingAttributePoints++;
                valueToRemove = 1;
                attribute.Value -= valueToRemove;
                AddToActionsLog($"{attribute.BaseAttribute.Name} value reduced by 1 to {attribute.Value} +1 free point");
            }

            if (attribute.Value/10 < initialValue / 10)
            {
                attribute.Points -= 1;
            }

            foreach (var skill in ThisCharacter.PlayerSkills)
            {
                var primaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.PrimaryAttributeBaseAttributeId);
                var secondaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.SecondaryAttributeBaseAttributeId);

                if (primaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (skill.BaseSkill.Type == "Basic" || skill.BaseSkill.Type == "Trained")
                    {
                        skill.Value -= valueToRemove;
                    }

                    else if (skill.BaseSkill.Type == "Expert")
                    {
                        if (attribute.Value / 10 < initialValue / 10)
                        {
                            skill.Value -= 1;
                        }
                    }
                }
                else if (secondaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (attribute.Value / 10 < initialValue / 10)
                    {
                        skill.Value -= 1;
                    }
                }
            }

            return await PlayerAttributeService.UpdatePlayerAttribute(attribute.Id, attribute);
        }

        protected void HandleIncreaseAge()
        {
            ThisCharacter.Age++;

            if (ThisCharacter.Age < 36)
            {
                ThisCharacter.GestaltLevel++;
            }
            else if (ThisCharacter.Age > 35 && ThisCharacter.Age%5 == 0)
            {
                ThisCharacter.GestaltLevel++;
            }

        }
        protected void HandleDecreaseAge()
        {
            
            if (ThisCharacter.Age < 36)
            {
                ThisCharacter.GestaltLevel--;
            }
            else if (ThisCharacter.Age > 35 && ThisCharacter.Age % 5 == 0)
            {
                ThisCharacter.GestaltLevel--;
            }

            ThisCharacter.Age--;
        }
        protected void SetAge()
        {
            InitialValue = ThisCharacter.Age;
        }
        protected void UpdateAge()
        {
            var delta = ThisCharacter.Age - InitialValue;

            if (delta > 0)
            {
                for (int i = InitialValue; i < Math.Min(ThisCharacter.Age,35); i++)
                {
                    ThisCharacter.GestaltLevel++;
                }

                for (int i = Math.Max(InitialValue,35); i < ThisCharacter.Age; i++)
                {
                    if ((i+1) % 5 == 0) 
                    {
                        ThisCharacter.GestaltLevel++;
                    }
                }
            }

            if (delta < 0)
            {
                for (int i = ThisCharacter.Age; i < Math.Min(InitialValue, 35); i++)
                {
                    ThisCharacter.GestaltLevel--;
                }

                for (int i = Math.Max(ThisCharacter.Age, 35); i < InitialValue; i++)
                {
                    if ((i + 1) % 5 == 0)
                    {
                        ThisCharacter.GestaltLevel--;
                    }
                }
            }
            
        }

        protected async Task HandleOnValidBaseAbilitySubmit()
        {
            if (!string.IsNullOrEmpty(Helper.FormString))
            {
                var tempAbility = new PlayerAbility
                {
                    BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString)), Tier = 1
                };

                AddToActionsLog($"<div align=\"center\"><b>^---- Added {tempAbility.BaseAbility.ShortName} ----^</b></div>");

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
                        OnPlayerAbilitySingleAttributeAddSpendSelectionToggleOn(tempAbility);
                    }
                    else
                    {
                        OnPlayerAbilityToggleOn(tempAbility);
                    }
                }

                foreach (var trainingValue in ThisCharacter.TrainingValues)
                {
                    foreach (var baseTrainingValue in tempAbility.BaseAbility.ModifiesBaseTrainingValues)
                    {
                        if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                        {
                            trainingValue.Value += 1;
                            AddToActionsLog($"{tempAbility.BaseAbility.ShortName} increased Training Value for {trainingValue.BaseTrainingValue.Name} by 1");
                        }
                    }
                }

                ThisCharacter.PlayerAbilities.Add(tempAbility);
            }
        }
        protected async Task<PlayerAbility> HandleIncreasePlayerAbility(PlayerAbility ability)
        {
            AddToActionsLog($"<div align=\"center\"><b>^---- Increase {ability.BaseAbility.ShortName} Value ----^</b></div>");

            if (ability.Tier == 5)
            {
                AddToActionsLog($"{ability.BaseAbility.ShortName} already advanced 5 times. Advancement prohibited.");
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
                        AddToActionsLog($"{ability.BaseAbility.ShortName} increased Training Value for {trainingValue.BaseTrainingValue.Name} by 1");
                    }
                }
            }

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }
        protected async Task<PlayerAbility> HandleDecreasePlayerAbility(PlayerAbility ability)
        {
            AddToActionsLog($"<div align=\"center\"><b>^---- Decrease {ability.BaseAbility.ShortName} Value ----^</b></div>");
            var attribute = ThisCharacter.PlayerAttributes
                .FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            if (ability.Tier == 1)
            {
                AddToActionsLog($"{ability.BaseAbility.ShortName} cannot be reduced below 1. Remove ability to reduce further.");
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            if (ability.AdvancedUsing.Count > 0)
            {
                if (ability.AdvancedUsing[^1] == "gestalt")
                {
                    ThisCharacter.GestaltLevel += (ability.Tier);
                    AddToActionsLog($"+{ability.Tier} Gestalt added for reducing {ability.BaseAbility.ShortName}");
                    ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                }

                else if (ability.AdvancedUsing[^1] == "gestaltDouble")
                {
                    ThisCharacter.GestaltLevel += (ability.Tier) * 2;
                    AddToActionsLog($"+{ability.Tier*2} Gestalt added for reducing {ability.BaseAbility.ShortName}");
                    ability.AdvancedUsing.Remove(ability.AdvancedUsing[^1]);
                }

                else if (ability.AdvancedUsing[^1] == "points" || ability.AdvancedUsing[^1] == "pointsDouble")
                {
                    attribute.Points += 1;
                    AddToActionsLog($"+1 point added to {ability.BaseAbility.Name} for reducing {ability.BaseAbility.ShortName}");
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
                            AddToActionsLog($"{trainingValue.BaseTrainingValue.Name} reduced by 1");
                        }
                    }
                }
            }

            if (attribute.Points >= 0)
            {
                ClearHighlightAttribute(attribute);
            }

            if (ThisCharacter.GestaltLevel >= 0)
            {
                ClearHighlightGestaltValue();
            }

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }
        protected void DeletePlayerAbility(PlayerAbility ability)
        {
            AddToActionsLog("<div align=\"center\"><b>^---- Delete Ability ----^</b></div>");

            ThisCharacter.PlayerAbilities.Remove(ability);
            var attribute = ThisCharacter.PlayerAttributes
                .FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            var result = "";

            for (int i = ability.AdvancedUsing.Count-1; i > -1; i--)
            {
                if (ability.AdvancedUsing[i] == "gestalt")
                {
                    if (i == 0)
                    {
                        ThisCharacter.GestaltLevel += (5 - attribute.Bonus);
                        result = $"+{5 - attribute.Bonus} Gestalt";
                        AddToActionsLog(result);
                    }
                    else
                    {
                        ThisCharacter.GestaltLevel += ability.Tier;
                        result = $"+{ability.Tier} Gestalt";
                        AddToActionsLog(result);
                    }
                }
                else if (ability.AdvancedUsing[i] == "gestaltDouble")
                {
                    if (i == 0)
                    {
                        ThisCharacter.GestaltLevel += (5 - attribute.Bonus)*2;
                        result = $"+{(5 - attribute.Bonus)*2} Gestalt";
                        AddToActionsLog(result);
                    }
                    else
                    {
                        ThisCharacter.GestaltLevel += ability.Tier*2;
                        result = $"+{ability.Tier} Gestalt";
                        AddToActionsLog(result);
                    }
                }
                else if (ability.AdvancedUsing[i] == "points" || ability.AdvancedUsing[i] == "pointsDouble")
                {
                    attribute.Points += 1;
                    result = $"+1 point to {attribute.BaseAttribute.Name}";
                    AddToActionsLog(result);
                }

                ability.Tier -= 1;
            }

            result = $"Removed {ability.BaseAbility.ShortName}";
            AddToActionsLog(result);

            ability.AdvancedUsing = new List<string>();

            if (attribute.Points >= 0)
            {
                ClearHighlightAttribute(attribute);
            }

            if (ThisCharacter.GestaltLevel >= 0)
            {
                ClearHighlightGestaltValue();
            }

            foreach (var skill in ability.SupportsPlayerSkills)
            {
                ThisCharacter.PlayerSkills.FirstOrDefault(s => s.Id == skill.Id).IsSupported = false;
                foreach (var playerAbility in ThisCharacter.PlayerAbilities)
                {
                    foreach (var playerSkill in playerAbility.SupportsPlayerSkills)
                    {
                        ThisCharacter.PlayerSkills.FirstOrDefault(s => s.Id == playerSkill.Id).IsSupported = true;
                    }
                }
            }
        }

        protected void InitializePlayerSkills(BaseSkill skill)
        {
            var playerSkill = new PlayerSkill(){BaseSkill = skill};

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

            ThisCharacter.PlayerSkills.Add(playerSkill);
        }
        protected async Task<PlayerSkill> HandleIncreasePlayerSkill(PlayerSkill skill)
        {
            AddToActionsLog($"<div align=\"center\"><b>^---- Increase {skill.BaseSkill.ShortName} Value ----^</b></div>");

            InitialValue = skill.Value;
            
            var totalAdvancement = 0;

            if (skill.AdvancementsList.Count==5)
            {
                AddToActionsLog($"{skill.BaseSkill.Name} already advanced 5 times. Further advancement prohibited.");
                return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
                //PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
            }

            if (skill.BaseSkill.Type == "Basic")
            {
                if (skill.IsSupported)
                {
                    var roll = RollD5("Highest");
                    totalAdvancement += roll;
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    foreach (var ability in ThisCharacter.PlayerAbilities)
                    {
                        foreach (var playerSkill in ability.SupportsPlayerSkills)
                        {
                            if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                            {
                                if (ability.BaseAbility.AdvancesSkills)
                                {
                                    totalAdvancement += ability.Tier;
                                    AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is supported");
                }
                else
                {
                    var roll = RollD5();
                    totalAdvancement += roll;
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is not supported");
                }
            }

            else if (skill.BaseSkill.Type == "Trained")
            {
                if (skill.IsSpecialized)
                {
                    var roll = RollD5("Highest");
                    totalAdvancement += roll;
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
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
                                        AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
                                    }
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                    AddToActionsLog($"{skill.BaseSkill.Name} is a specialized {skill.BaseSkill.Type} skill");
                }
                else if (skill.IsSupported)
                {
                    var roll = RollD5();
                    totalAdvancement += roll;
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    foreach (var ability in ThisCharacter.PlayerAbilities)
                    {
                        foreach (var playerSkill in ability.SupportsPlayerSkills)
                        {
                            if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                            {
                                if (ability.BaseAbility.AdvancesSkills)
                                {
                                    totalAdvancement += ability.Tier;
                                    AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
                                }
                            }
                        }
                    }
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is supported");
                }
                else
                {
                    var roll = RollD5("Lowest");
                    totalAdvancement += roll;
                    ThisCharacter.GestaltLevel -= 1;
                    skill.Advancements += 1;
                    skill.Value = InitialValue + totalAdvancement;
                    skill.AdvancementsList.Add(totalAdvancement);
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is neither specialized nor supported");
                }
            }

            else if (skill.BaseSkill.Type == "Expert")
            {
                var roll = 0;
                if (skill.IsSpecialized && skill.IsSupported)
                {
                    roll = RollD5();
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is both specialized and supported");
                }
                else if (skill.IsSupported)
                {
                    roll = RollD5("Lowest");
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is supported");
                }
                else
                {
                    AddToActionsLog($"{skill.BaseSkill.Name} is a {skill.BaseSkill.Type} skill and is not supported. Advancement is prohibited.");
                    return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
                    //return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
                }

                totalAdvancement += roll;
                ThisCharacter.GestaltLevel -= 1;
                skill.Advancements += 1;

                foreach (var ability in ThisCharacter.PlayerAbilities)
                {
                    foreach (var playerSkill in ability.SupportsPlayerSkills)
                    {
                        if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                        {
                            if (ability.BaseAbility.AdvancesSkills)
                            {
                                totalAdvancement += ability.Tier;
                                AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
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
                                AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
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
                                AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
                            }
                        }
                        else if (ability.BaseAbility.Name == "Training, Vehicle/Vessel")
                        {
                            if (skill.BaseSkill.Name == "Pilot")
                            {
                                totalAdvancement += ability.Tier;
                                AddToActionsLog($"{ability.BaseAbility.Name} added {ability.Tier} to advancement of {skill.BaseSkill.Name}");
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
                AddToActionsLog($"{skill.BaseSkill.Name} already advanced 5 times. Further advancement prohibited.");
            }

            if (ThisCharacter.GestaltLevel < 0)
            {
                HighlightGestaltValue();
            }

            AddToActionsLog("Details:");
            AddToActionsLog($"Total advancement for {skill.BaseSkill.Name} = {totalAdvancement}");

            if (skill.Value > 99)
            {
                var difference = skill.Value - 99;
                skill.Value = 99;
                skill.AdvancementsList[^1] = skill.AdvancementsList[^1] - difference;
                AddToActionsLog($"<i>{skill.BaseSkill.Name} has reached the maximum allowed value of 99.</i>");
            }

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }
        protected async Task<PlayerSkill> HandleDecreasePlayerSkill(PlayerSkill skill)
        {

            AddToActionsLog($"<div align=\"center\"><b>^---- Decrease {skill.BaseSkill.ShortName} Value ----^</b></div>");

            InitialValue = skill.Value;

            if (skill.AdvancementsList.Count>0)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value = InitialValue - lastAdvancement;
                AddToActionsLog($"+1 Gestalt: {skill.BaseSkill.Name} value decreased by {lastAdvancement} (from {InitialValue} to {skill.Value}).");
                skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
            }

            if (ThisCharacter.GestaltLevel >= 0)
            {
                ClearHighlightGestaltValue();
            }

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }
        protected async Task HandleSpecializeSkill(PlayerSkill skill)
        {

            var newPlayerSkill = new PlayerSkill
            {
                BaseSkill = skill.BaseSkill,
                Value = skill.Value,
                IsSupported = skill.IsSupported,
                Advancements = skill.Advancements,
                AdvancementsList = skill.AdvancementsList,
                PlayerCharacter = skill.PlayerCharacter,
                PlayerCharacterId = skill.PlayerCharacterId,
                Notes = skill.Notes,
                AttributeValue = skill.AttributeValue,
                IsSpecialized = true
            };

            newPlayerSkill = await PlayerSkillService.CreatePlayerSkill(newPlayerSkill);

            ThisCharacter.GestaltLevel -= skill.AdvancementsList.Count;
            

            if (ThisCharacter.GestaltLevel < 0)
            {
                HighlightGestaltValue();
            }

            OnSpecializePlayerSkillToggleOn(newPlayerSkill);
        }
        protected void HandleRemovePlayerSkill(PlayerSkill skill)
        {
            ThisCharacter.SpecializedPlayerSkills.Remove(skill);

            if (SpecializedSkillsLeftTable.Contains(skill))
            {
                SpecializedSkillsLeftTable.Remove(skill);
            }

            if (SpecializedSkillsRightTable.Contains(skill))
            {
                SpecializedSkillsRightTable.Remove(skill);
            }

            ThisCharacter.GestaltLevel += skill.AdvancementsList.Count;
            AddToActionsLog($"<div align=\"center\"><b>^---- Remove {skill.BaseSkill.Name} ({skill.Specialty}) ----^</b></div>");
            AddToActionsLog($"+{skill.AdvancementsList.Count} Gestalt for removing {skill.BaseSkill.Name} ({skill.Specialty})");

            if (ThisCharacter.GestaltLevel >= 0)
            {
                ClearHighlightGestaltValue();
            }
        }
        protected void SortSkills()
        {
            BasicSkills = BasicSkills.OrderBy(s => s.BaseSkill.Name).ToList();
            TrainedSkills = TrainedSkills.OrderBy(s => s.BaseSkill.Name).ToList();
            ExpertSkills = ExpertSkills.OrderBy(s => s.BaseSkill.Name).ToList();

            foreach (var skill in ThisCharacter.PlayerSkills)
            {
                if (skill.BaseSkill.Type == "Expert")
                {
                    ExpertSkills.Add(skill);
                }
                if (skill.BaseSkill.Type == "Trained")
                {
                    TrainedSkills.Add(skill);
                }
                if (skill.BaseSkill.Type == "Basic")
                {
                    BasicSkills.Add(skill);
                }
            }

            var counter = 0;
            foreach (var skill in BasicSkills)
            {
                if (counter <= BasicSkills.Count / 2 && counter * 2 != BasicSkills.Count)
                {
                    BasicSkillsLeftTable.Add(skill);
                }
                else
                {
                    BasicSkillsRightTable.Add(skill);
                }

                counter++;
            }

            counter = 0;
            foreach (var skill in TrainedSkills)
            {
                if (counter <= TrainedSkills.Count / 2 && counter * 2 != TrainedSkills.Count)
                {
                    TrainedSkillsLeftTable.Add(skill);
                }
                else
                {
                    TrainedSkillsRightTable.Add(skill);
                }

                counter++;
            }

            counter = 0;
            foreach (var skill in ExpertSkills)
            {
                if (counter <= ExpertSkills.Count / 2 && counter * 2 != ExpertSkills.Count)
                {
                    ExpertSkillsLeftTable.Add(skill);
                }
                else
                {
                    ExpertSkillsRightTable.Add(skill);
                }

                counter++;
            }
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
            var roll1modified = false;
            var roll2modified = false;

            var roll1 = rand.Next(1, 7);
            if (roll1 == 6)
            {
                roll1modified = true;
                roll1 = 5;
            }
            rolls.Add(roll1);

            var roll2 = rand.Next(1, 7);
            if (roll2 == 6)
            {
                roll2modified = true;
                roll2 = 5;
            }
            rolls.Add(roll2);

            if (type == "Highest")
            {
                var result = $"Rolling 2D6... Highest of [{roll1}, {roll2}] = {rolls.Max()}";
                if (roll1modified || roll2modified)
                {
                    result += " (modified from 6)";
                }
                AddToActionsLog(result);
                return rolls.Max();
            }

            else if (type == "Lowest")
            {
                var result = $"Rolling 2D6... Lowest of [{roll1}, {roll2}] = {rolls.Min()}";
                AddToActionsLog(result);
                return rolls.Min();
            }

            else
            {
                var result = $"Rolling 1D6... Result = {roll1}";
                if (roll1modified)
                {
                    result += " (modified from 6)";
                }
                AddToActionsLog(result);

                return roll1;
            }
            
        }

        protected void AddToActionsLog(string s)
        {
            ActionsLog.Reverse();

            ActionsLog.Add(s);

            if (ActionsLog.Count > 24)
            {
                ActionsLog.Remove(ActionsLog[0]);
            }

            ActionsLog.Reverse();
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

        protected void HandleToggleSkills()
        {
            AddSkills = !AddSkills;
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
        protected void OnStrengthToggle(MouseEventArgs e)
        {
            StrengthDescription.Toggle();
        }

        protected BSModal PerceptionDescription { get; set; }
        protected void OnPerceptionToggle(MouseEventArgs e)
        {
            PerceptionDescription.Toggle();
        }

        protected BSModal EmpathyDescription { get; set; }
        protected void OnEmpathyToggle(MouseEventArgs e)
        {
            EmpathyDescription.Toggle();
        }

        protected BSModal WillpowerDescription { get; set; }
        protected void OnWillpowerToggle(MouseEventArgs e)
        {
            WillpowerDescription.Toggle();
        }

        protected BSModal WelcomeModal { get; set; }
        protected void ToggleWelcomeModal(MouseEventArgs e)
        {
            WelcomeModal.Toggle();
        }

        protected BSModal CharacterSheetModal { get; set; }
        protected void ToggleCharacterSheetModal(MouseEventArgs e)
        {
            PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);

            GeneratePdf();
            CharacterSheetModal.Toggle();
        }

        protected BSModal BaseAbilityDescription { get; set; }
        protected void OnBaseAbilityToggleOn(BaseAbility ability)
        {
            ThisBaseAbility = ability;
            BaseAbilityDescription.Toggle();
        }
        protected void OnBaseAbilityToggleOff()
        {
            BaseAbilityDescription.Toggle();
        }

        protected BSModal PlayerAbilityAttributeSelection { get; set; }
        protected void OnPlayerAbilityToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilityAttributeSelection.Toggle();
        }
        protected void OnPlayerAbilityToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add("points");
            HasInstruction = false;

            if (attribute.Points < 0)
            {
                HighlightAttribute(attribute);
            }

            var result = $"Spent 1 {attribute.BaseAttribute.Name} point to add {ability.BaseAbility.ShortName}";
            AddToActionsLog(result);

            PlayerAbilityAttributeSelection.Toggle();
        }
        protected void OnPlayerAbilityToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);
            var spend = 5 - attribute.Bonus;

            ThisCharacter.GestaltLevel -= spend;
            ability.AdvancedUsing.Add("gestalt");
            var result = $"Spent {spend} Gestalt";

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= (5 - attribute.Bonus);
                ability.AdvancedUsing[^1] += "Double";
                result += $" + {spend} Gestalt (prof. w/o instr.)";
            }

            HasInstruction = false;

            if (ThisCharacter.GestaltLevel < 0)
            {
                HighlightGestaltValue();
            }

            result += $" to add {ability.BaseAbility.ShortName} linked to {attribute.BaseAttribute.Name}";
            AddToActionsLog(result);

            PlayerAbilityAttributeSelection.Toggle();

            if (ability.BaseAbility.Name == "Support Basic Skill")
            {
                OnSupportBasicSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Trained Skill")
            {
                OnSupportTrainedSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Expert Skill")
            {
                OnSupportExpertSkillToggleOn(ability.BaseAbility);
            }
        }

        protected BSModal PlayerAbilitySingleAttributeAddSpendSelection { get; set; }
        protected void OnPlayerAbilitySingleAttributeAddSpendSelectionToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }
        protected void OnPlayerAbilitySingleAttributeAddSpendSelectionToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add("points");
            HasInstruction = false;

            if (attribute.Points < 0)
            {
                HighlightAttribute(attribute);
            }

            var result = $"Spent 1 {attribute.BaseAttribute.Name} point to add {ability.BaseAbility.ShortName}";
            AddToActionsLog(result);

            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }
        protected void OnPlayerAbilitySingleAttributeAddSpendSelectionToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);
            var spend = 5 - attribute.Bonus;

            ThisCharacter.GestaltLevel -= spend;
            ability.AdvancedUsing.Add("gestalt");
            var result = $"Spent {spend} Gestalt";

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= spend;
                ability.AdvancedUsing[^1] += "Double";
                result += $" + {spend} Gestalt (prof. w/o instr.)";
            }

            HasInstruction = false;

            if (ThisCharacter.GestaltLevel < 0)
            {
                HighlightGestaltValue();
            }

            result += $" to add {ability.BaseAbility.ShortName} linked to {attribute.BaseAttribute.Name}";
            AddToActionsLog(result);

            PlayerAbilitySingleAttributeAddSpendSelection.Toggle();
        }

        protected BSModal PlayerAbilitySpendSelection { get; set; }
        protected void onPlayerAbilitySpendSelectionToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            ThisBaseAbility = ThisPlayerAbility.BaseAbility;
            PlayerAbilitySpendSelection.Toggle();
        }
        protected void OnPlayerAbilitySpendSelectionToggleOffUsingPoints(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);

            attribute.Points -= 1;
            ability.AdvancedUsing.Add("points");
            HasInstruction = false;

            if (attribute.Points < 0)
            {
                HighlightAttribute(attribute);
            }

            var result = $"Spent 1 {attribute.BaseAttribute.Name} point to advance {ability.BaseAbility.ShortName} to tier {ability.Tier}";
            AddToActionsLog(result);

            PlayerAbilitySpendSelection.Toggle();

            if (ability.BaseAbility.Name == "Support Basic Skill")
            {
                OnSupportBasicSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Trained Skill")
            {
                OnSupportTrainedSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Expert Skill")
            {
                OnSupportExpertSkillToggleOn(ability.BaseAbility);
            }
        }
        protected void OnPlayerAbilitySpendSelectionToggleOffUsingGestalt(PlayerAbility ability)
        {
            var attribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode);
            var result = $"Spent {ability.Tier} Gestalt";

            ThisCharacter.GestaltLevel -= ability.Tier;
            ability.AdvancedUsing.Add("gestalt");

            if (ability.BaseAbility.IsProfessional && HasInstruction == false)
            {
                ThisCharacter.GestaltLevel -= ability.Tier;
                ability.AdvancedUsing[^1] += "Double";
                result += $" + {ability.Tier} Gestalt (prof. w/o instr.)";
            }

            HasInstruction = false;

            if (ThisCharacter.GestaltLevel < 0)
            {
                HighlightGestaltValue();
            }

            result += $" to advance {ability.BaseAbility.ShortName} to tier {ability.Tier}";
            AddToActionsLog(result);

            PlayerAbilitySpendSelection.Toggle();

            if (ability.BaseAbility.Name == "Support Basic Skill")
            {
                OnSupportBasicSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Trained Skill")
            {
                OnSupportTrainedSkillToggleOn(ability.BaseAbility);
            }

            if (ability.BaseAbility.Name == "Support Expert Skill")
            {
                OnSupportExpertSkillToggleOn(ability.BaseAbility);
            }
        }

        protected void HighlightAttribute(PlayerAttribute attribute)
        {
            var attributeName = attribute.BaseAttribute.Name;

            if (attributeName == "Strength")
            {
                HighlightStrength = "background-color: #FFFF00";
            }
            else if (attributeName == "Perception")
            {
                HighlightPerception = "background-color: #FFFF00";
            }
            else if (attributeName == "Empathy")
            {
                HighlightEmpathy = "background-color: #FFFF00";
            }
            else if (attributeName == "Willpower")
            {
                HighlightWillpower = "background-color: #FFFF00";
            }

            AddToActionsLog($"{attributeName} POINTS EXCEEDED");
        }
        protected void ClearHighlightAttribute(PlayerAttribute attribute)
        {
            var attributeName = attribute.BaseAttribute.Name;

            if (attributeName == "Strength")
            {
                HighlightStrength = "";
            }
            else if (attributeName == "Perception")
            {
                HighlightPerception = "";
            }
            else if (attributeName == "Empathy")
            {
                HighlightEmpathy = "";
            }
            else if (attributeName == "Willpower")
            {
                HighlightWillpower = "";
            }
        }

        protected void HighlightGestaltValue()
        {
            HighlightGestalt = "background-color: #FFFF00";
        }
        protected void ClearHighlightGestaltValue()
        {
            HighlightGestalt = "";
        }

        protected async Task<PlayerAbility> UpdateThisPlayerAttribute()
        {
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
        }

        protected BSModal BaseSkillDescription { get; set; }
        protected void OnBaseSkillToggleOn(BaseSkill skill)
        {
            ThisBaseSkill = skill;
            BaseSkillDescription.Toggle();
        }
        protected void OnBaseSkillToggleOff()
        {
            BaseSkillDescription.Toggle();
        }

        protected BSModal SpecializePlayerSkill { get; set; }
        protected void OnSpecializePlayerSkillToggleOn(PlayerSkill skill)
        {
            ThisPlayerSkill = skill;
            SpecializePlayerSkill.Toggle();
        }
        protected async Task OnSpecializePlayerSkillToggleOff()
        {
            if (!string.IsNullOrWhiteSpace(ThisPlayerSkill.Specialty))
            {
                var newPlayerSkill = ThisPlayerSkill;
                var skillName = newPlayerSkill.BaseSkill.Name;
                var skillSpecialty = newPlayerSkill.Specialty.ToLower();

                if (skillName == "Pilot")
                {
                    if (skillSpecialty == "motorcycle" || skillSpecialty == "motorcycles" || skillSpecialty == "dirt bike" || skillSpecialty == "dirt bikes")
                    {
                        if (ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility.Name == "Biker") != null)
                        {
                            newPlayerSkill.IsSupported = true;
                        }
                    }
                    if (skillSpecialty == "bicycle" || skillSpecialty == "bicycles" || skillSpecialty == "bike" || skillSpecialty == "bikes")
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

                await PlayerSkillService.UpdatePlayerSkill(newPlayerSkill.Id, newPlayerSkill);

                ThisCharacter.SpecializedPlayerSkills.Add(newPlayerSkill);

                if (SpecializedSkillsLeftTable.Count <= SpecializedSkillsRightTable.Count)
                {
                    SpecializedSkillsLeftTable.Add(newPlayerSkill);
                }
                else
                {
                    SpecializedSkillsRightTable.Add(newPlayerSkill);
                }

                AddToActionsLog($"<div align=\"center\"><b>^---- Specialize {ThisPlayerSkill.BaseSkill.Name} in {ThisPlayerSkill.Specialty} ----^</b></div>");
                AddToActionsLog($"-{newPlayerSkill.AdvancementsList.Count} Gestalt to create {newPlayerSkill.BaseSkill.Name} ({newPlayerSkill.Specialty}) ");

                ThisPlayerSkill = new PlayerSkill(){BaseSkill = new BaseSkill()};
            }
            else
            {
                ThisCharacter.GestaltLevel += ThisPlayerSkill.AdvancementsList.Count;
            }

            SpecializePlayerSkill.Toggle();

            //await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);

        }

        protected BSModal SupportBasicSkill { get; set; }
        protected void OnSupportBasicSkillToggleOn(BaseAbility ability)
        {
            foreach (var playerSkill in ThisCharacter.PlayerSkills)
            {
                var helper = new HelperClass();

                if (playerSkill.BaseSkill.Type == "Basic" && playerSkill.IsSupported == false)
                {
                    helper.name = playerSkill.BaseSkill.ShortName;
                    helper.style = "unselected";

                    Helpers.Add(helper);
                }
            }
            ThisBaseAbility = ability;
            SupportBasicSkill.Toggle();
        }
        protected void OnSupportBasicSkillToggleOff()
        {
            var playerSkill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == ThisBaseSkill.Name);
            var playerAbility = ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility == ThisBaseAbility);

            playerSkill.IsSupported = true;
            playerAbility.SupportsPlayerSkills.Add(playerSkill);

            Helpers = new List<HelperClass>();
            Disable = false;

            SupportBasicSkill.Toggle();
        }

        protected BSModal SupportTrainedSkill { get; set; }
        protected void OnSupportTrainedSkillToggleOn(BaseAbility ability)
        {
            foreach (var playerSkill in ThisCharacter.PlayerSkills)
            {
                var helper = new HelperClass();

                if (playerSkill.BaseSkill.Type == "Trained" && playerSkill.IsSupported == false)
                {
                    helper.name = playerSkill.BaseSkill.ShortName;
                    helper.style = "unselected";

                    Helpers.Add(helper);
                }
            }
            ThisBaseAbility = ability;
            SupportTrainedSkill.Toggle();
        }
        protected void OnSupportTrainedSkillToggleOff()
        {
            var playerSkill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == ThisBaseSkill.Name);
            var playerAbility = ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility == ThisBaseAbility);

            playerSkill.IsSupported = true;
            playerAbility.SupportsPlayerSkills.Add(playerSkill);

            Helpers = new List<HelperClass>();
            Disable = false;

            SupportTrainedSkill.Toggle();
        }

        protected BSModal SupportExpertSkill { get; set; }
        protected void OnSupportExpertSkillToggleOn(BaseAbility ability)
        {
            foreach (var playerSkill in ThisCharacter.PlayerSkills)
            {
                var helper = new HelperClass();

                if (playerSkill.BaseSkill.Type == "Expert" && playerSkill.IsSupported == false)
                {
                    helper.name = playerSkill.BaseSkill.ShortName;
                    helper.style = "unselected";

                    Helpers.Add(helper);
                }
            }
            ThisBaseAbility = ability;
            SupportExpertSkill.Toggle();
        }
        protected void OnSupportExpertSkillToggleOff()
        {
            var playerSkill = ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == ThisBaseSkill.Name);
            var playerAbility = ThisCharacter.PlayerAbilities.FirstOrDefault(a => a.BaseAbility == ThisBaseAbility);

            playerSkill.IsSupported = true;
            playerAbility.SupportsPlayerSkills.Add(playerSkill);

            Helpers = new List<HelperClass>();
            Disable = false;

            SupportExpertSkill.Toggle();
        }

        protected void OnSelectSkillToSupport(HelperClass skill)
        {
            if (skill.style == "unselected")
            {
                Disable = false;

                foreach (var helperSkill in Helpers)
                {
                    helperSkill.style = "unselected";
                }

                skill.style = "selected";

                ThisBaseSkill = BaseSkills.FirstOrDefault(s => s.ShortName == skill.name);
            }
            else
            {
                Disable = true;
                skill.style = "unselected";
            }
        }

        protected Array CsvStringToArray(string values)
        {
            return values.Split(',');
        }

        protected string GetAttributeValueByBaseAttributeName(string baseAttributeName)
        {
            var playerAttribute = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == baseAttributeName);
            if (playerAttribute == null)
            {
                return 0.ToString();
            }

            return playerAttribute.Value.ToString();
        }

        protected string GetPlayerSkillAdvancementsByBaseSkillName(string baseSkillName, bool specialized = false)
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

        protected string GetPlayerSkillValueByBaseSkillName(string baseSkillName, bool specialized = false)
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

        protected string GetPlayerTrainingValueValueByBaseTrainingValueName(string baseTrainingValueName)
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

            CollapseRightSidebar = "none";

            page0Labels.Add(new Label(ThisCharacter.FullName, 100, 30, 504, 100, Font.Helvetica, headerFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Strength").Substring(0, 1), 70, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Strength").Substring(1, 1), 94, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Perception").Substring(0, 1), 128, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Perception").Substring(1, 1), 152, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Empathy").Substring(0, 1), 187, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Empathy").Substring(1, 1), 211, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Willpower").Substring(0, 1), 245, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetAttributeValueByBaseAttributeName("Willpower").Substring(1, 1), 269, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(ThisCharacter.SurvivalPoints.ToString(), 104, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.GestaltLevel.ToString(), 193, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.CompetencePoints.ToString(), 277, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left));

            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Balance"), skillAdvancementsIndentLeft, 293, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Brawl"), skillAdvancementsIndentLeft, 309, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Climb"), skillAdvancementsIndentLeft, 325, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Composure"), skillAdvancementsIndentLeft, 341, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Dodge"), skillAdvancementsIndentLeft, 357, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Endurance"), skillAdvancementsIndentLeft, 373, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Expression"), skillAdvancementsIndentLeft, 389, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Grapple"), skillAdvancementsIndentLeft, 405, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Bow/Crossbow"), skillAdvancementsIndentLeft, 441, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Calm Other"), skillAdvancementsIndentLeft, 457, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy"), skillAdvancementsIndentLeft, 473, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Barter/Bribe>"), skillAdvancementsIndentLeft, 489, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Command>"), skillAdvancementsIndentLeft, 505, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Determine Motives>"), skillAdvancementsIndentLeft, 521, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Intimidate>"), skillAdvancementsIndentLeft, 537, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Diplomacy <Persuade>"), skillAdvancementsIndentLeft, 553, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Digital Systems"), skillAdvancementsIndentLeft, 569, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Advanced Medicine"), skillAdvancementsIndentLeft, 603, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Craft/Construct/Engineer"), skillAdvancementsIndentLeft, 619, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
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
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Martial Arts"), skillAdvancementsIndentLeft, 699, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Pilot"), skillAdvancementsIndentLeft, 715, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
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
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Balance"), skillIndentLeft, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Brawl"), skillIndentLeft, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Climb"), skillIndentLeft, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Composure"), skillIndentLeft, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Dodge"), skillIndentLeft, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Endurance"), skillIndentLeft, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Expression"), skillIndentLeft, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Grapple"), skillIndentLeft, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Bow/Crossbow"), skillIndentLeft, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Calm Other"), skillIndentLeft, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentLeft, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Diplomacy <Barter/Bribe>"), skillIndentLeft, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Diplomacy <Command>"), skillIndentLeft, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Diplomacy <Determine Motives>"), skillIndentLeft, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Diplomacy <Intimidate>"), skillIndentLeft, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Diplomacy <Persuade>"), skillIndentLeft, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Digital Systems"), skillIndentLeft, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Advanced Medicine"), skillIndentLeft, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Craft/Construct/Engineer"), skillIndentLeft, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Martial Arts"), skillIndentLeft, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Pilot"), skillIndentLeft, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Hold"), skillAdvancementsIndentRight, 293, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Jump/Leap"), skillAdvancementsIndentRight, 309, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Lift/Pull"), skillAdvancementsIndentRight, 325, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Resist Pain"), skillAdvancementsIndentRight, 341, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Search"), skillAdvancementsIndentRight, 357, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Spot/Listen"), skillAdvancementsIndentRight, 373, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Stealth"), skillAdvancementsIndentRight, 389, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label("", skillAdvancementsIndentRight, 405, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Firearms <Long Gun>"), skillAdvancementsIndentRight, 441, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Firearms <Pistol>"), skillAdvancementsIndentRight, 457, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("First Aid"), skillAdvancementsIndentRight, 473, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Bludgeoning>"), skillAdvancementsIndentRight, 489, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Piercing>"), skillAdvancementsIndentRight, 505, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Melee Attack <Slashing>"), skillAdvancementsIndentRight, 521, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Navigation"), skillAdvancementsIndentRight, 537, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Swim"), skillAdvancementsIndentRight, 553, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Throw"), skillAdvancementsIndentRight, 569, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Ride"), skillAdvancementsIndentRight, 603, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
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
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Science"), skillAdvancementsIndentRight, 635, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
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
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Survival"), skillAdvancementsIndentRight, 683, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));
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
            page0Labels.Add(new Label(GetPlayerSkillAdvancementsByBaseSkillName("Toughness"), skillAdvancementsIndentRight, 747, 504, 100, Font.Helvetica, skillsAdvancementsSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Hold"), skillIndentRight, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Jump/Leap"), skillIndentRight, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Lift/Pull"), skillIndentRight, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Resist Pain"), skillIndentRight, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Search"), skillIndentRight, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Spot/Listen"), skillIndentRight, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Stealth"), skillIndentRight, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Firearms <Long Gun>"), skillIndentRight, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Firearms <Pistol>"), skillIndentRight, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("First Aid"), skillIndentRight, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Melee Attack <Bludgeoning>"), skillIndentRight, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Melee Attack <Piercing>"), skillIndentRight, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Melee Attack <Slashing>"), skillIndentRight, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Navigation"), skillIndentRight, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Swim"), skillIndentRight, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Throw"), skillIndentRight, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Ride"), skillIndentRight, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Science"), skillIndentRight, 633, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 649, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 665, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Survival"), skillIndentRight, 681, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label("", skillIndentRight, 729, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerSkillValueByBaseSkillName("Toughness"), skillIndentRight, 745, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(ThisCharacter.DamageThreshold.ToString(), 520, 132, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(ThisCharacter.Morale.ToString(), 445, 313, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Archery Gear"), trainingValueIndentTopLeft, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Bludgeon"), trainingValueIndentTopLeft, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Piercing"), trainingValueIndentTopLeft, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Slashing"), trainingValueIndentTopLeft, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Long Gun"), trainingValueIndentTopRight, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Pistol"), trainingValueIndentTopRight, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Throwing"), trainingValueIndentTopRight, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Martial Arts"), trainingValueIndentTopRight, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Athletic Gear"), trainingValueIndentBottomLeft, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Climbing Gear"), trainingValueIndentBottomLeft, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Command Apparatus"), trainingValueIndentBottomLeft, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Firefighting"), trainingValueIndentBottomLeft, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("First Aid Kit"), trainingValueIndentBottomMiddle, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Medical Gear"), trainingValueIndentBottomMiddle, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Reconnaissance Gear"), trainingValueIndentBottomMiddle, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Survival Kit"), trainingValueIndentBottomMiddle, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Swimming/Diving"), trainingValueIndentBottomRight, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Tools"), trainingValueIndentBottomRight, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Value"), trainingValueIndentBottomRight, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));
            page0TransparencyGroup.Add(new Label(GetPlayerTrainingValueValueByBaseTrainingValueName("Vehicles"), trainingValueIndentBottomRight, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left));

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
