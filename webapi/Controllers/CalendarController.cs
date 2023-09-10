using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Net;
using System.Numerics;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        public class LunarMonthRequest
        {
            public int Month { get; set; }
            public int Year { get; set;}
        }

        private ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

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

            var data = _calendarService.GetLunarMonth(req.Month, req.Year);

            return new MonthResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet("ToLunarDate")]
        public BaseResponse<LunarDate> ToLunarDay(int dd, int mm, int yyyy)
        {
            try
            {
                return new BaseResponse<LunarDate>
                {
                    ErrorMessage = "",
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = _calendarService.ToLunarDay(dd, mm, yyyy)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LunarDate>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        [HttpGet("ToGregoryDate")]
        public BaseResponse<string> ToGregoryDay(int dd, int mm, int yyyy)
        {
            try
            {
                var output = _calendarService.ToGregoryDay(dd, mm, yyyy);
                return new BaseResponse<string>
                {
                    ErrorMessage = "",
                    IsSuccess = output != "Ngày không đúng định dạng",
                    StatusCode = output != "Ngày không đúng định dạng" ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                    Data = output
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }
    }
}
