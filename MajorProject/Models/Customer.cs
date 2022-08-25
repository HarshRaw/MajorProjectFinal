using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(100)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(100, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid! Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Please Provide {0}")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid {0} Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(250)")]
        [MinLength(20, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(250, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public ICollection<Car> Cars { get; set; }


    }
}
