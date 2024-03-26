using CMS.Models;
using CMS.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Visit> Visit { get; set; } = default!;

        public async Task<List<Visit>> GetVisitsForDateRange(DateTime startDate, DateTime endDate)
        {
            return await Visit
                .Include(v => v.Patient)
                .Include(v => v.Employee)
                .Where(p => p.VisitTime >= startDate && p.VisitTime < endDate)
                .ToListAsync();
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            var property = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));
            if (property != null)
            {
                return property.GetValue(this) as DbSet<T>;
            }

            throw new InvalidOperationException($"DbSet<{typeof(T).Name}> not found.");
        }

        public async Task<List<T>> GetPersonByName<T>(string name) where T : class, IFullName
        {
            var dbSet = GetDbSet<T>();
            var query = dbSet.AsQueryable();

            return await query.Where(p => EF.Functions.Like(p.Name, $"%{name}%") || EF.Functions.Like(p.Surname, $"%{name}%")).ToListAsync();
        }
    }
}