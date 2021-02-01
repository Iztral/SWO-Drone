using SWO.Shared.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Grade : BaseEntity
    {
        [ForeignKey("GradeTemplate")]
        [Required]
        public int TemplateID { get; set; }

        [ForeignKey("Simulation")]
        [Required]
        public int SimulationID { get; set; }

        [Required]
        public int Points { get; set; }

        public int TimeTaken { get; set; }

#nullable enable
        public string? Addendum { get; set; }

        public virtual Simulation Simulation { get; set; } = new Simulation();
        public virtual GradeTemplate GradeTemplate { get; set; } = new GradeTemplate();
    }
}
