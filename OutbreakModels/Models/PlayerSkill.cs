using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class PlayerSkill
    {
        public int Id { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public BaseSkill BaseSkill { get; set; }
        public int Value { get; set; } = 0;
        public int AttributeValue { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public int Advancements { get; set; }
        public List<int> AdvancementsList { get; set; } = new List<int>();
        public bool IsSupported { get; set; }
        public bool IsSpecialized { get; set; }
        public string Specialty { get; set; }

    }
}
