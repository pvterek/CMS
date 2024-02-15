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
            DateTime todayEnd = DateTime.Today.AddDays(1);

            var visits = await _db.Visit
                    .Where(p => p.VisitTime >= todayStart && p.VisitTime <= todayEnd)
                    .Join(_db.Patient, visit => visit.PatientId, patient => patient.PatientId, (visit, patient) => new { visit, patient })
                    .Join(_db.Employee, vp => vp.visit.EmployeeId, employee => employee.EmployeeId, (vp, employee) => new VisitViewModel
                    {
                        Visit = vp.visit,
                        Patient = vp.patient,
                        Employee = employee
                    })
                    .ToListAsync();

            return View(visits);
        }

        [HttpPost]
        public IActionResult Search(DateTime date)
        {
            List<Visit> visits = _db.Visit
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
