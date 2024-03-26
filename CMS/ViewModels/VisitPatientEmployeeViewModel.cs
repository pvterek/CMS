using CMS.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS.ViewModels
{
    public class VisitPatientEmployeeViewModel
    {
        [Required]
        public Visit Visit { get; set; }

        [Required]
        public List<Patient> Patients { get; set; }

        [Required]
        public List<Employee> Employees { get; set; }
    }
}
