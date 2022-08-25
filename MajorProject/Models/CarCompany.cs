using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "CarCompanies")]
    public class CarCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Car Company ID")]
        public int CarCompanyId { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(25)")]
        [MinLength(3, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(15, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Car Company")]
        public string CarCompanies { get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
