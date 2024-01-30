using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<VisitModel> VisitModel { get; set; } = default!;
        public DbSet<PatientModel> PatientModel { get; set; } = default!;
        public DbSet<EmployeeModel> EmployeeModel { get; set; } = default!;
    }
}
