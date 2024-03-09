using System.ComponentModel.DataAnnotations;
using CMS.Models.Interfaces;

namespace CMS.Models
{
    public class Patient : IFullName
    {
        [Key]
        public int PatientId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();

        public Patient()
        {

        }
    }
}