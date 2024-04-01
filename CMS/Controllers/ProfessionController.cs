using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class ProfessionController(ApplicationDbContext context) : Controller
    {

        // GET: Profession
        public async Task<IActionResult> Index()
        {
            return View(await context.Profession.ToListAsync());
        }

        // GET: Profession/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await context.Profession
                .FirstOrDefaultAsync(m => m.ProfessionId == id);
            if (profession == null)
            {
                return NotFound();
            }

            return View(profession);
        }

        // GET: Profession/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profession/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessionId,Name")] Profession profession)
        {
            if (ModelState.IsValid)
            {
                context.Add(profession);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profession);
        }

        // GET: Profession/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await context.Profession.FindAsync(id);
            if (profession == null)
            {
                return NotFound();
            }
            return View(profession);
        }

        // POST: Profession/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessionId,Name")] Profession profession)
        {
            if (id != profession.ProfessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(profession);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionExists(profession.ProfessionId))
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
            return View(profession);
        }

        // GET: Profession/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await context.Profession
                .FirstOrDefaultAsync(m => m.ProfessionId == id);
            if (profession == null)
            {
                return NotFound();
            }

            return View(profession);
        }

        // POST: Profession/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profession = await context.Profession.FindAsync(id);
            if (profession != null)
            {
                context.Profession.Remove(profession);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionExists(int id)
        {
            return context.Profession.Any(e => e.ProfessionId == id);
        }
    }
}
