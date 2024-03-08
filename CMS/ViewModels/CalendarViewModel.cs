namespace CMS.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<VisitViewModel> Visits { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}