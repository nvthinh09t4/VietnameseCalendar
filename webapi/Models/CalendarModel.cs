using System.Net;
using webapi.Extensions;
using webapi.Services;

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
        public string SexagenaryDate { get; set; } = "";
        public string Can { get; set; }

        private string _chi;
        public string Chi { 
            get { return _chi; }
            set { _chi = value; CalculateZodiacHours(); } 
        }

        public bool IsActiveDay { get; set; }
        public bool IsToday { get; set; }
        public HoloscopeDate KhongMinhHoroscopeInformation { get; set; }
        //Giờ hoàng đạo
        public List<ZodiacHour> ZodiacGoodHours { get; set; }

        public List<ZodiacHour> ZodiacBadHours { get; set; }
        private void CalculateZodiacHours()
        {
            ZodiacGoodHours = new List<ZodiacHour>();
            ZodiacBadHours = new List<ZodiacHour>();
            switch (Chi)
            {
                case "Dần":
                case "Thân":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tys });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Suu });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Thin });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ty });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Mui });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tuat });
                    break;
                case "Mão":
                case "Dậu":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tys });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dan });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Mao });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ngo });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Mui });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dau });
                    break;
                case "Thìn":
                case "Tuất":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dan });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Thin });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ty });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Than });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dau });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Hoi });
                    break;
                case "Tỵ":
                case "Hợi":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Suu });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Thin });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ngo });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tuat });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Hoi });
                    break;
                case "Tý":
                case "Ngọ":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tys });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Suu });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Mao });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ngo });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Than });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dau });
                    break;
                case "Sửu":
                case "Mùi":
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Dan });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Mao });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Ty });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Than });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Tuat });
                    ZodiacGoodHours.Add(new ZodiacHour { Zodiac = eZodiac.Hoi });
                    break;
            }
            foreach (var zodiac in Enum.GetValues(typeof(eZodiac)).Cast<eZodiac>())
            {
                if (!ZodiacGoodHours.Any(x => x.Zodiac == zodiac))
                {
                    ZodiacBadHours.Add(new ZodiacHour { Zodiac = zodiac });
                }
            }
        }
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

    //giờ hoàng đạo
    public class ZodiacHour
    {
        public eZodiac Zodiac { get; set; }
        public string Name => EnumHelper<eZodiac>.GetDisplayValue(Zodiac);
        public string TimeBox { get
            {
                var timeBox = string.Empty;
                switch (Zodiac)
                {
                    case eZodiac.Tys: timeBox = "từ 23h ngày hôm trước - 01h"; break;
                    case eZodiac.Suu: timeBox = "từ 01h – 03h"; break;
                    case eZodiac.Dan: timeBox = "từ 03h – 05h"; break;
                    case eZodiac.Mao: timeBox = "từ 05h – 07h"; break;
                    case eZodiac.Thin: timeBox = "từ 07h – 09h"; break;
                    case eZodiac.Ty: timeBox = "từ 09h – 11h"; break;
                    case eZodiac.Ngo: timeBox = "từ 11h – 13h"; break;
                    case eZodiac.Mui: timeBox = "từ 13h – 15h"; break;
                    case eZodiac.Than: timeBox = "từ 15h – 17h"; break;
                    case eZodiac.Dau: timeBox = "từ 17h – 19h"; break;
                    case eZodiac.Tuat: timeBox = "từ 19h – 21h"; break;
                    case eZodiac.Hoi: timeBox = "từ 21h – 23h"; break;
                }
                return timeBox;
            }
        }
    }

    public class MonthResponse : BaseResponse<LunarMonth>
    {
    }
}
