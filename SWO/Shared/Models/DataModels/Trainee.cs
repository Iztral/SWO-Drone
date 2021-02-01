using SWO.Shared.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class Trainee : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual Member User { get; set; }
#nullable enable
        public virtual ICollection<Simulation>? Simulations { get; set; }
    }
}
