using SWO.Models;
using SWO.Models.DataModels;

namespace SWO.Shared.Models.CombinedModels
{
    public class GradeIndexModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public int TimeTaken { get; set; }

        public int MaxPoints { get; set; }

        public string Addendum { get; set; }

        public GradeIndexModel()
        {

        }

        public GradeIndexModel(Grade grade, GradeTemplate gradeTemplate)
        {
            ID = grade.ID;
            Name = gradeTemplate.Name;
            Points = grade.Points;
            TimeTaken = grade.TimeTaken;
            MaxPoints = gradeTemplate.MaxPoints;
            Addendum = grade.Addendum;
        }
    }
}
