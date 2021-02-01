using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Controllers.Extensions;
using SWO.Server.Data;
using SWO.Shared.Models.CombinedModels;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenarioController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ScenarioController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scenarios = await _context.Scenarios.ToListAsync();
            var scenarioModels = _mapper.Map<List<Scenario>, List<ScenarioViewModel>>(scenarios);

            return Ok(scenarioModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("simulator")]
        public async Task<IActionResult> GetWithSimulator()
        {
            var scenarios = await _context.Scenarios.ToListAsync();
            var scenariosWithSimulators = (scenarios.Select(scenario => new ScenarioSimulatorCombinedModel(scenario))).ToList();

            return Ok(scenariosWithSimulators);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var scenario = await _context.Scenarios.FirstOrDefaultAsync(a => a.ID == id);
            var scenarioModel = _mapper.Map<Scenario, ScenarioViewModel>(scenario);

            return Ok(scenarioModel);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}/simulator")]
        public async Task<IActionResult> GetWithSimulator(int id)
        {
            var scenario = await _context.Scenarios.FirstOrDefaultAsync(a => a.ID == id);
            var scenarioWithSimulator = new ScenarioSimulatorCombinedModel(scenario);

            return Ok(scenarioWithSimulator);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ScenarioViewModel scenarioModel)
        {
            var scenario = _mapper.Map<ScenarioViewModel, Scenario>(scenarioModel);
            _context.Add(scenario);
            await _context.SaveChangesAsync();
            new AutomateScenarioGrade(_context).AddPotentialTemplatesToNewScenario(scenario.ID);

            return Ok(scenario.ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(ScenarioViewModel scenarioModel)
        {
            var scenario = _mapper.Map<ScenarioViewModel, Scenario>(scenarioModel);
            _context.Entry(scenario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var scenario = new Scenario { ID = id };
            _context.Remove(scenario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
