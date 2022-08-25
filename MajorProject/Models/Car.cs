using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "Cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Customer ID")]
        public int CarId { get; set; }



        #region CarModel Link

        [Display(Name = "Car Model")]
        public int CarMID { get; set; }
        [ForeignKey(nameof(Car.CarMID))]
        public CarModel CarModels { get; set; }

        #endregion








        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(15)")]
        [MinLength(6, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(15, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Car Number")]
        public string CarNumber { get; set; }



        #region Customer Link

        [Display(Name = "Customer")]
        public int CustomerID { get; set; }
        [ForeignKey(nameof(Car.CustomerID))]
        public Customer Customers { get; set; }

        #endregion

        public ICollection<Issue> Issues { get; set; }

    }
}
