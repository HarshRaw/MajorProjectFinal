using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{

    [Table(name: "Services")]

    public class Service
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Service ID")]
        public int ServiceID { get; set; }



        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(100)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(100, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Services")]
        public string Services { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(350)")]
        [MinLength(10, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(350, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Service Description")]
        public string ServiceDescription { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [DefaultValue(10)]
        public float Price { get; set; }

        public ICollection<ServiceBooking> ServiceBookings { get; set; }
    }
}
