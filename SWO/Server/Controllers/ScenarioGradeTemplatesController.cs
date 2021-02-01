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
    [Route("api/scengradetemp")]
    [ApiController]
    public class ScenarioGradeTemplatesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ScenarioGradeTemplatesController(ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            this._context = context;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("scenario/{scenarioID}")]
        public async Task<IActionResult> GetGradeTemplatesForScenario(int scenarioID)
        {
            List<ScenarioGradesTemplatesInfoModel> gradeInfos = new List<ScenarioGradesTemplatesInfoModel>();
            foreach (var scenarioGradesTemplates in await _context.ScenarioGradesTemplates.Where(x => x.ScenarioID == scenarioID).ToListAsync())
            {
                gradeInfos.Add(new ScenarioGradesTemplatesInfoModel(scenarioGradesTemplates));
            }

            return Ok(gradeInfos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(ScenarioGradesTemplatesViewModel scenarioGradesTemplatesModel)
        {
            var scenarioGradesTemplates = _mapper.Map<ScenarioGradesTemplatesViewModel, ScenarioGradesTemplates>(scenarioGradesTemplatesModel);
            _context.Entry(scenarioGradesTemplates).State = EntityState.Modified;
            new AutomateScenarioGrade(_context).UpdateScenarioMaxGradeSum(scenarioGradesTemplates.ScenarioID);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("list")]
        public async Task<IActionResult> PutList(List<ScenarioGradesTemplatesViewModel> scenarioGradeTemplateModels)
        {
            var scenarioGradesTemplates = _mapper.Map<List<ScenarioGradesTemplatesViewModel>, List<ScenarioGradesTemplates>>(scenarioGradeTemplateModels);

            foreach (var item in scenarioGradesTemplates)
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            new AutomateScenarioGrade(_context).UpdateScenarioMaxGradeSum(scenarioGradesTemplates.First().ScenarioID);
            return NoContent();
        }
    }
}
