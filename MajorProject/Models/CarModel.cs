using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "CarModels")]
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Car Model ID")]
        public int CarCompanyId { get; set; }

        #region Car Link

        [Display(Name = "Car Company")]
        public int CarCID { get; set; }
        [ForeignKey(nameof(CarModel.CarCID))]
        public CarCompany CarCompanies { get; set; }

        #endregion


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(25)")]
        [MinLength(3, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(15, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Car Model")]
        public string CarModels { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
