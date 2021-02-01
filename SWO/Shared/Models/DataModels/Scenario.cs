using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Scenario : BaseEntity
    {
        [Required]
        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }

        [ForeignKey("Simulator")]
        [Required]
        public int SimulatorID { get; set; }

        public int MaxGradeSum { get; set; }

        public virtual ICollection<Simulation>? Simulations { get; set; }
        public virtual Simulator Simulator { get; set; }
        public virtual ICollection<ScenarioGradesTemplates>? ScenarioTemplate { get; set; }
    }
}
