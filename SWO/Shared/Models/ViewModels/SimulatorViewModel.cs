using SWO.Models.DataModels;

namespace SWO.Shared.Models.ViewModels
{
    public class SimulatorViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public SimType Type { get; set; }

        public int LocationID { get; set; }

#nullable enable
        public string? Photo { get; set; }

        public string? Description { get; set; }
    }
}
