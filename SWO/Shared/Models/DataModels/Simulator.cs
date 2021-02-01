using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public enum SimType
    {
        [Description("DJI Phantom")]
        DJI_Phantom,
        [Description("DJI Mavic")]
        DJI_Mavic,
        [Description("DJI Mini")]
        DJI_Mini,
        [Description("VR DJI Phantom")]
        VR_DJI_Phantom,
        [Description("VR DJI Mini")]
        VR_DJI_Mavic,
        [Description("VR DJI Mini")]
        VR_DJI_Mini,
        [Description("Wszystkie")]
        All = -1
    }

    public class Simulator : BaseEntity
    {
        [Required]
        public string Name { get; set; }

#nullable enable
        [Required]
        public SimType Type { get; set; }

        [ForeignKey("Location")]
        [Required]
        public int LocationID { get; set; }

        public string? Photo { get; set; }

        public string? Description { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Sensor>? Sensors { get; set; }
        public virtual ICollection<GradeTemplate>? GradeTemplates { get; set; }
        public virtual ICollection<Scenario>? Scenarios { get; set; }
    }
}
