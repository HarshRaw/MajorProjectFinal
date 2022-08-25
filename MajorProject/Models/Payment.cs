using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel;

namespace MajorProject.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Payment ID")]
        public int PaymentId { get; set; }



        #region ServiceBooking Link

        [Display(Name = "Service Booking")]
        public int ServiceBookingID { get; set; }
        [ForeignKey(nameof(Payment.ServiceBookingID))]
        public ServiceBooking ServiceBookings { get; set; }

        #endregion

        #region Payment Link

        [Display(Name = "Payment Method")]
        public int PaymentMethodID { get; set; }
        [ForeignKey(nameof(Payment.PaymentMethodID))]
        public PaymentMode PaymentModes { get; set; }

        #endregion


        [Required(ErrorMessage = "Please Provide {0}!")]
        //[RegularExpression(@"^([a-zA-Z0-9_-+]{3,}@[a-zA-Z]{3,})$", ErrorMessage = "Invalid! UPI ID")]
        [Display(Name = "UPI Id")]
        public string UPIID { get; set; }


        [Required(ErrorMessage = "Please Provide {0}!")]
        [DefaultValue(false)]
        [Display(Name = "Payment Status")]
        public bool PStatus { get; set; }
    }
}
