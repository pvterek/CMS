using CMS.Models.CustomValidationAttribute;
using CMS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Patient : IFullName
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateTimeRange(DateTimeRangeMode.Birthday)]
        public DateTime Birthday { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();

        public byte[]? Photo { get; set; }

        public Patient()
        {

        }
    }
}