using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "PaymentMethods")]

    public class PaymentMode
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Payment Mode ID")]
        public int PaymentModeID { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(50)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Payment Mode")]
        public string PaymentModes { get; set; }



        [Required(ErrorMessage = "Please Provide {0}!")]
        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Available { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
