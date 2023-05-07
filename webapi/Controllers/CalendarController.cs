using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Net;
using System.Numerics;
using static webapi.Models.CalendarModel;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        public class MonthResponse
        {
            public HttpStatusCode StatusCode { get; set; }
            public bool IsSuccess { get; set; }
            public string? ErrorMessage { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public List<Week> Weeks { get; set; } = new List<Week>();
            public List<LunarDay> Dates { get; set; } = new List<LunarDay>();
        }

        public class LunarDateResponse
        {
            public HttpStatusCode StatusCode { get; set; }
            public bool IsSuccess { get; set; }
            public string? ErrorMessage { get; set; }
            public LunarDay LunarDate { get; set; }
        }

        

        public class LunarMonthRequest
        {
            public int Month { get; set; }
            public int Year { get; set;}
        }

        string[] Can = { "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ"};
        string[] Chi = { "Thân", "Dậu", "Tuất", "Hợi", "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi" };

        
        string[] ChiThang = { "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi", "Tý", };

        DateTime NgayNeo = new DateTime(2023, 5, 2);
        string[] CanNgay = { "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ" };
        string[] ChiNgay = { "Thân", "Dậu", "Tuất", "Hợi", "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi" };

        [HttpPost("GetLunarMonth")]
        public MonthResponse GetLunarMonth(LunarMonthRequest req)
        {
            if (req == null)
            {
                return new MonthResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccess = false, ErrorMessage = "null request" };
            }

            if (req.Month < 0 || req.Month > 12)
            {
                return new MonthResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccess = false, ErrorMessage = "month not support" };
            }

            var calendar = new ChineseLunisolarCalendar();
            if (req.Year < calendar.MinSupportedDateTime.Year || req.Year > calendar.MaxSupportedDateTime.Year)
            {
                return new MonthResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccess = false, ErrorMessage = "year not support" };
            }

            var lunarDates = new List<LunarDay>();
            var startDate = new DateTime(req.Year, req.Month, 1);
            var lastDate = new DateTime(req.Year, req.Month, DateTime.DaysInMonth(req.Year, req.Month));
            

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
                var lunarDate = GetLunarDate(currDate, req.Month, req.Year, true);
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
                    var lunarDate = GetLunarDate(currDate, req.Month, req.Year, false);
                    lunarDates.Insert(0, lunarDate);
                    weeks[0].DaysOfWeek.Insert(0, lunarDate);
                }
            }

            if (weeks[weeks.Count-1].DaysOfWeek.Count < 7)
            {
                var remainingDays = 7 - weeks[weeks.Count - 1].DaysOfWeek.Count;
                var lastStartDate = lastDate.AddDays(1);
                var lastEndDate = lastDate.AddDays(remainingDays);
                for (var currDate = lastStartDate; currDate <= lastEndDate; currDate = currDate.AddDays(1))
                {
                    var lunarDate = GetLunarDate(currDate, req.Month, req.Year, false);
                    lunarDates.Add(lunarDate);
                    weeks[weeks.Count - 1].DaysOfWeek.Add(lunarDate);
                }
            }

            return new MonthResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Weeks = weeks,
                Dates = lunarDates
            };
        }

        [HttpGet("ToLunarDate")]
        public LunarDateResponse ToLunarDate(int dd, int mm, int yyyy)
        {
            try
            {
                var dateTime = new DateTime(yyyy, mm, dd);
                return new LunarDateResponse
                {
                    ErrorMessage = "",
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    LunarDate = GetLunarDate(dateTime, mm, yyyy, true)
                };
            }
            catch (Exception ex)
            {
                return new LunarDateResponse
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadGateway,
                };
            }
        }

        LunarDay GetLunarDate(DateTime currDate, int month, int year, bool isActiveDay)
        {
            var calendar = new ChineseLunisolarCalendar();
            var currLunarMonth = calendar.GetMonth(currDate);
            var leapMonth = calendar.GetLeapMonth(year);
            var lunarInDate = calendar.GetDayOfMonth(currDate);
            var lunarInMonth = currLunarMonth < leapMonth ? currLunarMonth : currLunarMonth - 1;
            var lunarInYear = calendar.GetYear(currDate);
            Calendar cal = new CultureInfo("vi-VN").Calendar;
            var weekIndex = cal.GetWeekOfYear(currDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return new LunarDay
            {
                GregoryDate = currDate,
                LunarInDate = lunarInDate,
                LunarInMonth = lunarInMonth,
                LunarInYear = lunarInYear,
                SexagenaryYear = $"{Can[lunarInYear % 10]} {Chi[lunarInYear % 12]}",
                SexagenaryMonth = GetCanChiThang(lunarInYear, lunarInMonth),
                SexagenaryDate = GetCanChiNgay(currDate),
                IsActiveDay = isActiveDay,
                WeekIndex = weekIndex,
                IsToday = currDate.Date == DateTime.Today
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
