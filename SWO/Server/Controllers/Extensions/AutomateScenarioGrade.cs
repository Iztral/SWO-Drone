using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Data;
using System.Collections.Generic;
using System.Linq;

namespace SWO.Server.Controllers.Extensions
{
    public class AutomateScenarioGrade
    {
        private ApplicationDBContext Context { get; set; }

        public AutomateScenarioGrade(ApplicationDBContext _context)
        {
            Context = _context;
        }

        #region Scenario MaxGradeSum

        public void UpdateScenariosMaxGradeSumOnTemplateChange(GradeTemplate gradeTemplate)
        {
            Context.Entry(gradeTemplate).Reference(x => x.Simulator).Load();
            var simulator = Context.Entry(gradeTemplate).Entity.Simulator;
            if (simulator.Scenarios != null)
            {
                var scenariosToEdit = simulator.Scenarios.ToList();
                foreach (Scenario scenario in scenariosToEdit)
                {
                    UpdateScenarioMaxGradeSum(scenario.ID);
                }
            }
        }

        public void UpdateScenarioMaxGradeSum(int scenarioID)
        {
            var scenario = Context.Scenarios.FirstOrDefaultAsync(a => a.ID == scenarioID).Result;
            scenario.MaxGradeSum = GetCalculatedAssignedMaxGradeSum(GetAssignedGradesTemplates(scenarioID));

            Context.Entry(scenario).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void UpdateScenarioMaxGradeSum(List<ScenarioGradesTemplates> assignedGradeTemplates)
        {
            var scenario = Context.Scenarios.FirstOrDefaultAsync(a => a.ID == assignedGradeTemplates.First().ScenarioID).Result;

            scenario.MaxGradeSum = GetCalculatedAssignedMaxGradeSum(assignedGradeTemplates);

            Context.Entry(scenario).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public List<ScenarioGradesTemplates> GetAssignedGradesTemplates(int scenarioID)
        {
            return Context.ScenarioGradesTemplates
                    .Where(x => x.ScenarioID == scenarioID && x.Assigned == true)
                    .ToListAsync().Result;
        }

        public int GetCalculatedAssignedMaxGradeSum(List<ScenarioGradesTemplates> assignedGradeTemplates)
        {
            int sum = 0;
            foreach (var assignedGradeTemplate in assignedGradeTemplates)
            {
                Context.Entry(assignedGradeTemplate).Reference(x => x.GradeTemplate).Load();
                sum += assignedGradeTemplate.GradeTemplate.MaxPoints;
            }
            return sum;
        }

        #endregion

        #region ScenarioGradeTemplates
        public void AddNewPotentialTemplateToSimulatorScenarios(GradeTemplate gradeTemplate)
        {
            var scenarioGradesTemplates = new List<ScenarioGradesTemplates>();

            foreach (Scenario scenario in Context.Scenarios.Where(x => x.SimulatorID == gradeTemplate.SimulatorID).ToList())
            {
                scenarioGradesTemplates.Add(new ScenarioGradesTemplates(scenario.ID, gradeTemplate.ID));
            }
            Context.AddRange(scenarioGradesTemplates);
            Context.SaveChanges();
        }

        public void AddPotentialTemplatesToNewScenario(int scenarioID)
        {
            var scenario = Context.Scenarios.Where(x => x.ID == scenarioID).First();
            Context.Entry(scenario).Reference(x => x.Simulator).Load();
            var simulator = scenario.Simulator;
            var templatesToAdd = simulator.GradeTemplates.ToList();
            var scenarioGradesTemplates = (templatesToAdd.Select(templateToAdd => new ScenarioGradesTemplates(scenarioID, templateToAdd.ID))).ToList();
            Context.AddRange(scenarioGradesTemplates);
            Context.SaveChanges();
        }

        #endregion
    }
}
