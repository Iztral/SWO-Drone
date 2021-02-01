using SWO.Models;
using SWO.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWO.Shared.Models.ChartModels
{
    public class SimulationPercentageModel
    {
        public DateTime Date { get; set; }

        public double ScorePercentage { get; set; }

        public SimulationPercentageModel()
        {

        }

        public SimulationPercentageModel(Simulation simulation)
        {
            Date = simulation.Date;
            ScorePercentage = GetScorePercentage(simulation.GradeSum, simulation.Scenario.MaxGradeSum);
        }
        private double GetScorePercentage(int gradeSum, int maxGradeSum)
        {
            return Math.Round(((double)gradeSum / (double)maxGradeSum) * 100, 1);
        }
    }
}
