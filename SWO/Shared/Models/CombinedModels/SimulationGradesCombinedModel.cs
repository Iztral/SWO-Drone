using SWO.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace SWO.Shared.Models.CombinedModels
{
    public class SimulationGradesCombinedModel
    {
        public int ID { get; set; }

        public int TraineeID { get; set; }

        public List<Grade> Grades { get; set; }

        public SimulationGradesCombinedModel()
        {

        }

        public SimulationGradesCombinedModel(Simulation simulation, List<Grade> grades)
        {
            ID = simulation.ID;
            TraineeID = simulation.TraineeID;
            Grades = grades;
        }
    }
}
