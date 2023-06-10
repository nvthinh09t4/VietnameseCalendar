namespace webapi.Entities.Dto
{
    public class HolidayDto
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public string? Description { get; set; }
        public bool IsLunarHoliday { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public List<string> ReferenceLinks { get; set; }
        public int DayOff { get; set; }
        public string? Url { get; set; }
    }
}
