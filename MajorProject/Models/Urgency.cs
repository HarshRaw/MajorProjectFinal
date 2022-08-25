using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "Urgencies")]
    public class Urgency
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Issue Category ID")]
        public int UrgencyId { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(50)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Urgency")]
        public string Urgencies { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(50)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Time To Reach")]
        public string TimeToReach { get; set; }

        public ICollection<Issue> Issues { get; set; }  
    }
}
