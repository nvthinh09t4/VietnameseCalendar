using Microsoft.AspNetCore.Mvc;
using webapi.Entities;
using webapi.Models;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingInforController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITrackingInforService _trackingInforService;
        public TrackingInforController(ApplicationDbContext context, ITrackingInforService trackingInforService) 
        {
            _context = context;
            _trackingInforService = trackingInforService;
        }
        // POST api/<TrackingInforController>
        [HttpPost]
        public void Post([FromBody] CreateTrackingInforModel model)
        {
            _trackingInforService.CreateTrackingInfor(new TrackingInfor
            {
                CreatedDate = DateTime.Now,
                IpAddress = model.IpAddress,
                UserAgent = model.UserAgent,
                AdditionInfor = model.AdditionInfor
            });
        }
    }
}
