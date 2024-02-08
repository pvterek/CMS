using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class VisitModelsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VisitModelsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: VisitModels
        public async Task<IActionResult> Index()
        {
            return View(await _db.VisitModel.ToListAsync());
        }

        // GET: VisitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitModel = await _db.VisitModel
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visitModel == null)
            {
                return NotFound();
            }

            return View(visitModel);
        }

        // GET: VisitModels/Create
        public IActionResult Create()
        {
            var visit = new VisitModel();
            var patients = _db.PatientModel.ToList();
            var employees = _db.EmployeeModel.ToList();

            var viewModel = new VisitPatientEmployeeViewModel
            {
                Visit = visit,
                Patients = patients,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VisitModel visit)
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

            var visitModel = await _db.VisitModel.FindAsync(id);
            if (visitModel == null)
            {
                return NotFound();
            }
            return View(visitModel);
        }

        // POST: VisitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitTime")] VisitModel visitModel)
        {
            if (id != visitModel.VisitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(visitModel);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitModelExists(visitModel.VisitId))
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
            return View(visitModel);
        }

        // GET: VisitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitModel = await _db.VisitModel
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
            var visitModel = await _db.VisitModel.FindAsync(id);
            if (visitModel != null)
            {
                _db.VisitModel.Remove(visitModel);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitModelExists(int id)
        {
            return _db.VisitModel.Any(e => e.VisitId == id);
        }
    }
}
