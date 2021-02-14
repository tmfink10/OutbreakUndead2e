using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class PlayerCharacter
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public string Sex { get; set; }
        public ICollection<PlayerAttribute> PlayerAttributes { get; set; } = new List<PlayerAttribute>();
        public ICollection<PlayerSkill> PlayerSkills { get; set; } = new List<PlayerSkill>();
        public ICollection<PlayerAbility> PlayerAbilities { get; set; } = new List<PlayerAbility>();
        public ICollection<PlayerTrainingValue> TrainingValues { get; set; } = new List<PlayerTrainingValue>();
        public int SurvivalPoints { get; set; } = 0;
        public int GestaltLevel { get; set; } = 0;
        public int CargoCapacity { get; set; } = 0;
        public int CompetencePoints { get; set; } = 0;
        public int HealthPoints { get; set; } = 0;
        public int DamageThreshold { get; set; } = 0;
        public int Morale { get; set; } = 0;
        public string Notes { get; set; } = "";
    }
}
