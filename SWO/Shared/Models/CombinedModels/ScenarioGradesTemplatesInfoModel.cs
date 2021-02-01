using SWO.Models.DataModels;

namespace SWO.Shared.Models.CombinedModels
{
    public class ScenarioGradesTemplatesInfoModel
    {
        public int ID { get; set; }

        public bool Assigned { get; set; }

        public int TemplateID { get; set; }

        public int ScenarioID { get; set; }

        public string Name { get; set; }

#nullable enable
        public string? Description { get; set; }

        public int MaxPoints { get; set; }

        public string? Note { get; set; }

        public int? Phase { get; set; }

        public ScenarioGradesTemplatesInfoModel()
        {

        }

        public ScenarioGradesTemplatesInfoModel(ScenarioGradesTemplates scenarioGradesTemplates)
        {
            ID = scenarioGradesTemplates.ID;
            Assigned = scenarioGradesTemplates.Assigned;
            ScenarioID = scenarioGradesTemplates.ScenarioID;
            TemplateID = scenarioGradesTemplates.GradeTemplate.ID;
            Name = scenarioGradesTemplates.GradeTemplate.Name;
            Description = scenarioGradesTemplates.GradeTemplate.Description;
            MaxPoints = scenarioGradesTemplates.GradeTemplate.MaxPoints;
            Note = scenarioGradesTemplates.GradeTemplate.Note;
            Phase = scenarioGradesTemplates.GradeTemplate.Phase;
        }
    }
}
