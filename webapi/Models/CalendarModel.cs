using System.Net;

namespace webapi.Models
{
    public class LunarDate
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
        public List<LunarDate> DaysOfWeek { get; set; } = new List<LunarDate>();
    }

    public class Month
    {
        public int GregoryMonth { get; set; }
        public int GregoryYear { get; set; }
        public List<Week> Weeks { get; set; } = new List<Week>();
        public List<LunarDate> Dates { get; set; } = new List<LunarDate>();
    }

    public class LunarMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<Week> Weeks { get; set; } = new List<Week>();
        public List<LunarDate> Dates { get; set; } = new List<LunarDate>();
    }

    public class MonthResponse : BaseResponse<LunarMonth>
    {
    }
}
