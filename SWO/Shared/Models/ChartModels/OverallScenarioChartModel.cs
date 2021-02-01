using SWO.Models;
using SWO.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWO.Shared.Models.ChartModels
{
    public class OverallScenarioChartModel
    {
        public int ScenarioID { get; set; }

        public string Name { get; set; }

        public double AverageScore { get; set; }

        public List<SimulationPercentageModel> DataItems { get; set; }

        public OverallScenarioChartModel()
        {

        }

        public OverallScenarioChartModel(Scenario scenario, List<SimulationPercentageModel> dataItems)
        {
            ScenarioID = scenario.ID;
            Name = scenario.Name;
            DataItems = dataItems;

            if (DataItems.Count > 0)
                AverageScore = Math.Round(DataItems.Average(x => x.ScorePercentage), 2);
            else
                AverageScore = 0;
        }

    }
}
