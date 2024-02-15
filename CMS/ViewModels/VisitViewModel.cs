using CMS.Models;

namespace CMS.ViewModels
{
    public class VisitViewModel
    {
        public Visit Visit { get; set; }
        public Patient Patient { get; set; }
        public Employee Employee { get; set; }
    }
}
