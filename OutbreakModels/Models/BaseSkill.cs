using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class BaseSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        public string Type { get; set; }
        public int PrimaryAttributeBaseAttributeId { get; set; }
        public BaseAttribute PrimaryAttribute { get; set; }
        public int SecondaryAttributeBaseAttributeId { get; set; }
        public BaseAttribute SecondaryAttribute { get; set; }
    }
}
