using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class GradeTemplate : BaseEntity
    {
        [Required]
        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }

        [Required]
        public int MaxPoints { get; set; }

        [ForeignKey("Simulator")]
        [Required]
        public int SimulatorID { get; set; }

        public string? Note { get; set; }

        public int? Phase { get; set; }

        public int OptimalTime { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual Simulator Simulator { get; set; }
        public virtual ICollection<ScenarioGradesTemplates>? ScenarioTemplate { get; set; }
    }
}
