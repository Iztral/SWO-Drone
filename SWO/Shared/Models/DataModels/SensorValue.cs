using SWO.Shared.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class SensorValue : BaseEntity
    {
        public string Value { get; set; }

        [Required]
        [ForeignKey("Sensor")]
        public int SensorID { get; set; }

        [Required]
        [ForeignKey("Simulation")]
        public int SimulationID { get; set; }

        public virtual Sensor Sensor { get; set; }
        public virtual Simulation Simulation { get; set; }
    }
}
