using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string Profession { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();

        public EmployeeModel()
        {

        }
    }
}