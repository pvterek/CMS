using CMS.Models;
using CMS.ViewModels;
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
        public DbSet<Visit> Visit { get; set; } = default!;
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;

        public async Task<List<VisitViewModel>> GetVisitsForDateRange(DateTime startDate, DateTime endDate)
        {
            return await Visit
                .Where(p => p.VisitTime >= startDate && p.VisitTime < endDate)
                .Join(Patient, visit => visit.PatientId, patient => patient.PatientId, (visit, patient) => new { visit, patient })
                .Join(Employee, vp => vp.visit.EmployeeId, employee => employee.EmployeeId, (vp, employee) => new VisitViewModel
                {
                    Visit = vp.visit,
                    Patient = vp.patient,
                    Employee = employee
                })
                .ToListAsync();
        }
    }
}
