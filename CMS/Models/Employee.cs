using CMS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Employee : IFullName
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        [ForeignKey("ProfessionId")]
        public virtual Profession Profession { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();

        public byte[]? Photo { get; set; }

        public Employee()
        {

        }
    }
}