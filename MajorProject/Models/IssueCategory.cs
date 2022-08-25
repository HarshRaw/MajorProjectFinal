using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "IssueCategories")]
    public class IssueCategory
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Issue Category ID")]
        public int IssueCategoryId { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(50)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Issue")]
        public string Issue { get; set; }

        public ICollection<Issue> Issues { get; set; }

    }
}
