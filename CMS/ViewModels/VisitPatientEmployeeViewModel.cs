using CMS.Models;

namespace CMS.ViewModels
{
    public class VisitPatientEmployeeViewModel
    {
        public Visit Visit { get; set; }

        public List<Patient> Patients { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
