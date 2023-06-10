using Microsoft.EntityFrameworkCore;
using webapi.Entities.Dto;

namespace webapi.Entities.Repository
{
    public interface IHolidayRepository : IBaseRepository<Holiday>
    {
        Task<List<Holiday>> GetHolidays(bool isLunarDay);
    }
    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Holiday>> GetHolidays(bool isLunarDay)
        {
            List<Holiday> result = await GetAll().Where(x => x.IsLunarHoliday == isLunarDay).ToListAsync();
            return result;
        }
    }
}
