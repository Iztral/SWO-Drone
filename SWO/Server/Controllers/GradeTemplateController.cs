using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Controllers.Extensions;
using SWO.Server.Data;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTemplateController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public GradeTemplateController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gradeTemplates = await _context.GradeTemplates.ToListAsync();
            var gradeTemplateModels = _mapper.Map<List<GradeTemplate>, List<GradeTemplateViewModel>>(gradeTemplates);

            return Ok(gradeTemplateModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("simulator/{id}")]
        public async Task<IActionResult> GetGradeTemplatesFromSimulator(int id)
        {
            var gradeTemplates = await _context.GradeTemplates.Where(x => x.SimulatorID == id).ToListAsync();
            var gradeTemplateModels = _mapper.Map<List<GradeTemplate>, List<GradeTemplateViewModel>>(gradeTemplates);

            return Ok(gradeTemplateModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("scenario/{scenarioID}")]
        public async Task<IActionResult> GetGradeTemplatesFromScenario(int scenarioID)
        {
            var assignedTemplates = await _context.ScenarioGradesTemplates.Where(x => x.ScenarioID == scenarioID && x.Assigned == true).ToListAsync();
            List<GradeTemplateViewModel> gradeTemplateModels = new List<GradeTemplateViewModel>();
            foreach (var assignedTemplate in assignedTemplates)
            {
                var model = _mapper.Map<GradeTemplate, GradeTemplateViewModel>(assignedTemplate.GradeTemplate);
                gradeTemplateModels.Add(model);
            }

            return Ok(gradeTemplateModels);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var gradeTemplate = await _context.GradeTemplates.FirstOrDefaultAsync(a => a.ID == id);
            var gradeTemplateModel = _mapper.Map<GradeTemplate, GradeTemplateViewModel>(gradeTemplate);

            return Ok(gradeTemplateModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(GradeTemplateViewModel gradeTemplateModel)
        {
            var gradeTemplate = _mapper.Map<GradeTemplateViewModel, GradeTemplate>(gradeTemplateModel);
            _context.Add(gradeTemplate);
            await _context.SaveChangesAsync();
            new AutomateScenarioGrade(_context).AddNewPotentialTemplateToSimulatorScenarios(gradeTemplate);
            
            return Ok(gradeTemplate.ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(GradeTemplateViewModel gradeTemplateModel)
        {
            var gradeTemplate = _mapper.Map<GradeTemplateViewModel, GradeTemplate>(gradeTemplateModel);
            _context.Entry(gradeTemplate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            new AutomateScenarioGrade(_context).UpdateScenariosMaxGradeSumOnTemplateChange(gradeTemplate);
            
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gradeTemplate = new GradeTemplate { ID = id };
            _context.Remove(gradeTemplate);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
