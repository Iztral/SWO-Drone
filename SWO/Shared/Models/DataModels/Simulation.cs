using SWO.Shared.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Simulation : BaseEntity
    {
        [ForeignKey("Trainee")]
        [Required]
        public int TraineeID { get; set; }

        [ForeignKey("Instructor")]
        [Required]
        public int InstructorID { get; set; }

        [ForeignKey("Scenario")]
        [Required]
        public int ScenarioID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int GradeSum { get; set; }

        public virtual Trainee Trainee { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Scenario Scenario { get; set; }
#nullable enable
        public virtual ICollection<Grade>? Grades { get; set; }
    }
}
