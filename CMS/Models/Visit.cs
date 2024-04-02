using CMS.Models.CustomValidationAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }

        [Required(ErrorMessage = "Please select a patient.")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [Required(ErrorMessage = "Please select an employee.")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [Required(ErrorMessage = "Please enter a visit time.")]
        [DisplayName("Time of the visit")]
        [DateTimeRange(DateTimeRangeMode.Visit)]
        public DateTime VisitTime { get; set; }

        public Visit()
        {

        }
    }
}