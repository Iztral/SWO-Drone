namespace SWO.Shared.Models.ViewModels
{
    public class LocationViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

#nullable enable
        public string? Address { get; set; }

        public string? Website { get; set; }

        public string? Description { get; set; }

        public string? Photo { get; set; }
    }
}
