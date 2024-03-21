using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class VisitController(ApplicationDbContext context) : Controller
    {

        // GET: VisitModels
        public async Task<IActionResult> Index()
        {
            var visitsWithPatientsQuery =
                from visit in context.Visit
                join patient in context.Patient on visit.PatientId equals patient.PatientId into patients
                from patient in patients.DefaultIfEmpty()
                select new
                {
                    visit,
                    patient
                };

            var visitsWithEmployeesQuery =
                from vp in visitsWithPatientsQuery
                join employee in context.Employee on vp.visit.EmployeeId equals employee.EmployeeId into employees
                from employee in employees.DefaultIfEmpty()
                select new VisitViewModel
                {
                    Visit = vp.visit,
                    Patient = vp.patient,
                    Employee = employee
                };

            var visits = await visitsWithEmployeesQuery.ToListAsync();

            return View(visits);
        }


        // GET: VisitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await context.Visit
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: VisitModels/Create
        public async Task<IActionResult> Create()
        {
            VisitPatientEmployeeViewModel viewModel = new()
            {
                Visit = new() { VisitTime = DateTime.Today },
                Patients = await context.Patient.ToListAsync(),
                Employees = await context.Employee.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Visit visit)
        {
            if (ModelState.IsValid)
            {
                context.Add(visit);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(visit);
        }

        // GET: VisitModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await context.Visit.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }

            VisitPatientEmployeeViewModel visitDetails = new()
            {
                Visit = visit,
                Patients = await context.Patient.ToListAsync(),
                Employees = await context.Employee.ToListAsync()
            };

            return View(visitDetails);
        }

        // POST: VisitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitId,PatientId,EmployeeId,VisitTime")] Visit visit)
        {
            if (id != visit.VisitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(visit);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.VisitId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visit);
        }

        // GET: VisitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitModel = await context.Visit
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visitModel == null)
            {
                return NotFound();
            }

            return View(visitModel);
        }

        // POST: VisitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await context.Visit.FindAsync(id);
            if (visit != null)
            {
                context.Visit.Remove(visit);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return context.Visit.Any(e => e.VisitId == id);
        }
    }
}
