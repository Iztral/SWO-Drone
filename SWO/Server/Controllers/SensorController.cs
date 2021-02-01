using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Data;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public SensorController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sensors = await _context.Sensors.ToListAsync();
            var sensorModels = _mapper.Map<List<Sensor>, List<SensorViewModel>>(sensors);

            return Ok(sensorModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(a => a.ID == id);
            var sensorModel = _mapper.Map<Sensor, SensorViewModel>(sensor);

            return Ok(sensorModel);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("simulator/{id}")]
        public async Task<IActionResult> GetSensorsFromSimulator(int id)
        {
            var sensors = await _context.Sensors.Where(x => x.SimulatorID == id).ToListAsync();
            var sensorModels = _mapper.Map<List<Sensor>, List<SensorViewModel>>(sensors);

            return Ok(sensorModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(SensorViewModel sensorModel)
        {
            var sensor = _mapper.Map<SensorViewModel, Sensor>(sensorModel);
            _context.Add(sensor);
            await _context.SaveChangesAsync();

            return Ok(sensor.ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(SensorViewModel sensorModel)
        {
            var sensor = _mapper.Map<SensorViewModel, Sensor>(sensorModel);
            _context.Entry(sensor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = new Sensor { ID = id };
            _context.Remove(sensor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
