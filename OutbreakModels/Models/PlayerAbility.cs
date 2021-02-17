using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class PlayerAbility
    {
        public int Id { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public int BaseAbilityId { get; set; }
        public BaseAbility BaseAbility { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; } = " ";
        public int Tier { get; set; }
        public List<PlayerSkill> SupportsPlayerSkills { get; set; } = new List<PlayerSkill>();
        public string AddedUsingBaseAttributeCode { get; set; }
        public List<string> AdvancedUsing { get; set; } = new List<string>();
    }
}
