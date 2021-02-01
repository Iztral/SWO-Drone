using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models;
using SWO.Models.DataModels;
using SWO.Server.Data;
using SWO.Shared.Models.CombinedModels;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public SimulatorController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var simulators = await _context.Simulators.ToListAsync();
            var simulatorModels = _mapper.Map<List<Simulator>, List<SimulatorViewModel>>(simulators);
            return Ok(simulatorModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("location")]
        public async Task<IActionResult> GetWithLocation()
        {
            var simulators = await _context.Simulators.ToListAsync();
            var simulatorsWithLocations = (simulators.Select(simulator => new SimulatorLocationCombinedModel(simulator))).ToList();

            return Ok(simulatorsWithLocations);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var simulator = await _context.Simulators.FirstOrDefaultAsync(a => a.ID == id);
            var simulatorModel = _mapper.Map<Simulator, SimulatorViewModel>(simulator);
            return Ok(simulatorModel);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}/location")]
        public async Task<IActionResult> GetWithLocation(int id)
        {
            var simulator = await _context.Simulators.FirstOrDefaultAsync(a => a.ID == id);
            var simulatorWithLocation = new SimulatorLocationCombinedModel(simulator);

            return Ok(simulatorWithLocation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(SimulatorViewModel simulatorModel)
        {
            var simulator = _mapper.Map<SimulatorViewModel, Simulator>(simulatorModel);
            _context.Add(simulator);
            await _context.SaveChangesAsync();
            return Ok(simulator.ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(SimulatorViewModel simulatorModel)
        {
            var simulator = _mapper.Map<SimulatorViewModel, Simulator>(simulatorModel);
            _context.Entry(simulator).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var simulator = new Simulator { ID = id };
            _context.Remove(simulator);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
