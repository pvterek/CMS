using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class VisitModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please select a patient.")]
        public int PatientId { get; set; }


        [Required(ErrorMessage = "Please select an employee.")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Please enter a visit time.")]
        public DateTime VisitTime { get; set; }

        //public EmployeeModel AddedBy { get; set; }

        public VisitModel()
        {

        }
    }
}