using SWO.Models.DataModels;

namespace SWO.Shared.Models.CombinedModels
{
    public class SimulatorLocationCombinedModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public SimType Type { get; set; }

        public int LocationID { get; set; }

#nullable enable
        public string? Photo { get; set; }

        public string? Description { get; set; }
#nullable disable
        public string LocationName { get; set; }

        public SimulatorLocationCombinedModel()
        {

        }

        public SimulatorLocationCombinedModel(Simulator simulator)
        {
            ID = simulator.ID;
            Name = simulator.Name;
            Type = simulator.Type;
            LocationID = simulator.LocationID;
            Photo = simulator.Photo;
            Description = simulator.Description;
            LocationName = simulator.Location.Name;
        }
    }
}
