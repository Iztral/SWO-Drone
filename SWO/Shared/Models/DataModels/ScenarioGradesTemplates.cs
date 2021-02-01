using SWO.Shared.Models.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWO.Models.DataModels
{
    public class ScenarioGradesTemplates : BaseEntity
    {
        [ForeignKey("Scenario")]
        public int ScenarioID { get; set; }

        [ForeignKey("GradeTemplate")]
        public int TemplateID { get; set; }

        public bool Assigned { get; set; }

        public ScenarioGradesTemplates(int scenarioID, int templateID)
        {
            ScenarioID = scenarioID;
            TemplateID = templateID;
            Assigned = false;
        }

        public virtual Scenario Scenario { get; set; }
        public virtual GradeTemplate GradeTemplate { get; set; }
    }
}
