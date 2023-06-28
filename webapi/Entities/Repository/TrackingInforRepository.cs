using Microsoft.EntityFrameworkCore;
using webapi.Entities.Dto;

namespace webapi.Entities.Repository
{
    public interface ITrackingInforRepository : IBaseRepository<TrackingInfor>
    {
    }
    public class TrackingInforRepository : BaseRepository<TrackingInfor>, ITrackingInforRepository
    {
        public TrackingInforRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
