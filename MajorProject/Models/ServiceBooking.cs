using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "ServiceBookings")]
    public class ServiceBooking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Service Booking ID")]
        public int ServiceBookingId { get; set; }

        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        //public DateTime Date { get; set; } = DateTime.Now;
        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;

        #region Issue Link

        [Display(Name = "Issue")]
        public int Issue { get; set; }
        [ForeignKey(nameof(ServiceBooking.Issue))]
        public Issue Issues { get; set; }

        #endregion

        #region Service Link

        [Display(Name = "Service")]
        public int Service { get; set; }
        [ForeignKey(nameof(ServiceBooking.Service))]
        public Service Services { get; set; }

        #endregion

        [Required(ErrorMessage = "Please Provide {0}!")]
        [Column(TypeName = "varchar(250)")]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(250, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Current Location Of Car")]
        public string CurrentLocationOfCar { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
