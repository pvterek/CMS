using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = DateTime.Today.AddDays(1).AddTicks(-1);

            var visitsViewModel = new VisitsViewModel
            {
                Visits = await _db.VisitModel
                    .Where(p => p.VisitTime >= todayStart && p.VisitTime <= todayEnd)
                    .Join(_db.PatientModel, visit => visit.PatientId, patient => patient.PatientId, (visit, patient) => new { visit, patient })
                    .Join(_db.EmployeeModel, vp => vp.visit.EmployeeId, employee => employee.EmployeeId, (vp, employee) => new VisitViewModel
                    {
                        PatientFullName = vp.patient.FullName,
                        EmployeeFullName = employee.FullName,
                        VisitTime = vp.visit.VisitTime
                    })
                    .ToListAsync()
            };

            return View(visitsViewModel);
        }

        [HttpPost]
        public IActionResult Search(DateTime date)
        {
            List<VisitModel> visits = _db.VisitModel
                .Where(p => p.VisitTime >= date) //&& p.VisitTime <= DateTime.Today)
                .ToList();

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
