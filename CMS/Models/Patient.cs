using CMS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

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

        public byte[]? Photo { get; set; }

        public Patient()
        {

        }
    }
}