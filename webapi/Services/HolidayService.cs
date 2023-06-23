using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webapi.Entities;
using webapi.Entities.Dto;
using webapi.Entities.Repository;
using webapi.Models;

namespace webapi.Services
{
    public interface IHolidayService
    {
        Task<HolidayDto> CreateHoliday(CreateHolidayModel request);
        Task CreateHolidays(List<CreateHolidayModel> request);
        Task<HolidayDto> UpdateHoliday(UpdateHolidayModel request);
        Task<List<HolidayDto>> GetHolidays(GetHolidaysModel request);
        Task<HolidayDto> GetHoliday(int id);
        Task<HolidayDto> GetHoliday(string url);
        Task<List<HolidayDto>> GetHolidays(bool isLunarDay);
        Task DeleteHoliday(int id);
    }
    public class HolidayService : IHolidayService
    {
        private IHolidayRepository _holidayRepository;
        private IMapper _mapper;

        public HolidayService(IHolidayRepository holidayRepository, IMapper mapper) 
        {
            _holidayRepository = holidayRepository;
            _mapper = mapper;
        }

        public async Task<HolidayDto> CreateHoliday(CreateHolidayModel request)
        {
            var holiday = new Holiday
            {
                HolidayName = request.HolidayName,
                Description = request.Description,
                Day = request.Day,
                Month = request.Month,
                IsLunarHoliday = request.IsLunarHoliday,
                DayOff = request.DayOff,
                Url = request.Url,
                ReferenceLinks = JsonConvert.SerializeObject(request.ReferenceLinks),
            };
            var result = await _holidayRepository.CreateAsync(holiday);
            return _mapper.Map<HolidayDto>(result);
        }

        public async Task CreateHolidays(List<CreateHolidayModel> request)
        {
            List<Holiday> holidays = new List<Holiday>();
            foreach (var holiday in request)
            {
                holidays.Add(new Holiday
                {
                    HolidayName = holiday.HolidayName,
                    Description = holiday.Description,
                    Day = holiday.Day,
                    Month = holiday.Month,
                    IsLunarHoliday = holiday.IsLunarHoliday,
                    DayOff = holiday.DayOff,
                    Url = holiday.Url,
                    ReferenceLinks = JsonConvert.SerializeObject(holiday.ReferenceLinks),
                });
            }
            await _holidayRepository.CreateRangeAsync(holidays);
        }

        public Task DeleteHoliday(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HolidayDto> GetHoliday(int id)
        {
            var holiday = await _holidayRepository.GetAsync(id);
            return _mapper.Map<HolidayDto>(holiday);
        }

        public async Task<HolidayDto> GetHoliday(string url)
        {
            var holiday = await _holidayRepository.GetAll().FirstOrDefaultAsync(x => x.Url == url);
            return _mapper.Map<HolidayDto>(holiday);
        }

        public Task<List<HolidayDto>> GetHolidays(GetHolidaysModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HolidayDto>> GetHolidays(bool isLunarDay)
        {
            var holidays = await _holidayRepository.GetHolidays(isLunarDay);
            return _mapper.Map<List<HolidayDto>>(holidays);
        }

        public Task<HolidayDto> UpdateHoliday(UpdateHolidayModel request)
        {
            throw new NotImplementedException();
        }
    }
}
