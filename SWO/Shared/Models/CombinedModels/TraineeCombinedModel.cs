using SWO.Models.DataModels;

namespace SWO.Shared.Models.CombinedModels
{
    public class TraineeCombinedModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public TraineeCombinedModel()
        {

        }

        public TraineeCombinedModel(Trainee trainee)
        {
            ID = trainee.ID;
            Name = trainee.User.Name;
            Surname = trainee.User.Surname;
        }
    }
}
