using webapi.Entities;
using webapi.Entities.Dto;
using webapi.Entities.Repository;

namespace webapi.Services
{
    public interface ITrackingInforService
    {
        Task CreateTrackingInfor(TrackingInfor trackingInfor);
    }
    public class TrackingInforService : ITrackingInforService
    {
        private ITrackingInforRepository _trackingInforRepository;

        public TrackingInforService(ITrackingInforRepository trackingInforRepository)
        {
            _trackingInforRepository = trackingInforRepository;
        }
        public Task CreateTrackingInfor(TrackingInfor trackingInfor)
        {
            return _trackingInforRepository.CreateAsync(trackingInfor);
        }
    }
}
