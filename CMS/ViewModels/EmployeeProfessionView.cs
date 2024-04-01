using CMS.Models;

namespace CMS.ViewModels
{
    public class EmployeeProfessionView
    {
        public Employee Employee { get; set; }

        public IEnumerable<Profession> Professions { get; set; }

        public IFormFile Photo { get; set; }
    }
}
