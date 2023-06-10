namespace webapi.Models
{
    public class CreateHolidayModel
    {
        public string HolidayName { get; set; }
        public string? Description { get; set; }
        public bool IsLunarHoliday { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int DayOff { get; set; }
        public string? Url { get; set; }
        public List<string> ReferenceLinks { get; set; } = new List<string>();
    }

    public class UpdateHolidayModel
    {
        public string HolidayName { get; set; }
        public string? Description { get; set; }
        public bool IsLunarHoliday { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int DayOff { get; set; }
        public string? Url { get; set; }
        public List<string> ReferenceLinks { get; set; } = new List<string>();
    }

    public class GetHolidaysModel
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int IsLunarDay {get;set; }
    }
}
