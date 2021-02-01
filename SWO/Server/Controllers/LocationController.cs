using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Data;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public LocationController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = await _context.Locations.ToListAsync();
            var locationModels = _mapper.Map<List<Location>, List<LocationViewModel>>(locations);

            return Ok(locationModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(a => a.ID == id);
            var locationModel = _mapper.Map<Location, LocationViewModel>(location);

            return Ok(locationModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(LocationViewModel locationModel)
        {
            var location = _mapper.Map<LocationViewModel, Location>(locationModel);
            _context.Add(location);
            await _context.SaveChangesAsync();

            return Ok(location.ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(LocationViewModel locationModel)
        {
            var location = _mapper.Map<LocationViewModel, Location>(locationModel);
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = new Location { ID = id };
            _context.Remove(location);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
