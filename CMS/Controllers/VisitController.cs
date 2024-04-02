using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CMS.Controllers
{
    public class VisitController(ApplicationDbContext context) : Controller
    {

        // GET: VisitModels
        public async Task<IActionResult> Index()
        {
            var visits = await context.Visit
                .Include(v => v.Patient)
                .Include(v => v.Employee)
                .ToListAsync();

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
                .Include(v => v.Patient)
                .Include(v => v.Employee)
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
            var viewModel = await PopulateViewModel(new() { VisitTime = DateTime.Today });
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visit")] VisitPatientEmployeeViewModel viewModel)
        {
            var visit = viewModel.Visit;

            if (ValidateVisit(visit))
            {
                context.Add(visit);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            viewModel = await PopulateViewModel(viewModel.Visit);
            return View(viewModel);
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

            var viewModel = await PopulateViewModel(visit);
            return View(viewModel);
        }

        // POST: VisitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Visit")] VisitPatientEmployeeViewModel viewModel)
        {
            var visit = viewModel.Visit;

            if (id != visit.VisitId)
            {
                return NotFound();
            }

            if (ValidateVisit(visit))
            {
                try
                {
                    context.Update(visit);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) when (!VisitExists(visit.VisitId))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            viewModel = await PopulateViewModel(viewModel.Visit);
            return View(viewModel);
        }

        // GET: VisitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await context.Visit
                .Include(v => v.Patient)
                .Include(v => v.Employee)
                .FirstOrDefaultAsync(m => m.VisitId == id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
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

        private bool ValidateVisit(Visit visit)
        {
            return Validator.TryValidateObject(visit, new ValidationContext(visit), null, true);
        }

        private async Task<VisitPatientEmployeeViewModel> PopulateViewModel(Visit visit)
        {
            var viewModel = new VisitPatientEmployeeViewModel
            {
                Visit = visit,
                Patients = await context.Patient.ToListAsync(),
                Employees = await context.Employee.ToListAsync()
            };

            return viewModel;
        }
    }
}
