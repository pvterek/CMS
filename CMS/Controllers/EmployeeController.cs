using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CMS.Controllers
{
    public class EmployeeController(ApplicationDbContext context) : Controller
    {
        // GET: EmployeeModels
        public async Task<IActionResult> Index(string? employee)
        {
            var employees = string.IsNullOrEmpty(employee)
                ? await context.Employee.Include(e => e.Profession).ToListAsync()
                : await context.GetPersonByName<Employee>(employee);

            return View(employees);
        }

        // GET: EmployeeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employee
                .Include(e => e.Profession)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: EmployeeModels/Create
        public async Task<IActionResult> Create()
        {
            EmployeeProfessionView viewModel = new()
            {
                Professions = await context.Profession.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employee,Photo")] EmployeeProfessionView viewModel)
        {
            var (employee, photo) = (viewModel.Employee, viewModel.Photo);

            employee.Profession = await context.Profession.FirstOrDefaultAsync(m => m.ProfessionId == employee.ProfessionId);

            if (!ValidateEmployee(employee))
            {
                viewModel.Professions = await context.Profession.ToListAsync();
                return View(viewModel);
            }

            if (photo != null)
            {
                SavePhotoToEmployee(photo, employee);
            }

            context.Add(employee);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new EmployeeProfessionView
            {
                Employee = employee,
                Professions = await context.Profession.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: EmployeeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Employee,Photo")] EmployeeProfessionView viewModel)
        {
            var (employee, photo) = (viewModel.Employee, viewModel.Photo);

            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            employee.Profession = await context.Profession.FirstOrDefaultAsync(m => m.ProfessionId == employee.ProfessionId);

            if (ValidateEmployee(employee))
            {
                if (photo != null)
                {
                    SavePhotoToEmployee(photo, employee);
                }

                try
                {
                    context.Update(employee);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) when (!EmployeeExists(employee.EmployeeId))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            viewModel.Professions = await context.Profession.ToListAsync();
            return View(viewModel);
        }

        // GET: EmployeeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employee
                .Include(e => e.Profession)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: EmployeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await context.Employee.FindAsync(id);
            if (employee != null)
            {
                context.Employee.Remove(employee);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return context.Employee.Any(e => e.EmployeeId == id);
        }

        private bool ValidateEmployee(Employee employee)
        {
            return Validator.TryValidateObject(employee, new ValidationContext(employee), null, true);
        }

        private void SavePhotoToEmployee(IFormFile photo, Employee employee)
        {
            using var memoryStream = new MemoryStream();
            photo.CopyTo(memoryStream);
            employee.Photo = memoryStream.ToArray();
        }
    }
}
