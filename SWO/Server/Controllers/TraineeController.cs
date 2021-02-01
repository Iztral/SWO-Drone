using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Data;
using SWO.Shared.Models.ChartModels;
using SWO.Shared.Models.CombinedModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TraineeController(ApplicationDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trainees = await _context.Trainees.ToListAsync();
            List<TraineeCombinedModel> traineeViewModels = new List<TraineeCombinedModel>();
            foreach (var trainee in trainees)
            {
                traineeViewModels.Add(new TraineeCombinedModel(trainee));
            }
            return Ok(traineeViewModels);
        }

        [HttpGet("{traineeID}")]
        public async Task<IActionResult> Get(int traineeID)
        {
            var trainee = await _context.Trainees.FirstAsync(x => x.ID == traineeID);
            return Ok(new TraineeCombinedModel(trainee));
        }

        [HttpGet("hasSimulationsInScenario/{scenarioID}")]
        public async Task<IActionResult> GetTraineeWithSimulationsInScenario(int scenarioID)
        {
            var scenario = await _context.Scenarios.FirstAsync(x => x.ID == scenarioID);
            var trainees = new List<TraineeCombinedModel>();
            foreach(var simulation in scenario.Simulations)
            {
                var trainee = simulation.Trainee;
                if(!trainees.Exists(x => x.ID == trainee.ID))
                {
                    trainees.Add(new TraineeCombinedModel(trainee));
                }
            }
            return Ok(trainees);
        }
        
        [HttpGet("{traineeID}/lastsimulations/{scenarioID}")]
        public async Task<IActionResult> GetMostRecentSimulationForTrainee(int traineeID, int scenarioID)
        {
            var trainee = await _context.Trainees.FirstAsync(x => x.ID == traineeID);
            var lastSimulation = trainee.Simulations.Where(x => x.ScenarioID == scenarioID).OrderBy(x => x.Date).Last();
            var simulationGrades = await _context.Grades.Where(x => x.SimulationID == lastSimulation.ID).ToListAsync();
            SimulationGradesCombinedModel simulation = new SimulationGradesCombinedModel(lastSimulation, simulationGrades);

            return Ok(simulation);
        }


        [HttpGet("scenario/{traineeID}")]
        public async Task<IActionResult> GetScenariosFromSimulator(int traineeID)
        {
            var overallScenarios = new List<OverallScenarioChartModel>();
            var traineeSimulation = GetTraineeSimulations(traineeID);
            foreach (Scenario scenario in await _context.Scenarios.ToListAsync())
            {
                overallScenarios.Add(new OverallScenarioChartModel(scenario, GetDataItemsFromSimulations(traineeSimulation.Where(x => x.ScenarioID == scenario.ID).ToList())));
            }

            return Ok(overallScenarios);
        }

        [HttpGet("scenario/compare/{traineeID}")]
        public async Task<IActionResult> GetScenariosFromSimulatorCompare(int traineeID)
        {
            List<OverallScenarioChartModel> overallScenarios = new List<OverallScenarioChartModel>();
            var traineeSimulation = GetTraineeSimulations(traineeID);
            foreach (Scenario scenario in await _context.Scenarios.ToListAsync())
            {
                overallScenarios.Add(new OverallScenarioChartModel(scenario, GetDataItemsFromSimulations(traineeSimulation.Where(x => x.ScenarioID == scenario.ID).ToList())));
            }

            return Ok(new TraineeOverallScenarioChartModel(traineeID, overallScenarios));
        }

        private List<Simulation> GetTraineeSimulations(int traineeID)
        {
            var trainee = _context.Trainees.FirstAsync(x => x.ID == traineeID).Result;
            return _context.Trainees.FirstAsync(x => x.ID == traineeID).Result.Simulations.ToList();
        }

        private List<SimulationPercentageModel> GetDataItemsFromSimulations(List<Simulation> userScenarioSimulations)
        {
            return userScenarioSimulations.Select(simulation => new SimulationPercentageModel(simulation)).ToList();
        }
    }
}
