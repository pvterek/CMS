using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class VisitModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VisitModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.VisitModel.ToListAsync());
        }

        // GET: VisitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitModel = await _context.VisitModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitModel == null)
            {
                return NotFound();
            }

            return View(visitModel);
        }

        // GET: VisitModels/Create
        public IActionResult Create()
        {
            ViewBag.Patients = _context.PatientModel.Select(p => new { p.Id, p.FullName }).ToList();
            ViewBag.Employees = _context.EmployeeModel.Select(e => new { e.Id, e.FullName }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VisitModel visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = _context.PatientModel.Select(p => new { p.Id, p.FullName }).ToList();
            ViewBag.Employees = _context.EmployeeModel.Select(e => new { e.Id, e.FullName }).ToList();

            return View(visit);
        }

        // GET: VisitModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitModel = await _context.VisitModel.FindAsync(id);
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
            if (id != visitModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitModelExists(visitModel.Id))
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

            var visitModel = await _context.VisitModel
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var visitModel = await _context.VisitModel.FindAsync(id);
            if (visitModel != null)
            {
                _context.VisitModel.Remove(visitModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitModelExists(int id)
        {
            return _context.VisitModel.Any(e => e.Id == id);
        }
    }
}
