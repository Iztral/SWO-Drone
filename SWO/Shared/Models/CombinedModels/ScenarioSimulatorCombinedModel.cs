using SWO.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWO.Shared.Models.CombinedModels
{
    public class ScenarioSimulatorCombinedModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }
#nullable disable

        public int SimulatorID { get; set; }

        public int MaxGradeSum { get; set; }

        public string SimulatorName { get; set; }

        public ScenarioSimulatorCombinedModel()
        {

        }

        public ScenarioSimulatorCombinedModel(Scenario scenario)
        {
            ID = scenario.ID;
            Name = scenario.Name;
            Description = scenario.Description;
            SimulatorID = scenario.SimulatorID;
            MaxGradeSum = scenario.MaxGradeSum;
            SimulatorName = scenario.Simulator.Name;
        }
    }
}
