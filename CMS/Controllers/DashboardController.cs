using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CMS.Controllers
{
    public class DashboardController(ApplicationDbContext context) : Controller
    {
        public async Task<IActionResult> Index(DateTime? date)
        {
            DateTime targetDate = date ?? DateTime.Today;
            DateTime endDate = targetDate.AddDays(1);

            var visits = await context.GetVisitsForDateRange(targetDate, endDate);

            var calendarData = new CalendarViewModel()
            {
                Visits = visits,
                CurrentDate = targetDate
            };

            return View(calendarData);
        }

        [HttpPost]
        public async Task<IActionResult> Search(DateTime date)
        {
            List<Visit> visits = await context.Visit.Where(p => p.VisitTime >= date).ToListAsync();

            return View(visits);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
