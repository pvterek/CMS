using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS.Controllers
{
    public class HomeController(ApplicationDbContext context) : Controller
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
        public IActionResult Search(DateTime date)
        {
            List<Visit> visits = [.. context.Visit.Where(p => p.VisitTime >= date)];

            return View(visits);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
