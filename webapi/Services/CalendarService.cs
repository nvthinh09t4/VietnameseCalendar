using System.Globalization;
using webapi.Models;

namespace webapi.Services
{
    public interface ICalendarService
    {
        LunarMonth GetLunarMonth(int month, int year);
        LunarDate ToLunarDay(int dd, int mm, int yyyy);
        string ToGregoryDay(int dd, int mm, int yyyy);
    }

    public class CalendarService : ICalendarService
    {
        string[] Can = { "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ" };
        string[] Chi = { "Thân", "Dậu", "Tuất", "Hợi", "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi" };
        string[] ChiThang = { "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi", "Tý", };
        DateTime NgayNeo = new DateTime(2023, 5, 2);
        string[] CanNgay = { "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ" };
        string[] ChiNgay = { "Thân", "Dậu", "Tuất", "Hợi", "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi" };
        private IKhongMinhHoroscopeCalendarService _khongMinhCalendar;

        public CalendarService(IKhongMinhHoroscopeCalendarService khongMinhCalendar)
        {
            _khongMinhCalendar = khongMinhCalendar;
        }

        public LunarMonth GetLunarMonth(int month, int year)
        {
            var lunarDates = new List<LunarDate>();
            var startDate = new DateTime(year, month, 1);
            var lastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            Calendar cal = new CultureInfo("vi-VN").Calendar;
            var weeks = new List<Week>();
            var currentWeek = new Week();
            var index = 0;
            for (var currDate = startDate; currDate <= lastDate; currDate = currDate.AddDays(1))
            {
                int week = cal.GetWeekOfYear(currDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (currentWeek.WeekIndex != week)
                {
                    if (currentWeek.DaysOfWeek.Count > 0)
                    {
                        weeks.Add(currentWeek);
                        index++;
                    }
                    currentWeek = new Week { Index = index, WeekIndex = week };
                }
                var lunarDate = GetLunarDate(currDate, month, year, true);
                currentWeek.DaysOfWeek.Add(lunarDate);
                lunarDates.Add(lunarDate);
            }

            if (weeks[weeks.Count - 1].Index != currentWeek.Index)
            {
                weeks.Add(currentWeek);
            }

            if (weeks[0].DaysOfWeek.Count < 7)
            {
                var remainingDays = 7 - weeks[0].DaysOfWeek.Count;
                var firstStartDate = startDate.AddDays(-remainingDays);
                var firstEndDate = startDate.AddDays(-1);
                for (var currDate = firstStartDate; currDate <= firstEndDate; currDate = currDate.AddDays(1))
                {
                    var lunarDate = GetLunarDate(currDate, month, year, false);
                    lunarDates.Insert(0, lunarDate);
                    weeks[0].DaysOfWeek.Insert(0, lunarDate);
                }
            }

            if (weeks[weeks.Count - 1].DaysOfWeek.Count < 7)
            {
                var remainingDays = 7 - weeks[weeks.Count - 1].DaysOfWeek.Count;
                var lastStartDate = lastDate.AddDays(1);
                var lastEndDate = lastDate.AddDays(remainingDays);
                for (var currDate = lastStartDate; currDate <= lastEndDate; currDate = currDate.AddDays(1))
                {
                    var lunarDate = GetLunarDate(currDate, month, year, false);
                    lunarDates.Add(lunarDate);
                    weeks[weeks.Count - 1].DaysOfWeek.Add(lunarDate);
                }
            }

            return new LunarMonth
            {
                Weeks = weeks,
                Dates = lunarDates
            };
        }

        public string ToGregoryDay(int dd, int mm, int yyyy)
        {
            var calendar = new ChineseLunisolarCalendar();
            var gregorianCal = new GregorianCalendar();

            try
            {
                DateTime lunarDate = calendar.ToDateTime(yyyy, month: mm, day: dd, /*hms:*/ 0, 0, 0, 0);
                Int32 year = gregorianCal.GetYear(lunarDate);
                Int32 month = gregorianCal.GetMonth(lunarDate);
                Int32 day = gregorianCal.GetDayOfMonth(lunarDate);
                return $"{day}/{month}/{year}";
            }
            catch (Exception ex)
            {
                return "Ngày không đúng định dạng";
            }
            
        }

        public LunarDate ToLunarDay(int dd, int mm, int yyyy)
        {
            return GetLunarDate(new DateTime(yyyy, mm, dd), mm, yyyy, false);
        }

        LunarDate GetLunarDate(DateTime currDate, int month, int year, bool isActiveDay)
        {
            var calendar = new ChineseLunisolarCalendar();
            var currLunarMonth = calendar.GetMonth(currDate);
            year = calendar.GetYear(currDate);
            var leapMonth = calendar.GetLeapMonth(year);
            var lunarInDate = calendar.GetDayOfMonth(currDate);
            var lunarInMonth = leapMonth == 0 ? currLunarMonth : (currLunarMonth < leapMonth ? currLunarMonth : currLunarMonth - 1);
            var lunarInYear = calendar.GetYear(currDate);
            Calendar cal = new CultureInfo("vi-VN").Calendar;
            var weekIndex = cal.GetWeekOfYear(currDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var ngayCanChi = GetCanChiNgay(currDate);
            return new LunarDate
            {
                GregoryDate = currDate,
                LunarInDate = lunarInDate,
                LunarInMonth = lunarInMonth,
                LunarInYear = lunarInYear,
                SexagenaryYear = $"{Can[lunarInYear % 10]} {Chi[lunarInYear % 12]}",
                SexagenaryMonth = GetCanChiThang(lunarInYear, lunarInMonth),
                SexagenaryDate = ngayCanChi,
                Can = ngayCanChi.Split(' ')[0],
                Chi = ngayCanChi.Split(' ')[1],
                IsActiveDay = isActiveDay,
                WeekIndex = weekIndex,
                IsToday = currDate.Date == DateTime.Today,
                KhongMinhHoroscopeInformation = _khongMinhCalendar.ToHoloscopeDate(lunarInMonth, lunarInDate)
            };
        }

        string GetCanChiThang(int year, int month)
        {
            string[] CanThang = { "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", };
            var canChiNam = year % 5;
            switch (canChiNam)
            {
                case 0:
                    CanThang = new string[] { "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", };
                    break;
                case 1:
                    CanThang = new string[] { "Kỷ", "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", };
                    break;
                case 2:
                    CanThang = new string[] { "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", };
                    break;
                case 3:
                    CanThang = new string[] { "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", };
                    break;
                case 4:
                    CanThang = new string[] { "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý", "Giáp", };
                    break;
            }
            return $"{CanThang[month % 10]} {ChiThang[month % 12]}";
        }

        string GetCanChiNgay(int year, int month, int date)
        {
            return GetCanChiNgay(new DateTime(year, month, date));
        }
        string GetCanChiNgay(DateTime date)
        {
            var delta = date - NgayNeo;
            var deltaDay = delta.Days;
            while (deltaDay < 0)
            {
                deltaDay += 60;
            }
            return $"{CanNgay[deltaDay % 10]} {ChiNgay[deltaDay % 12]}";
        }
    }
}
