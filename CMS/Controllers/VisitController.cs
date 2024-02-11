using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class VisitController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VisitController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: VisitModels
        public async Task<IActionResult> Index()
        {
            return View(await _db.Visit.ToListAsync());
        }

        // GET: VisitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _db.Visit
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: VisitModels/Create
        public IActionResult Create()
        {
            var visit = new Visit();
            var patients = _db.Patient.ToList();
            var employees = _db.Employee.ToList();

            var viewModel = new VisitPatientEmployeeViewModel
            {
                Visit = visit,
                Patients = patients,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Visit visit)
        {
            if (ModelState.IsValid)
            {
                _db.Add(visit);
                await _db.SaveChangesAsync();

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

            var visit = await _db.Visit.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            return View(visit);
        }

        // POST: VisitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitTime")] Visit visit)
        {
            if (id != visit.VisitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(visit);
                    await _db.SaveChangesAsync();
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

            var visitModel = await _db.Visit
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
            var visit = await _db.Visit.FindAsync(id);
            if (visit != null)
            {
                _db.Visit.Remove(visit);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _db.Visit.Any(e => e.VisitId == id);
        }
    }
}
