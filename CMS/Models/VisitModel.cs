using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class VisitModel
    {
        [Key]
        public int VisitId { get; set; }


        [Required(ErrorMessage = "Please select a patient.")]
        public int PatientId { get; set; }


        [Required(ErrorMessage = "Please select an employee.")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Please enter a visit time.")]
        public DateTime VisitTime { get; set; }

        //public int AddedBy { get; set; }

        public VisitModel()
        {

        }
    }
}