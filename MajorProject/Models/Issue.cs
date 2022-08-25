using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table(name: "Issues")]
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Issue ID")]
        public int IssueId { get; set; }



        #region Car Link

        [Display(Name = "Car Number")]
        public int Car { get; set; }
        [ForeignKey(nameof(Issue.Car))]
        public Car Cars { get; set; }

        #endregion


        #region Urgency Link

        [Display(Name = "Urgency")]
        public int Urgency { get; set; }
        [ForeignKey(nameof(Issue.Urgency))]
        public Urgency Urgencies { get; set; }

        #endregion


        #region IssueCategory Link

        [Display(Name = "Issue")]
        public int IssueCategory { get; set; }
        [ForeignKey(nameof(Issue.IssueCategory))]
        public IssueCategory IssueCategories { get; set; }

        #endregion

        [Display(Name = "Issue Description")]
        public string Services { get; set; } = null;


        public ICollection<ServiceBooking> ServiceBookings { get; set; }
    }
}
