namespace CMS.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();

        public PatientModel()
        {

        }
    }
}