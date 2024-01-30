namespace CMS.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

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