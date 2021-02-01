using SWO.Models.DataModels;

namespace SWO.Shared.Models.ViewModels
{
    public class MemberViewModel
    {
        public int ID { get; set; }

        public string IdentityID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Role Role { get; set; }
    }
}
