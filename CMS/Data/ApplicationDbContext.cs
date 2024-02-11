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
        public DbSet<Visit> Visit { get; set; } = default!;
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
    }
}
