using CMS.Models;

namespace CMS.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<Visit> Visits { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}