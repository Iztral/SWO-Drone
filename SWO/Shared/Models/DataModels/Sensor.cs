using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Sensor : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

#nullable enable
        public string? Addendum { get; set; }

        [Required]
        [ForeignKey("Simulator")]
        public int SimulatorID { get; set; }

        public virtual Simulator Simulator { get; set; }
        public virtual ICollection<SensorValue>? SensorValues { get; set; }
    }
}
