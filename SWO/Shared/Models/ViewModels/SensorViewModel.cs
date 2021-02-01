namespace SWO.Shared.Models.ViewModels
{
    public class SensorViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

#nullable enable
        public string? Addendum { get; set; }

        public int SimulatorID { get; set; }
    }
}
