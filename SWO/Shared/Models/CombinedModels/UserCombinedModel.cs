using SWO.Models.DataModels;

namespace SWO.Shared.Models.CombinedModels
{
    public class UserCombinedModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public UserCombinedModel(Member user)
        {
            ID = user.ID;
            Name = user.Name;
            Surname = user.Surname;
        }

        public UserCombinedModel()
        {

        }
    }
}
