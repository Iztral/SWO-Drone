using SWO.Shared.Models.DataModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SWO.Models.DataModels
{
    public enum Role
    {
        [Description("Administrator")]
        Admin,
        [Description("Instruktor")]
        Instructor,
        [Description("Ćwiczący")]
        Trainee,
        [Description("Wszyscy")]
        All = -1
    };

    public class Member : BaseEntity
    {
        [Required]
        public string IdentityID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public Role Role { get; set; }

#nullable enable
        public string? Photo { get; set; }

        public virtual Trainee? Trainee { get; set; }
        public virtual Instructor? Instructor { get; set; }

        public string GetFullName()
        {
            return Name + " " + Surname;
        }
    }
}
