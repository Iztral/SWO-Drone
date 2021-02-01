using System;

namespace SWO.Shared.Models.ViewModels
{
    public class SimulationViewModel
    {
        public int ID { get; set; }

        public int TraineeID { get; set; }

        public int InstructorID { get; set; }

        public int ScenarioID { get; set; }

        public DateTime Date { get; set; }

        public int GradeSum { get; set; }
    }
}
