using SWO.Models;
using SWO.Models.DataModels;
using System;
using System.Collections.Generic;

namespace SWO.Shared.Models.CombinedModels
{
    public class SimulationDetailsModel
    {
        public int ID { get; set; }

        public int GradeSum { get; set; }

        public int SimulationTime { get; set; }

        public DateTime Date { get; set; }

        public int TraineeID { get; set; }

        public int InstructorID { get; set; }

        public string TraineeFullName { get; set; }

        public string InstructorFullName { get; set; }

        public int ScenarioID { get; set; }

        public string ScenarioName { get; set; }

        public int ScenarioMaxGradeSum { get; set; }

        public List<GradeIndexModel> GradeViews { get; set; }

        public SimulationDetailsModel()
        {

        }

        public SimulationDetailsModel(Simulation simulation)
        {
            ID = simulation.ID;
            GradeSum = simulation.GradeSum;
            Date = simulation.Date;

            TraineeID = simulation.Trainee.ID;
            TraineeFullName = simulation.Trainee.User.GetFullName();

            InstructorID = simulation.Instructor.ID;
            InstructorFullName = simulation.Instructor.User.GetFullName();

            ScenarioID = simulation.Scenario.ID;
            ScenarioName = simulation.Scenario.Name;
            ScenarioMaxGradeSum = simulation.Scenario.MaxGradeSum;
        }

        public SimulationDetailsModel(Simulation simulation, List<GradeIndexModel> grades)
        {
            ID = simulation.ID;
            GradeSum = simulation.GradeSum;
            Date = simulation.Date;

            TraineeID = simulation.Trainee.ID;
            TraineeFullName = simulation.Trainee.User.GetFullName();

            InstructorID = simulation.Instructor.ID;
            InstructorFullName = simulation.Instructor.User.GetFullName();

            ScenarioID = simulation.Scenario.ID;
            ScenarioName = simulation.Scenario.Name;
            ScenarioMaxGradeSum = simulation.Scenario.MaxGradeSum;
            GradeViews = grades;
            foreach(var grade in GradeViews)
            {
                SimulationTime += grade.TimeTaken;
            }
        }
    }
}
