using System.Collections.Generic;

namespace SmashHub.Domain.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VariableName { get; set; }
        public int YPosition { get; set; }
        public decimal ReleaseOrder { get; set; }
        public List<Combo> Combos { get; set; }
    }
}