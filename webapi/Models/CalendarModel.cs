namespace webapi.Models
{
    public class CalendarModel
    {
        public class LunarDay
        {
            // ngay duong
            public DateTime GregoryDate { get; set; }
            public int GregoryInDate => GregoryDate.Day;
            public int GregoryInMonth => GregoryDate.Month;
            public int GregoryInYear => GregoryDate.Year;
            public int WeekIndex { get; set; }
            public int DayIndex { get; set; }
            public DayOfWeek GregoryDayInWeek => GregoryDate.DayOfWeek;

            // ngay am
            public int LunarInDate { get; set; }
            public int LunarInMonth { get; set; }
            public int LunarInYear { get; set; }

            public string SexagenaryYear { get; set; }
            public string SexagenaryMonth { get; set; }
            public string SexagenaryDate { get; set; }

            public bool IsActiveDay { get; set; }
            public bool IsToday { get; set; }
        }

        public class Week
        {
            public int Index { get; set; }
            public int WeekIndex { get; set; }
            public List<LunarDay> DaysOfWeek { get; set; } = new List<LunarDay>();
        }
    }
}
