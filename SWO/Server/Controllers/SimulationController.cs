using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Data;
using SWO.Shared.Models.CombinedModels;
using SWO.Shared.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public SimulationController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var simulations = await _context.Simulations.ToListAsync();
            var simulationModels = _mapper.Map<List<Simulation>, List<SimulationViewModel>>(simulations);

            return Ok(simulationModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("details")]
        public async Task<IActionResult> GetAllSimulationsWithDetails()
        {
            List<SimulationDetailsModel> simulationDetails = new List<SimulationDetailsModel>();
            foreach (Simulation simulation in await _context.Simulations.ToListAsync())
            {
                simulationDetails.Add(new SimulationDetailsModel(simulation));
            }

            return Ok(simulationDetails);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var simulation = await _context.Simulations.FirstOrDefaultAsync(a => a.ID == id);
            var simulationModel = _mapper.Map<Simulation, SimulationViewModel>(simulation);

            return Ok(simulation);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetSingleWithDetails(int id)
        {
            var simulation = await _context.Simulations.Where(x => x.ID == id).FirstAsync();
            var grades = _context.Grades.Where(x => x.SimulationID == id).ToList();

            List<GradeIndexModel> GradeViews = new List<GradeIndexModel>();
            foreach(var grade in grades)
            {
                _context.Entry(grade).Reference(x => x.GradeTemplate).Load();
                var scenariograde = await _context.ScenarioGradesTemplates.FirstAsync(x => x.TemplateID == grade.TemplateID && x.ScenarioID == simulation.ScenarioID);
                if(scenariograde.Assigned)
                    GradeViews.Add(new GradeIndexModel(grade, _context.GradeTemplates.First(x => x.ID == grade.TemplateID)));
            }

            SimulationDetailsModel simulationDetails = new SimulationDetailsModel(simulation, GradeViews);

            return Ok(simulationDetails);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("trainee/{id}/details")]
        public async Task<IActionResult> GetAllWithDetailsForUser(int id)
        {
            List<SimulationDetailsModel> simulationDetails = new List<SimulationDetailsModel>();
            foreach (Simulation simulation in await _context.Simulations.Where(x => x.TraineeID == id).ToListAsync())
            {
                simulationDetails.Add(new SimulationDetailsModel(simulation));
            }
            return Ok(simulationDetails);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("scenario/{id}/details")]
        public async Task<IActionResult> GetAllWithDetailsForScenario(int id)
        {
            List<SimulationDetailsModel> simulationDetails = new List<SimulationDetailsModel>();
            foreach (Simulation simulation in await _context.Simulations.Where(x => x.ScenarioID == id).ToListAsync())
            {
                simulationDetails.Add(new SimulationDetailsModel(simulation));
            }
            return Ok(simulationDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var simulation = new Simulation { ID = id };
            _context.Remove(simulation);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("populate")]
        public async Task<IActionResult> Populate(int i)
        {
            await SeedDatabaseWithSimulations();
            return NoContent();
        }

        public async Task SeedDatabaseWithSimulations()
        {
            List<Trainee> trainees = _context.Trainees.ToList();
            List<Scenario> scenarios = _context.Scenarios.ToList();

            foreach (var trainee in trainees)
            {
                int seed = GenerateRandomInt(3, 6);
                int seed2 = 0;
                foreach (var scenario in scenarios)
                {
                    for (int i = 1; i < 12; i++)
                    {
                        DateTime day = new DateTime(2020, i, GenerateRandomInt(1, 30));
                        var traineeSim = new Simulation
                        {
                            TraineeID = trainee.ID,
                            InstructorID = 1,
                            ScenarioID = scenario.ID,
                            Date = day,
                            GradeSum = seed + i + seed2
                        };
                        _context.Add(traineeSim);
                        await _context.SaveChangesAsync();


                        var assignedTemplates = await _context.ScenarioGradesTemplates.Where(x => x.ScenarioID == scenario.ID && x.Assigned == true).ToListAsync();
                        var gradeTemplates = new List<GradeTemplate>();
                        foreach (var assignedTemplate in assignedTemplates)
                        {
                            gradeTemplates.Add(assignedTemplate.GradeTemplate);
                        }

                        foreach (var assignedTemplate in gradeTemplates)
                        {
                            var grade = new Grade
                            {
                                TemplateID = assignedTemplate.ID,
                                SimulationID = traineeSim.ID,
                                Points = GenerateRandomInt(0, assignedTemplate.MaxPoints),
                                TimeTaken = GenerateRandomInt(0, (assignedTemplate.OptimalTime + 5)),
                                GradeTemplate = null,
                                Simulation = null
                            };
                            _context.Add(grade);
                            await _context.SaveChangesAsync();
                        }
                    }
                    seed2++;
                }
            }
        }

        private static int GenerateRandomInt(int minValue, int maxValue)
        {
            using RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] val = new byte[6];
            crypto.GetBytes(val);
            int randomvalue = BitConverter.ToInt32(val, 1);
            Random rnd = new Random(randomvalue);
            return rnd.Next(minValue, maxValue);
        }
    }
}