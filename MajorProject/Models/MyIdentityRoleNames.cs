using System.ComponentModel.DataAnnotations;

namespace MajorProject.Models
{
    public enum MyIdentityRoleNames
    {
        [Display(Name ="Admin Role")]
        RoleAdmin,

        [Display(Name = "User Role")]
        RoleUser
    }
}
