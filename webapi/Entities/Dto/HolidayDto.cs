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
        public bool IsImportant { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description1 { get; set; }
        public string? ImgUrl1 { get; set; }
        public string? Description2 { get; set; }
        public string? ImgUrl2 { get; set; }
        public string? Description3 { get; set; }
        public string? ImgUrl3 { get; set; }
        public string? Description4 { get; set; }
        public string? ImgUrl4 { get; set; }
        public string? Description5 { get; set; }
        public string? ImgUrl5 { get; set; }
        public string? ImgCaption1 { get; set; }
        public string? ImgCaption2 { get; set; }
        public string? ImgCaption3 { get; set; }
        public string? ImgCaption4 { get; set; }
        public string? ImgCaption5 { get; set; }
    }
}
