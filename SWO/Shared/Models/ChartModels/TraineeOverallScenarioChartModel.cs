using System;
using System.Collections.Generic;
using System.Text;

namespace SWO.Shared.Models.ChartModels
{
    public class TraineeOverallScenarioChartModel
    {
        public int TraineeID { get; set; }

        public List<OverallScenarioChartModel> ScenarioChartModels { get; set; }

        public TraineeOverallScenarioChartModel()
        {

        }

        public TraineeOverallScenarioChartModel(int traineeID, List<OverallScenarioChartModel> scenarioChartModels)
        {
            TraineeID = traineeID;
            ScenarioChartModels = scenarioChartModels;
        }
    }
}
