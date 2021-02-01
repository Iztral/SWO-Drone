using SWO.Models.DataModels;

namespace SWO.Shared.Models.ViewModels
{
    public class GradeTemplateViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }

        public int MaxPoints { get; set; }

        public int SimulatorID { get; set; }

        public string? Note { get; set; }

        public int? Phase { get; set; }

        public int OptimalTime { get; set; }

    }
}
