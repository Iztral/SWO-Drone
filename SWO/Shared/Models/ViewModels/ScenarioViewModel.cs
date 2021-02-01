namespace SWO.Shared.Models.ViewModels
{
    public class ScenarioViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }

        public int SimulatorID { get; set; }

        public int MaxGradeSum { get; set; }
    }
}
