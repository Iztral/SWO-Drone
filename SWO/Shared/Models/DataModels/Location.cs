using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Location : BaseEntity
    {
        [Required]
        public string Name { get; set; }

#nullable enable
        [Required]
        public string? Address { get; set; }

        public string? Website { get; set; }

        public string? Description { get; set; }

        public string? Photo { get; set; }

        public virtual List<Simulator>? Simulators { get; set; }
    }
}
