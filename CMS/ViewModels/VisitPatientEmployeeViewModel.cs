using CMS.Models;

namespace CMS.ViewModels
{
    public class VisitPatientEmployeeViewModel
    {
        public VisitModel Visit { get; set; }
        public List<PatientModel> Patients { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}
