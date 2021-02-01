namespace SWO.Shared.Models.ViewModels
{
    public class GradeViewModel
    {
        public int ID { get; set; }

        public int TemplateID { get; set; }

        public int SimulationID { get; set; }

        public int Points { get; set; }
#nullable enable
        public string? Addendum { get; set; }
    }
}
