using System;
using System.Collections.Generic;
using System.Text;

namespace OutbreakModels.Models
{
    public class PlayerTrainingValue
    {
        public int Id { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public BaseTrainingValue BaseTrainingValue { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
