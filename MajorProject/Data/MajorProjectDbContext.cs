using Microsoft.EntityFrameworkCore;
using MajorProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MajorProject.Data
{
    public class MajorProjectDbContext : IdentityDbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueCategory> IssueCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceBooking> ServiceBookings { get; set; }
        public DbSet<Urgency> Urgencies { get; set; } 


        public MajorProjectDbContext(DbContextOptions<MajorProjectDbContext> options) : base(options)
        {

        }


        public DbSet<MajorProject.Models.CarCompany> CarCompany { get; set; }


        public DbSet<MajorProject.Models.CarModel> CarModel { get; set; }
    }
}
