using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class BaseAbility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        public List<BaseAttribute> UsesBaseAttributes { get; set; } = new List<BaseAttribute>();
        public List<BaseSkill> SupportsBaseSkills { get; set; } = new List<BaseSkill>();
        public List<BaseTrainingValue> ModifiesBaseTrainingValues { get; set; } = new List<BaseTrainingValue>();
        public bool AdvancesSkills { get; set; }
        public string Type { get; set; }
        public bool IsProfessional { get; set; }
        public string UsesBaseAttributesCoded { get; set; }
        public string ModifiesTrainingValuesCoded { get; set; }
        public string ModifiesTrainingValuesOptionsCoded { get; set; }
    }
}
